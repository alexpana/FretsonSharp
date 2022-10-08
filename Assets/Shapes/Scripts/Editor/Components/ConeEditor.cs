using System.Linq;
using UnityEditor;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Cone))]
    [CanEditMultipleObjects]
    public class ConeEditor : ShapeRendererEditor
    {
        private readonly SerializedProperty propFillCap = null;
        private readonly SerializedProperty propLength = null;

        private readonly SerializedProperty propRadius = null;
        private readonly SerializedProperty propSizeSpace = null;

        public override void OnInspectorGUI()
        {
            BeginProperties();
            ShapesUI.FloatInSpaceField(propRadius, propSizeSpace);
            ShapesUI.FloatInSpaceField(propLength, propSizeSpace, false);
            EditorGUILayout.PropertyField(propFillCap);
            if (EndProperties())
                foreach (var cone in targets.Cast<Cone>())
                    cone.UpdateMesh();
        }
    }
}