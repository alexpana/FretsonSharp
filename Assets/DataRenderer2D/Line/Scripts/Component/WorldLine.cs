using System.Collections.Generic;

namespace geniikw.DataRenderer2D
{
    public class WorldLine : WorldDataMesh, ISpline
    {
        public Spline line;

        public bool useUpdate = false;

        private IMeshDrawer m_builder;

        protected override IEnumerable<IMesh> MeshFactory =>
            (m_builder ?? (m_builder = LineBuilder.Factory.Normal(this, transform))).Draw();

        protected override void Awake()
        {
            base.Awake();
            line.EditCallBack += GeometyUpdateFlagUp;
            line.owner = this;
        }

        Spline ISpline.Line => line;
    }
}