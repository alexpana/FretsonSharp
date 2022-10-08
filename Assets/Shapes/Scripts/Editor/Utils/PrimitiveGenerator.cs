using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    public static class PrimitiveGenerator
    {
        private const float INV_SQRT3 = 0.577350269189f;

        // Icosahedron base topology, vertex radius 1
        public static readonly BasicMeshData Icosahedron = new()
        {
            tris = new List<int>
            {
                0, 1, 2, 2, 6, 0, 0, 6, 5, 5, 7, 0, 0, 7, 1, 1, 7, 3, 3, 8, 1, 1, 8, 2, 2, 8, 4, 2, 4, 6, 6, 4, 10, 6,
                10, 5, 5, 10, 11, 5, 11, 7, 7, 11, 3, 9, 10, 4, 4, 8, 9, 9, 8, 3, 3, 11, 9, 9, 11, 10
            },
            verts = new List<Vector3>
            {
                new(0, -0.5257311f, -0.8506508f), new(-0.5257311f, -0.8506508f, 0), new(-0.8506508f, 0, -0.5257311f),
                new(0, -0.5257311f, 0.8506508f), new(-0.5257311f, 0.8506508f, 0), new(0.8506508f, 0, -0.5257311f),
                new(0, 0.5257311f, -0.8506508f), new(0.5257311f, -0.8506508f, 0), new(-0.8506508f, 0, 0.5257311f),
                new(0, 0.5257311f, 0.8506508f), new(0.5257311f, 0.8506508f, 0), new(0.8506508f, 0, 0.5257311f)
            }
        };

        public static readonly BasicMeshData Triangle = new()
        {
            tris = new List<int> { 0, 1, 2 },
            verts = new List<Vector3> { new(1, 0, 0), new(0, 1, 0), new(0, 0, 1) },
            normals = new List<Vector3>
            {
                Vector3.one.normalized, Vector3.one.normalized, Vector3.one.normalized
            } // normals mostly required to suppress warnings
        };

        public static readonly BasicMeshData Quad = new()
        {
            verts = new List<Vector3> { new(-1, -1), new(-1, 1), new(1, 1), new(1, -1) },
            normals = new List<Vector3> { new(0, 0, -1), new(0, 0, -1), new(0, 0, -1), new(0, 0, -1) },
            uvs = new List<Vector2> { new(-1, -1), new(-1, 1), new(1, 1), new(1, -1) },
            colors = new List<Color> { new(1, 0, 0, 0), new(0, 1, 0, 0), new(0, 0, 1, 0), new(0, 0, 0, 1) },
            tris = new List<int> { 0, 1, 2, 0, 2, 3 }
        };

        public static readonly BasicMeshData Cube = new()
        {
            verts = new List<Vector3>
            {
                new(-1, -1, -1), new(-1, -1, 1), new(-1, 1, -1), new(-1, 1, 1), new(1, -1, -1), new(1, -1, 1),
                new(1, 1, -1), new(1, 1, 1)
            },
            tris = new List<int>
            {
                0, 1, 2, 2, 1, 3, 3, 1, 7, 7, 1, 5, 5, 4, 7, 7, 4, 6, 6, 4, 2, 2, 4, 0, 0, 4, 5, 5, 1, 0, 7, 6, 2, 2, 3,
                7
            },
            normals = new List<Vector3>
            {
                new(-INV_SQRT3, -INV_SQRT3, -INV_SQRT3), new(-INV_SQRT3, -INV_SQRT3, INV_SQRT3),
                new(-INV_SQRT3, INV_SQRT3, -INV_SQRT3), new(-INV_SQRT3, INV_SQRT3, INV_SQRT3),
                new(INV_SQRT3, -INV_SQRT3, -INV_SQRT3), new(INV_SQRT3, -INV_SQRT3, INV_SQRT3),
                new(INV_SQRT3, INV_SQRT3, -INV_SQRT3), new(INV_SQRT3, INV_SQRT3, INV_SQRT3)
            }
        };

        public static void Generate3DPrimitiveAssets()
        {
            var config = ShapesConfig.Instance;

            // delete all
            // ShapesAssets assets = ShapesAssets.Instance;
            // string assetPath = AssetDatabase.GetAssetPath( assets );
            // Mesh[] assetsAtPath = AssetDatabase.LoadAllAssetsAtPath( assetPath ).OfType<Mesh>().ToArray();
            // for( int i = 0; i < assetsAtPath.Length; i++ ) {
            // 	GameObject.DestroyImmediate( assetsAtPath[i], true );
            // }
            // return;

            EditorUtility.SetDirty(ShapesAssets.Instance);
            UpdatePrimitiveMesh("quad", 0, config.boundsSizeQuad, Quad, ref ShapesAssets.Instance.meshQuad, 1);
            UpdatePrimitiveMesh("triangle", 0, config.boundsSizeTriangle, Triangle,
                ref ShapesAssets.Instance.meshTriangle, 1);
            UpdatePrimitiveMesh("cube", 0, config.boundsSizeCuboid, Cube, ref ShapesAssets.Instance.meshCube, 1);
            for (var d = 0; d < 5; d++)
            {
                UpdatePrimitiveMesh("sphere", d, config.boundsSizeSphere, GenerateIcosphere(config.sphereDetail[d]),
                    ref ShapesAssets.Instance.meshSphere);
                UpdatePrimitiveMesh("capsule", d, config.boundsSizeCapsule, GenerateCapsule(config.capsuleDivs[d]),
                    ref ShapesAssets.Instance.meshCapsule);
                UpdatePrimitiveMesh("cylinder", d, config.boundsSizeCylinder, GenerateCylinder(config.cylinderDivs[d]),
                    ref ShapesAssets.Instance.meshCylinder);
                UpdatePrimitiveMesh("cone", d, config.boundsSizeCone, GenerateCone(config.coneDivs[d], true),
                    ref ShapesAssets.Instance.meshCone);
                UpdatePrimitiveMesh("cone_uncapped", d, config.boundsSizeCone, GenerateCone(config.coneDivs[d], true),
                    ref ShapesAssets.Instance.meshConeUncapped);
                UpdatePrimitiveMesh("torus", d, config.boundsSizeTorus,
                    GenerateTorus(config.torusDivsMinorMajor[d].x, config.torusDivsMinorMajor[d].y, 1f, 2f),
                    ref ShapesAssets.Instance.meshTorus);
            }

            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(ShapesAssets.Instance));
            Debug.Log("Shapes primitives regenerated");
        }

        private static Mesh UpdatePrimitiveMesh(string primitive, int detail, float boundsSize, BasicMeshData meshData,
            ref Mesh[] meshArray, int detailLevelCount = 5)
        {
            // make sure array is prepared
            if (meshArray == null || meshArray.Length != detailLevelCount)
            {
                // array incorrectly set up
                meshArray = new Mesh[detailLevelCount];
                Debug.Log($"reinitialized {primitive} mesh array to {detailLevelCount}");
            }

            // field is missing a ref
            if (meshArray[detail] == null)
            {
                var meshName = $"{primitive}_{detail}";
                // find existing mesh
                var assetPath = AssetDatabase.GetAssetPath(ShapesAssets.Instance);
                var assetsAtPath = AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Mesh>().ToArray();
                var existingMesh = assetsAtPath.FirstOrDefault(x => x.name == meshName);
                if (existingMesh != null)
                {
                    // assign existing mesh
                    Debug.Log("Assigning missing mesh ref " + meshName);
                    meshArray[detail] = existingMesh;
                }
                else
                {
                    // create it if it's not found
                    Debug.Log("Creating missing mesh " + meshName);
                    meshArray[detail] = new Mesh { name = meshName };
                    AssetDatabase.AddObjectToAsset(meshArray[detail], ShapesAssets.Instance);
                }
            }

            var m = meshArray[detail];
            meshData.ApplyTo(m);
            m.bounds = new Bounds(Vector3.zero, Vector3.one * boundsSize);
            meshArray[detail] = m;
            return m;
        }

        public static int TriangleCountCapsule(int n)
        {
            return 8 * (n + n * n);
        }

        public static BasicMeshData GenerateCapsule(int divs)
        {
            var mesh = new BasicMeshData();
            mesh.normals = new List<Vector3>();

            var sides = divs * 4;

            for (var z = 0; z < 2; z++)
            for (var i = 0; i < sides; i++)
            {
                var t = i / (float)sides;
                Vector3 v = ShapesMath.AngToDir(t * ShapesMath.TAU);
                mesh.normals.Add(v);
                v.z = z;
                mesh.verts.Add(v);
            }

            // sides
            for (var i = 0; i < sides; i++)
            {
                var low0 = i;
                var top0 = sides + i;
                var low1 = (i + 1) % sides;
                var top1 = sides + (i + 1) % sides;
                mesh.tris.Add(low0);
                mesh.tris.Add(low1);
                mesh.tris.Add(top1);
                mesh.tris.Add(top1);
                mesh.tris.Add(top0);
                mesh.tris.Add(low0);
            }

            // round caps!
            var n = divs + 1;
            Vector3[] octaBaseVerts = { Vector3.right, Vector3.up, Vector3.left, Vector3.down };
            for (var z = 0; z < 2; z++) // half-octahedron
            for (var s = 0; s < 4; s++)
            {
                var v0 = z == 0 ? Vector3.back : Vector3.forward; // reverse depending on z
                var v1 = octaBaseVerts[s];
                var v2 = octaBaseVerts[(s + 1) % 4];

                var verts = BarycentricVertices(n, v0, v1, v2).ToArray();
                mesh.normals.AddRange(verts);
                if (z == 0)
                {
                    mesh.tris.AddRange(BarycentricTriangulation(n, mesh.verts.Count).Reverse());
                    mesh.verts.AddRange(verts.Select(x => x));
                }
                else
                {
                    mesh.tris.AddRange(BarycentricTriangulation(n, mesh.verts.Count));
                    mesh.verts.AddRange(verts.Select(x => x + Vector3.forward));
                }
            }

            mesh.RemoveDuplicateVertices();

            return mesh;
        }

        public static int TriangleCountCylinder(int divs)
        {
            return (divs - 1) * 4;
        }

        public static BasicMeshData GenerateCylinder(int divs)
        {
            var mesh = new BasicMeshData();
            mesh.normals = new List<Vector3>();

            for (var z = 0; z < 2; z++)
            for (var i = 0; i < divs; i++)
            {
                var t = i / (float)divs;
                Vector3 v = ShapesMath.AngToDir(t * ShapesMath.TAU);
                mesh.normals.Add(v);
                v.z = z;
                mesh.verts.Add(v);
            }

            // sides
            for (var i = 0; i < divs; i++)
            {
                var low0 = i;
                var top0 = divs + i;
                var low1 = (i + 1) % divs;
                var top1 = divs + (i + 1) % divs;
                mesh.tris.Add(low0);
                mesh.tris.Add(low1);
                mesh.tris.Add(top1);
                mesh.tris.Add(top1);
                mesh.tris.Add(top0);
                mesh.tris.Add(low0);
            }

            // cap bottom
            for (var i = 1; i < divs - 1; i++)
            {
                mesh.tris.Add(0);
                mesh.tris.Add((i + 1) % divs);
                mesh.tris.Add(i);
            }

            // cap top
            for (var i = 1; i < divs - 1; i++)
            {
                mesh.tris.Add(divs + 0);
                mesh.tris.Add(divs + i);
                mesh.tris.Add(divs + (i + 1) % divs);
            }

            return mesh;
        }

        public static int TriangleCountCone(int divs)
        {
            return (divs - 1) * 2;
        }

        public static BasicMeshData GenerateCone(int divs, bool generateCap)
        {
            var mesh = new BasicMeshData();

            mesh.verts.Add(Vector3.forward);
            for (var i = 1; i < divs + 1; i++)
            {
                var t = i / (float)divs;
                var iNext = i == divs ? 1 : i + 1;
                mesh.verts.Add(ShapesMath.AngToDir(t * ShapesMath.TAU));
                mesh.tris.Add(0); // vertex 0 is the tip
                mesh.tris.Add(i);
                mesh.tris.Add(iNext);

                if (generateCap && i > 1 && i < divs)
                {
                    mesh.tris.Add(1); // vertex 1 is the root edge vert
                    mesh.tris.Add(iNext);
                    mesh.tris.Add(i);
                }
            }

            mesh.normals = mesh.verts.Select(v => v).ToList(); // already normalized

            return mesh;
        }

        public static int TriangleCountIcosphere(int divs)
        {
            return divs * divs * 20;
        }

        public static BasicMeshData GenerateIcosphere(int divs)
        {
            var mesh = new BasicMeshData();
            mesh.normals = new List<Vector3>();

            // foreach face, generate all vertices and triangles
            for (var i = 0; i < 20; i++)
            {
                var v0 = Icosahedron.verts[Icosahedron.tris[i * 3]];
                var v1 = Icosahedron.verts[Icosahedron.tris[i * 3 + 1]];
                var v2 = Icosahedron.verts[Icosahedron.tris[i * 3 + 2]];
                // add this icosa face to the global list
                var n = divs + 1; // n is number of verts along one side of the triangle 
                var prevVertCount = mesh.verts.Count;
                var verts = BarycentricVertices(n, v0, v1, v2).ToArray();
                mesh.verts.AddRange(verts);
                mesh.normals.AddRange(verts);
                mesh.tris.AddRange(BarycentricTriangulation(n, prevVertCount));
            }

            // cleanup duplicate verts
            mesh.RemoveDuplicateVertices();

            return mesh;
        }


        private static IEnumerable<Vector3> BarycentricVertices(int n, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            for (var iy = 0; iy < n; iy++)
            {
                var ty = iy / (n - 1f);
                for (var ix = 0; ix < n - iy; ix++)
                {
                    var tx = iy == n - 1 ? 0f : ix / (n - iy - 1f);
                    var t = new Vector3((1f - ty) * (1f - tx), ty,
                        (1f - ty) * tx); // equivalent to the triple lerp method
                    yield return SphericalBarycentricInterpolationEquilateral(v0, v1, v2, t);
                }
            }
        }

        private static IEnumerable<int> BarycentricTriangulation(int n, int globalOffset)
        {
            var rootIndex = 0; // first index on each row
            for (var iy = 0; iy < n; iy++)
            {
                var rootNext = rootIndex + n - iy;
                var xDotCount = n - iy;
                for (var ix = 0; ix < xDotCount - 1; ix++)
                {
                    // foreach dot in a row (but one less)
                    yield return globalOffset + ix + rootIndex + 1;
                    yield return globalOffset + ix + rootIndex;
                    yield return globalOffset + ix + rootNext;
                    if (ix < xDotCount - 2)
                    {
                        yield return globalOffset + ix + rootNext;
                        yield return globalOffset + ix + rootNext + 1;
                        yield return globalOffset + ix + rootIndex + 1;
                    }
                }

                rootIndex = rootNext;
            }
        }

        public static BasicMeshData GenerateUVSphere(int divsLong, int divsLat)
        {
            var mesh = new BasicMeshData();
            mesh.normals = new List<Vector3>();

            var vertCount = divsLong * divsLat;
            var triCount = divsLong * (divsLat - 1) * 2 - divsLong * 2; // subtracting is to remove quads at the pole
            var verts = new Vector3[vertCount];
            var iVert = 0;

            // generate verts
            for (var iLo = 0; iLo < divsLong; iLo++)
            {
                var tLong = iLo / (float)divsLong;
                var angLong = tLong * ShapesMath.TAU;
                var dirXZ = ShapesMath.AngToDir(angLong);
                var dirLong = new Vector3(dirXZ.x, 0f, dirXZ.y);
                for (var iLa = 0; iLa < divsLat; iLa++)
                {
                    var tLat = iLa / (divsLat - 1f);
                    var angLat = Mathf.Lerp(-0.25f, 0.25f, tLat) * ShapesMath.TAU;
                    var dirProj = ShapesMath.AngToDir(angLat);
                    verts[iVert++] = dirLong * dirProj.x + Vector3.up * dirProj.y;
                }
            }

            // generate tris
            var tris = new int[triCount * 3];
            var iTri = 0;
            for (var iLo = 0; iLo < divsLong; iLo++)
            for (var iLa = 0; iLa < divsLat - 1; iLa++)
            {
                var iRoot = iLo * divsLat + iLa;
                var iRootNext = (iRoot + divsLat) % vertCount;
                if (iLa < divsLat - 2)
                {
                    // skip first and last (triangles at the poles)
                    tris[iTri++] = iRoot;
                    tris[iTri++] = iRoot + 1;
                    tris[iTri++] = iRootNext + 1;
                }

                if (iLa > 0)
                {
                    tris[iTri++] = iRootNext + 1;
                    tris[iTri++] = iRootNext;
                    tris[iTri++] = iRoot;
                }
            }

            mesh.verts.AddRange(verts);
            mesh.tris.AddRange(tris);
            mesh.normals.AddRange(verts);
            mesh.RemoveDuplicateVertices();

            return mesh;
        }

        public static int TriangleCountTorus(Vector2Int divsMinMaj)
        {
            return divsMinMaj.x * divsMinMaj.y * 2;
        }

        public static BasicMeshData GenerateTorus(int divsMinor, int divsMajor, float rMinor = 1, float rMajor = 1)
        {
            var mesh = new BasicMeshData();
            mesh.normals = new List<Vector3>();
            for (var iMaj = 0; iMaj < divsMajor; iMaj++)
            {
                var tMaj = iMaj / (float)divsMajor;
                var dirMaj = ShapesMath.AngToDir(tMaj * ShapesMath.TAU);
                for (var iMin = 0; iMin < divsMinor; iMin++)
                {
                    var tMin = iMin / (float)divsMinor;
                    var dirMinLocal = ShapesMath.AngToDir(tMin * ShapesMath.TAU);
                    var dirMin = (Vector3)dirMaj * dirMinLocal.x + new Vector3(0, 0, dirMinLocal.y);
                    mesh.normals.Add(dirMin);
                    mesh.verts.Add((Vector3)dirMaj * rMajor + dirMin * rMinor);
                    var maj0min0 = iMaj * divsMinor + iMin;
                    var maj1min0 = (iMaj + 1) % divsMajor * divsMinor + iMin;
                    var maj0min1 = iMaj * divsMinor + (iMin + 1) % divsMinor;
                    var maj1min1 = (iMaj + 1) % divsMajor * divsMinor + (iMin + 1) % divsMinor;
                    mesh.tris.Add(maj0min1);
                    mesh.tris.Add(maj0min0);
                    mesh.tris.Add(maj1min1);
                    mesh.tris.Add(maj1min0);
                    mesh.tris.Add(maj1min1);
                    mesh.tris.Add(maj0min0);
                }
            }

            return mesh;
        }


        private static float AngBetweenNormalizedVectors(Vector3 a, Vector3 b)
        {
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(a, b), -1f, 1f));
        }

        private static (float a, float b, float c) GetUnitSphereTriangleEdgeLengths(Vector3 a, Vector3 b, Vector3 c)
        {
            return (
                AngBetweenNormalizedVectors(b, c),
                AngBetweenNormalizedVectors(c, a),
                AngBetweenNormalizedVectors(a, b)
            );
        }


        private static float GetUnitSphereTriangleArea(Vector3 A, Vector3 B, Vector3 C)
        {
            var (a, b, c) = GetUnitSphereTriangleEdgeLengths(A, B, C); // equivalent to arc lengths
            var s = (a + b + c) / 2;
            return 4 * Mathf.Atan(Mathf.Sqrt(Mathf.Tan(s / 2) * Mathf.Tan((s - a) / 2) * Mathf.Tan((s - b) / 2) *
                                             Mathf.Tan((s - c) / 2)));
        }


        // https://math.stackexchange.com/questions/1151428/point-within-a-spherical-triangle-given-areas
        public static Vector3 SphericalBarycentricInterpolationEquilateral(Vector3 a, Vector3 b, Vector3 c,
            Vector3 coord)
        {
            // Presumes equilateral triangle
            var Ω = GetUnitSphereTriangleArea(a, b, c);

            Vector3 GetV3(Func<int, float> noot)
            {
                return new Vector3(noot(0), noot(1), noot(2));
            }

            var t = coord; // area proportion
            var θ = AngBetweenNormalizedVectors(a, b); // length/angle of one side
            var β = 2 * Mathf.Cos(θ) / (1f + Mathf.Cos(θ));
            var λ = GetV3(i => Mathf.Tan(t[i] * Ω / 2) / Mathf.Tan(Ω / 2));
            var v = GetV3(i => λ[i] / (1f + β + (1f - β) * λ[i]));
            return (v[0] * a + v[1] * b + v[2] * c) / (1f - v[0] - v[1] - v[2]);
        }


        // https://math.stackexchange.com/questions/1151428/point-within-a-spherical-triangle-given-areas
        public static Vector3 SphericalBarycentricInterpolation(Vector3 a, Vector3 b, Vector3 c, Vector3 coord)
        {
            var Ω = GetUnitSphereTriangleArea(a, b, c);

            Vector3 GetV3(Func<int, float> f)
            {
                return new Vector3(f(0), f(1), f(2));
            }

            Vector3[] A = { a, b, c }; // points on the sphere
            var t = coord; // area proportion
            var α = GetV3(i => Vector3.Dot(A[(i + 1) % 3], A[(i + 2) % 3]));
            var β = GetV3(i => (α[(i + 1) % 3] + α[(i + 2) % 3]) / (1 + α[i]));
            var λ = GetV3(i => Mathf.Tan(t[i] * Ω / 2) / Mathf.Tan(Ω / 2));
            var v = GetV3(i => λ[i] / (1f + β[i] + (1f - β[i]) * λ[i]));
            var u = GetV3(i => v[i] / (1f - v[0] - v[1] - v[2])); // vertex weights
            return u[0] * a + u[1] * b + u[2] * c;
        }

        public class BasicMeshData
        {
            public List<Color> colors = null; // null = unused
            public List<Vector3> normals = null; // null = unused
            public List<int> tris = new();
            public List<Vector2> uvs = null; // null = unused
            public List<Vector3> verts = new();

            public (Vector3, Vector3, Vector3) GetTriVerts(int triangle)
            {
                return (Icosahedron.verts[Icosahedron.tris[triangle * 3]],
                    Icosahedron.verts[Icosahedron.tris[triangle * 3 + 1]],
                    Icosahedron.verts[Icosahedron.tris[triangle * 3 + 2]]);
            }

            public void ApplyTo(Mesh mesh)
            {
                mesh.Clear();
                mesh.SetVertices(verts);
                if (normals != null) mesh.SetNormals(normals);
                if (uvs != null) mesh.SetUVs(0, uvs);
                if (colors != null) mesh.SetColors(colors);
                mesh.SetTriangles(tris, 0);
            }

            public void RemoveDuplicateVertices()
            {
                // find which vertices are similar to a previous one, and create a mapping to existing vertices
                var fromToMap = new Dictionary<int, int>();
                for (var i = 0; i < verts.Count; i++)
                {
                    if (fromToMap.ContainsKey(i))
                        continue; // skip removed vertices
                    for (var j = i + 1; j < verts.Count; j++)
                        if (Vector3.Distance(verts[i], verts[j]) < 0.0001f) // 10th of a millimeter
                            fromToMap[j] = i; // map new vertex to old similar vertex
                }

                // make all triangle indices point to the old ones
                for (var i = 0; i < tris.Count; i++)
                    if (fromToMap.TryGetValue(tris[i], out var existingVertexId))
                        tris[i] = existingVertexId;

                // remove unused verts
                var unusedVerts = fromToMap.Keys.OrderByDescending(x => x);
                foreach (var removeIndex in unusedVerts)
                {
                    verts.RemoveAt(removeIndex);
                    uvs?.RemoveAt(removeIndex);
                    normals?.RemoveAt(removeIndex);
                    colors?.RemoveAt(removeIndex);
                    for (var tri = 0; tri < tris.Count; tri++)
                        if (tris[tri] > removeIndex)
                            tris[tri]--; // decrease by one
                        else if (tris[tri] == removeIndex)
                            Debug.LogWarning("triangle pointing to deleted vertex :(");
                }

                // Debug.Log( "removed " + fromToMap.Keys.Count() + " verts" );
            }
        }
    }
}