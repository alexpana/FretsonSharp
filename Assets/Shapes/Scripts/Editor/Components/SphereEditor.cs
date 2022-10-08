using UnityEditor;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Sphere))]
    [CanEditMultipleObjects]
    public class SphereEditor : ShapeRendererEditor
    {
        private readonly SerializedProperty propRadius = null;
        private readonly SerializedProperty propRadiusSpace = null;

        public override void OnInspectorGUI()
        {
            BeginProperties();
            ShapesUI.FloatInSpaceField(propRadius, propRadiusSpace);
            EndProperties();
        }
    }
}