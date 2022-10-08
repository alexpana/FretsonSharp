using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    /// <summary>
    ///     draw mesh in canvas
    /// </summary>
    public class UILine : UIDataMesh, ISpline
    {
        public Spline line;

        private IEnumerable<IMesh> m_Drawer = null;

        protected override IEnumerable<IMesh> DrawerFactory =>
            m_Drawer ?? (m_Drawer = LineBuilder.Factory.Normal(this, transform).Draw());

        protected override void Start()
        {
            base.Start();
            line.owner = this;
            line.EditCallBack += GeometyUpdateFlagUp;
        }

        /// <summary>
        ///     hard copy.
        /// </summary>
        Spline ISpline.Line => line;

        public static UILine CreateLine(Transform parent = null)
        {
            var go = new GameObject("UILine");
            if (parent != null)
                go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;

            var line = go.AddComponent<UILine>();
            line.line = Spline.Default;
            line.Start();
            return line;
        }
    }
}