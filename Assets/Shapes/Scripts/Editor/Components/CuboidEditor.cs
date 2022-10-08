using UnityEditor;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Cuboid))]
    [CanEditMultipleObjects]
    public class CuboidEditor : ShapeRendererEditor
    {
        private readonly SerializedProperty propSize = null;
        private readonly SerializedProperty propSizeSpace = null;

        public override void OnInspectorGUI()
        {
            BeginProperties();
            ShapesUI.FloatInSpaceField(propSize, propSizeSpace);
            EndProperties();
        }
    }
}