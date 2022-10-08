using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    internal static class ShapesMeshUtils
    {
        private static Mesh quadMesh;

        private static Mesh triangleMesh;

        private static Mesh sphereMesh;

        private static Mesh cuboidMesh;

        private static Mesh torusMesh;

        private static Mesh coneMesh;

        private static Mesh coneMeshUncapped;

        private static Mesh cylinderMesh;

        private static Mesh capsuleMesh;
        public static Mesh[] QuadMesh => ShapesAssets.Instance.meshQuad;
        public static Mesh[] TriangleMesh => ShapesAssets.Instance.meshTriangle;
        public static Mesh[] SphereMesh => ShapesAssets.Instance.meshSphere;
        public static Mesh[] CuboidMesh => ShapesAssets.Instance.meshCube;
        public static Mesh[] TorusMesh => ShapesAssets.Instance.meshTorus;
        public static Mesh[] ConeMesh => ShapesAssets.Instance.meshCone;
        public static Mesh[] ConeMeshUncapped => ShapesAssets.Instance.meshConeUncapped;
        public static Mesh[] CylinderMesh => ShapesAssets.Instance.meshCylinder;
        public static Mesh[] CapsuleMesh => ShapesAssets.Instance.meshCapsule;


        private static Mesh EnsureValidMeshBounds(Mesh mesh, Bounds bounds)
        {
            mesh.hideFlags = HideFlags.HideInInspector;
            mesh.bounds = bounds;
            return mesh;
        }

        public static Mesh GetLineMesh(LineGeometry geometry, LineEndCap endCaps, DetailLevel detail)
        {
            switch (geometry)
            {
                case LineGeometry.Billboard:
                case LineGeometry.Flat2D:
                    return QuadMesh[0];
                case LineGeometry.Volumetric3D:
                    return endCaps == LineEndCap.Round ? CapsuleMesh[(int)detail] : CylinderMesh[(int)detail];
            }

            return default;
        }

#if UNITY_EDITOR

        static ShapesMeshUtils()
        {
            AssemblyReloadEvents.beforeAssemblyReload += OnPreAssemblyReload;
        }

        private static void OnPreAssemblyReload()
        {
            AssemblyReloadEvents.beforeAssemblyReload -= OnPreAssemblyReload;
            var bfs = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            bool IsDestroyTarget(FieldInfo f)
            {
                return f.GetCustomAttributes(typeof(DestroyOnAssemblyReload), false).Length > 0 &&
                       f.GetValue(null) != null;
            }

            foreach (var field in typeof(ShapesMeshUtils).GetFields(bfs).Where(IsDestroyTarget))
            {
                var obj = (Object)field.GetValue(null);
                Object.DestroyImmediate(obj);
            }
        }

#endif
    }
}