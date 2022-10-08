using UnityEditor;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Torus))]
    [CanEditMultipleObjects]
    public class TorusEditor : ShapeRendererEditor
    {
        private readonly SerializedProperty propAngRadiansEnd = null;
        private readonly SerializedProperty propAngRadiansStart = null;
        private readonly SerializedProperty propAngUnitInput = null;

        private readonly SerializedProperty propRadius = null;
        private readonly SerializedProperty propRadiusSpace = null;
        private readonly SerializedProperty propThickness = null;
        private readonly SerializedProperty propThicknessSpace = null;

        public override void OnInspectorGUI()
        {
            BeginProperties();
            ShapesUI.FloatInSpaceField(propRadius, propRadiusSpace);
            ShapesUI.FloatInSpaceField(propThickness, propThicknessSpace);
            DrawAngleProperties();
            EndProperties();
        }

        private void DrawAngleProperties()
        {
            ShapesUI.AngleProperty(propAngRadiansStart, "Angle start", propAngUnitInput, DiscEditor.angLabelLayout);
            ShapesUI.AngleProperty(propAngRadiansEnd, "Angle end", propAngUnitInput, DiscEditor.angLabelLayout);
            ShapesUI.DrawAngleSwitchButtons(propAngUnitInput);
        }
    }
}