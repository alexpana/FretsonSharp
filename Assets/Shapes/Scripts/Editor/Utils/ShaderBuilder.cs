using System;
using System.Collections.Generic;
using System.Linq;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    internal struct ShaderTag
    {
        private string key, value;

        public static implicit operator ShaderTag((string key, string value) noot)
        {
            return new ShaderTag() { key = noot.key, value = noot.value };
        }

        public static implicit operator string(ShaderTag st)
        {
            return st.ToString();
        }

        public override string ToString()
        {
            return $"\"{key}\" = \"{value}\"";
        }
    }

    internal class ShaderBuilder
    {
        private const char INDENT_STR = '\t';

        // all multi_compiles are defined here
        public static Dictionary<string, MultiCompile[]> shaderKeywords = new()
        {
            { "Disc", new[] { new MultiCompile("INNER_RADIUS"), new MultiCompile("SECTOR") } },
            { "Line 2D", new[] { new MultiCompile("CAP_ROUND", "CAP_SQUARE") } },
            { "Line 3D", new[] { new MultiCompile("CAP_ROUND", "CAP_SQUARE") } },
            {
                "Polyline 2D",
                new[]
                {
                    new MultiCompile("IS_JOIN_MESH"),
                    new MultiCompile("JOIN_MITER", "JOIN_ROUND",
                        "JOIN_BEVEL") /*, new MultiCompile( "CAP_ROUND", "CAP_SQUARE" )*/
                }
            },
            { "Rect", new[] { new MultiCompile("CORNER_RADIUS"), new MultiCompile("BORDERED") } }
        };

        private readonly ShapesBlendMode blendMode;
        private int indentLevel = 0;

        public string shader;
        private readonly string shaderName;

        public ShaderBuilder(string name, ShapesBlendMode blendMode, RenderPipeline rp)
        {
            this.blendMode = blendMode;
            shaderName = name;

            using (Scope($"Shader \"Shapes/{name} {blendMode.ToString()}\""))
            {
                using (Scope("Properties"))
                {
                    AppendLine("[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest (\"Z Test\", int) = 4");
                    AppendLine("_ZOffsetFactor (\"Z Offset Factor\", Float ) = 0");
                    AppendLine("_ZOffsetUnits (\"Z Offset Units\", int ) = 0");

                    AppendLine(
                        "[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp (\"Stencil Comparison\", int) = 8");
                    AppendLine(
                        "[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpPass (\"Stencil Operation Pass\", int) = 0");
                    AppendLine("_StencilID (\"Stencil ID\", int) = 0");
                    AppendLine("_StencilReadMask (\"Stencil Read Mask\", int) = 255");
                    AppendLine("_StencilWriteMask (\"Stencil Write Mask\", int) = 255");
                    AppendLine("_ColorMask (\"Color Mask\", int) = 15");
                    if (name == "Texture")
                        AppendLine("_MainTex (\"Texture\", 2D) = \"white\" {}");
                }

                using (Scope("SubShader"))
                {
                    using (Scope("Tags"))
                    {
                        // subshader tags
                        AppendLines(rp.GetSubshaderTags());
                        AppendLines(blendMode.GetSubshaderTags());
                    }

                    AppendPass(ShaderPassType.Render, rp);
                    if (rp != RenderPipeline.Legacy)
                        AppendPass(ShaderPassType.DepthOnly, rp);
                    AppendPass(ShaderPassType.Picking, rp);
                    AppendPass(ShaderPassType.Selection, rp);
                }
            }
        }

        public BracketScope Scope(string line)
        {
            return new BracketScope(this, line);
        }

        private void AppendPass(ShaderPassType pass, RenderPipeline rp)
        {
            using (Scope("Pass"))
            {
                // Name & LightMode
                var isLegacyMainRenderPass = rp == RenderPipeline.Legacy && pass == ShaderPassType.Render;
                if (isLegacyMainRenderPass == false)
                {
                    (var passName, var lightMode) = pass.NameAndLightMode(rp);
                    AppendLine($"Name \"{passName}\"");
                    AppendLine("Tags { " + (ShaderTag)("LightMode", lightMode) + " }");
                }

                using (Scope("Stencil"))
                {
                    AppendLine("Comp [_StencilComp]");
                    AppendLine("Pass [_StencilOpPass]");
                    AppendLine("Ref [_StencilID]");
                    AppendLine("ReadMask [_StencilReadMask]");
                    AppendLine("WriteMask [_StencilWriteMask]");
                }

                // culling/blend mode etc
                if (pass == ShaderPassType.Render)
                    AppendLines(blendMode.GetPassRenderStates());
                else
                    AppendLine("Cull Off"); // todo: might be incorrect for DepthOnly

                // hlsl program
                AppendHlslProgram(pass, rp);
            }
        }


        private void AppendHlslProgram(ShaderPassType passType, RenderPipeline rp)
        {
            AppendLine("HLSLPROGRAM");
            indentLevel++;
            AppendLine("#pragma vertex vert");
            AppendLine("#pragma fragment frag");
            AppendLine("#pragma multi_compile_fog");
            AppendLine("#pragma multi_compile_instancing");
            if (rp != RenderPipeline.Legacy)
            {
                AppendLine("#pragma prefer_hlslcc gles");
                AppendLine("#pragma exclude_renderers d3d11_9x");
                AppendLine("#pragma target 2.0");
            }

            if (shaderKeywords.ContainsKey(shaderName))
                AppendLines(shaderKeywords[shaderName].Select(x => x.ToString()));
            AppendLine($"#define {blendMode.BlendShaderDefine()}");
            if (passType == ShaderPassType.Picking || passType == ShaderPassType.Selection)
            {
                AppendLine("#pragma instancing_options assumeuniformscaling nomatrices nolightprobe nolightmap");
                if (passType == ShaderPassType.Picking)
                    AppendLine("#define SCENE_VIEW_PICKING");
                else if (passType == ShaderPassType.Selection)
                    AppendLine("#define SCENE_VIEW_OUTLINE_MASK");
            }

            AppendLine($"#include \"../../Core/{shaderName} Core.cginc\"");
            indentLevel--;
            AppendLine("ENDHLSL");
        }


        private string GetIndentation()
        {
            return new string(INDENT_STR, indentLevel);
        }

        private void AppendLine(string s)
        {
            shader += $"{GetIndentation()}{s}\n";
        }

        private void AppendLines(IEnumerable<string> strings)
        {
            foreach (var s in strings)
                AppendLine(s);
        }

        private void BeginScope(string line)
        {
            AppendLine(line);
            indentLevel++;
        }

        private void EndScope()
        {
            indentLevel--;
            AppendLine("}");
        }

        public struct BracketScope : IDisposable
        {
            private readonly ShaderBuilder builder;

            public BracketScope(ShaderBuilder builder, string line)
            {
                this.builder = builder;
                builder.AppendLine(line + " {");
                builder.indentLevel++;
            }

            public void Dispose()
            {
                builder.indentLevel--;
                builder.AppendLine("}");
            }
        }
    }
}