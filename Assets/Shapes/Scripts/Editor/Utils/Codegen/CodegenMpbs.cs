using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    public static class CodegenMpbs
    {
        private const string SIMPLE_TRANSFER = "protected override void TransferShapeProperties() => _ = 0; // :>";

        private static CodeWriter code;
        private static Dictionary<string, string> propToVarName;

        private static readonly string[] excludedShaders = { "Texture Core" }; // manually set up

        public static void Generate()
        {
            // load core files
            var coreShaders = ShapesIO.LoadCoreShaders()
                .Where(t => excludedShaders.Contains(t.name) == false)
                .OrderBy(x => x.name)
                .ToArray();

            // Dictionary for (_X) to (ShapesMaterialUtils.propX)
            propToVarName = LoadPropertyNames();

            // generate code
            code = new CodeWriter();
            using (code.MainScope(typeof(CodegenMpbs), "System.Collections.Generic", "UnityEngine"))
            {
                // main shaders
                coreShaders.ForEach(GenerateMpbClass);
                // text is a special case
                code.Spacer();
                using (code.Scope("internal class MpbText : MetaMpb"))
                {
                    code.Append(SIMPLE_TRANSFER);
                }
            }

            // save
            var path = ShapesIO.RootFolder + "/Scripts/Runtime/Immediate Mode/MetaMpbShapes.cs";
            code.WriteTo(path);
        }

        private static Dictionary<string, string> LoadPropertyNames()
        {
            var dict = new Dictionary<string, string>();
            var path = ShapesIO.RootFolder + "/Scripts/Runtime/Utils/ShapesMaterialUtils.cs";

            // manually parse this out I guess~
            foreach (var s in File.ReadAllLines(path))
                if (s.TrimStart().StartsWith("public static readonly int prop"))
                {
                    var propNameVar = s.Between("readonly int ", " = Shader.PropertyToID");
                    var propNameShader = s.Between("ToID( \"", "\" );");
                    dict[propNameShader] = propNameVar;
                }

            return dict;
        }

        // scuffed regex~
        private static string Between(this string s, string pre, string post)
        {
            return s.Split(new[] { pre }, StringSplitOptions.None)[1].Split(new[] { post }, StringSplitOptions.None)[0]
                .Trim();
        }

        private static List<Property> GetInterfaceListProperties<T>()
        {
            return typeof(T).GetProperties()
                .Select(p => new Property(p.PropertyType.GenericTypeArguments[0].Name, p.Name))
                .OrderBy(p => p.name).ToList();
        }

        private static void GenerateMpbClass(TextAsset coreFile)
        {
            var path = AssetDatabase.GetAssetPath(coreFile);
            var lines = File.ReadAllLines(path);

            var filled = false;
            var dashed = false;
            var properties = new List<PropertyVar>();
            var propertiesFill = GetInterfaceListProperties<IFillableMpb>();
            var propertiesDash = GetInterfaceListProperties<IDashableMpb>();

            // find all properties from the core shader files
            var propStart = false;
            foreach (var s in lines)
            {
                var sTrimmed = s.TrimStart();
                if (propStart)
                {
                    if (sTrimmed.StartsWith("SHAPES_FILL_PROPERTIES"))
                    {
                        filled = true;
                    }
                    else if (sTrimmed.StartsWith("SHAPES_DASH_PROPERTIES"))
                    {
                        dashed = true;
                    }
                    else if (sTrimmed.StartsWith("PROP_DEF("))
                    {
                        var contents = s.Between("_DEF(", ")");
                        var typeAndName = contents.Split(',');
                        var type = ShaderTypeToCsType(typeAndName[0].Trim());
                        var name = typeAndName[1].Trim();
                        if (name != "_Color") // skip global properties
                            properties.Add(new PropertyVar(type, name));
                    }
                    else if (sTrimmed.StartsWith("UNITY_INSTANCING_BUFFER_END"))
                    {
                        break; // done
                    }
                }
                else if (sTrimmed.StartsWith("UNITY_INSTANCING_BUFFER_START"))
                {
                    propStart = true;
                }
            }

            var shapeName = coreFile.name.Substring(0, coreFile.name.Length - " Core".Length);
            var className = "Mpb" + shapeName.Replace(" ", "");
            var baseTypes = "MetaMpb";
            if (filled) baseTypes += $", {nameof(IFillableMpb)}";
            if (dashed) baseTypes += $", {nameof(IDashableMpb)}";

            properties = properties.OrderBy(p => p.nameCsVar).ToList();

            code.Spacer();
            using (code.Scope($"internal class {className} : {baseTypes}"))
            {
                // shape vars
                if (properties.Count > 0)
                {
                    code.Spacer();
                    foreach (var p in properties)
                        code.Append($"internal readonly List<{p.type}> {p.nameCsVar} = InitList<{p.type}>();");
                }

                // fill/dash properties
                if (filled)
                {
                    code.Spacer();
                    code.Comment("fill boilerplate");
                    foreach (var p in propertiesFill)
                        code.Append(
                            $"List<{p.type}> {nameof(IFillableMpb)}.{p.name} {{ get; }} = InitList<{p.type}>();");
                }

                if (dashed)
                {
                    code.Spacer();
                    code.Comment("dash boilerplate");
                    foreach (var p in propertiesDash)
                        code.Append(
                            $"List<{p.type}> {nameof(IDashableMpb)}.{p.name} {{ get; }} = InitList<{p.type}>();");
                }

                // transfer function
                code.Spacer();
                if (properties.Count == 0)
                    code.Append("protected override void TransferShapeProperties() => _ = 0; // :>");
                else
                    using (code.Scope("protected override void TransferShapeProperties()"))
                    {
                        foreach (var p in properties)
                            code.Append($"Transfer( {nameof(ShapesMaterialUtils)}.{p.nameCsVarProp}, {p.nameCsVar} );");
                    }

                code.Spacer();
            }
        }

        private static string ShaderTypeToCsType(string t)
        {
            switch (t)
            {
                case "int":
                case "half":
                case "fixed":
                case "float":
                    return "float"; // SetFloat()
                case "int2":
                case "half2":
                case "fixed2":
                case "float2":
                case "int3":
                case "half3":
                case "fixed3":
                case "float3":
                case "int4":
                case "half4":
                case "fixed4":
                case "float4":
                    return "Vector4"; // SetVector()
            }

            throw new ArgumentException("INVALID TYPE: " + t);
        }


        private struct PropertyVar
        {
            public PropertyVar(string type, string nameShaderProp)
            {
                this.type = type;
                this.nameShaderProp = nameShaderProp;
                nameCsVar = char.ToLower(nameShaderProp[1]) + nameShaderProp.Substring(2);
                nameCsVarProp = propToVarName[nameShaderProp];
            }

            public readonly string type; // Vector4
            public string nameShaderProp; // _Rect
            public readonly string nameCsVar; // rect
            public readonly string nameCsVarProp; // ShapesMaterialUtils.propRect
        }

        private struct Property
        {
            public Property(string type, string name)
            {
                this.type = type;
                this.name = name;
                if (type == "Single")
                    this.type = "float";
            }

            public readonly string type; // Vector4
            public readonly string name; // _Rect
        }
    }
}