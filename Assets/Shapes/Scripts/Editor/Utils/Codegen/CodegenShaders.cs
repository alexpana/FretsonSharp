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
    public struct MultiCompile
    {
        private readonly string[] keywords;
        public int Count => keywords.Length + 1; // include 0th;
        public string this[int i] => i == 0 ? "" : keywords[i - 1];

        public MultiCompile(params string[] keywords)
        {
            this.keywords = keywords;
        }

        public override string ToString()
        {
            return $"#pragma multi_compile __ {string.Join(" ", keywords)}";
        }

        public IEnumerable<string> Enumerate()
        {
            for (var i = 0; i < Count; i++)
                yield return this[i];
        }
    }

    public static class CodegenShaders
    {
        private static readonly int blendModeCount = Enum.GetNames(typeof(ShapesBlendMode)).Length;

        internal static void GenerateShadersAndMaterials(RenderPipeline targetRP)
        {
            Debug.Log($"Regenerating shaders for {targetRP.PrettyName()}");

            // check if we need to update rp state
            var writeTargetRpToImportState = ShapesImportState.Instance.currentShaderRP != targetRP;

            // generate all shader paths & content
            var shaderPathContents = GetShaderPathContents();
            var pathMaterials = GetMaterialPathContents();

            // tally up all files we need to edit to make version control happy
            var filesToUnlock = new List<string>();
            if (writeTargetRpToImportState) filesToUnlock.Add(AssetDatabase.GetAssetPath(ShapesImportState.Instance));
            filesToUnlock.AddRange(shaderPathContents.Select(x => x.path));
            filesToUnlock.AddRange(pathMaterials.Where(x => x.mat != null).Select(x => x.path));

            // try to make sure they can be edited, then write all data
            if (ShapesIO.TryMakeAssetsEditable(filesToUnlock.ToArray()))
            {
                shaderPathContents.ForEach(pc => File.WriteAllText(pc.path, pc.content)); // write all shaders
                AssetDatabase.Refresh(ImportAssetOptions
                    .Default); // reimport all assets to load newly generated shaders
                pathMaterials.ForEach(x => x.GenerateOrUpdate()); // generate all materials
                if (writeTargetRpToImportState)
                {
                    // update the current shader state
                    ShapesImportState.Instance.currentShaderRP = targetRP;
                    EditorUtility.SetDirty(ShapesImportState.Instance);
                }

                AssetDatabase.Refresh(ImportAssetOptions.Default); // reimport stuff
            }
        }

        private static List<PathContent> GetShaderPathContents()
        {
            var rp = UnityInfo.GetCurrentRenderPipelineInUse();
            var pcs = new List<PathContent>();
            foreach (var name in StaticInit.shaderNames)
                for (var i = 0; i < blendModeCount; i++)
                {
                    var blendMode = (ShapesBlendMode)i;
                    var path = $"{ShapesIO.GeneratedShadersFolder}/{name} {blendMode}.shader";
                    var shaderContents = new ShaderBuilder(name, blendMode, rp).shader;
                    pcs.Add(new PathContent(path, shaderContents));
                }

            return pcs;
        }

        private static List<PathMaterial> GetMaterialPathContents()
        {
            var pathMaterials = new List<PathMaterial>();
            foreach (var name in StaticInit.shaderNames)
                for (var i = 0; i < blendModeCount; i++)
                {
                    var blendMode = (ShapesBlendMode)i;
                    var nameWithBlendMode = ShapesMaterials.GetMaterialName(name, blendMode.ToString());
                    var shader = Shader.Find($"Shapes/{nameWithBlendMode}");
                    if (shader == null)
                    {
                        Debug.LogError("missing shader " + $"Shapes/{nameWithBlendMode}");
                        continue;
                    }

                    if (ShaderBuilder.shaderKeywords.ContainsKey(name))
                    {
                        // create all permutations
                        var multis = ShaderBuilder.shaderKeywords[name];
                        var keywordPermutations = new List<string>();
                        foreach (var perm in GetPermutations(multis.Select(m => m.Enumerate())))
                        {
                            var validKeywords = perm.Where(p => string.IsNullOrEmpty(p) == false).ToArray();
                            var kws = $" [{string.Join("][", validKeywords)}]";
                            if (kws.Contains("[]")) // this means it has no permutations
                                kws = "";
                            pathMaterials.Add(GetPathMaterial(nameWithBlendMode + kws, shader, validKeywords));
                        }
                    }
                    else
                    {
                        pathMaterials.Add(GetPathMaterial(nameWithBlendMode, shader));
                    }
                }

            return pathMaterials;
        }

        private static PathMaterial GetPathMaterial(string fullMaterialName, Shader shader, string[] keywords = null)
        {
            var savePath = $"{ShapesIO.GeneratedMaterialsFolder}/{fullMaterialName}.mat";
            var mat = AssetDatabase.LoadAssetAtPath<Material>(savePath);
            return new PathMaterial(savePath, mat, shader, keywords);
        }

        private static void TrySetKeywordsAndDefaultProperties(IEnumerable<string> keywords, Material mat)
        {
            if (keywords != null)
                foreach (var keyword in keywords)
                    mat.EnableKeyword(keyword);

            ShapesMaterials.ApplyDefaultGlobalProperties(mat);
        }

        // magic wand wau ✨
        public static IEnumerable<IEnumerable<string>> GetPermutations(IEnumerable<IEnumerable<string>> inputData)
        {
            IEnumerable<IEnumerable<string>> emptyProduct = new[] { Enumerable.Empty<string>() };
            return inputData.Aggregate(emptyProduct,
                (accumulator, sequence) =>
                    accumulator.SelectMany(accseq => sequence, (accseq, item) => accseq.Concat(new[] { item })));
        }

        internal static class StaticInit
        {
            public static string[] shaderNames = ShapesIO.LoadCoreShaders()
                .Select(x => x.name.Substring(0, x.name.Length - " Core".Length)).ToArray();
        }

        private class PathContent
        {
            public readonly string content;
            public readonly string path;

            public PathContent(string path, string content)
            {
                (this.path, this.content) = (path, content);
            }
        }

        private class PathMaterial
        {
            public readonly string[] keywords;
            public Material mat;
            public readonly string path;
            public readonly Shader shader;

            public PathMaterial(string path, Material mat, Shader shader, string[] keywords)
            {
                (this.path, this.mat, this.shader, this.keywords) = (path, mat, shader, keywords);
            }

            public void GenerateOrUpdate()
            {
                if (mat != null)
                {
                    EditorUtility.SetDirty(mat);
                    mat.shader = shader;
                    mat.hideFlags = HideFlags.HideInInspector;
                    TrySetKeywordsAndDefaultProperties(keywords, mat);
                }
                else
                {
                    Debug.Log("creating material " + path);
                    mat = new Material(shader) { enableInstancing = true, hideFlags = HideFlags.HideInInspector };
                    TrySetKeywordsAndDefaultProperties(keywords, mat);
                    AssetDatabase.CreateAsset(mat, path);
                }
            }
        }
    }
}