using System;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    /// <summary>A helper component to tint shape component children</summary>
    [ExecuteAlways]
    public class ShapeGroup : MonoBehaviour
    {
        /// <summary>The number of ShapeGroup components active in the scenet</summary>
        public static int shapeGroupsInScene = 0;

        [ShapesColorField(true)] [SerializeField]
        private Color color = Color.white;

        // this is because in OnDisable, this component reads as still being enabled by the child shapes
        // so, we've got an additional lil noot to make sure things do a correct upon the things
        [field: NonSerialized] internal bool IsEnabled { get; private set; } = false;

        /// <summary>The color tint of all shapes in this group</summary>
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                UpdateChildShapes();
            }
        }

        private void OnEnable()
        {
            shapeGroupsInScene++;
            IsEnabled = true;
            UpdateChildShapes();
        }

        private void OnDisable()
        {
            shapeGroupsInScene--;
            IsEnabled = false;
            UpdateChildShapes();
        }

        private void OnValidate()
        {
            UpdateChildShapes();
        }

        private void UpdateChildShapes()
        {
            var shapes = GetComponentsInChildren<ShapeRenderer>();
            if (shapes != null)
                foreach (var shape in shapes)
                    shape.UpdateAllMaterialProperties();
        }
    }
}