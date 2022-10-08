using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Hole
{
    public interface IHole
    {
        HoleInfo Hole { get; }
    }

    public class UIHole : UIDataMesh, IUnitSize, IHole
    {
        public HoleInfo hole;

        private IMeshDrawer m_holeDrawer;

        protected override IEnumerable<IMesh> DrawerFactory
        {
            get
            {
                var h = m_holeDrawer ?? (m_holeDrawer = new HoleDrawer(this, this));
                return h.Draw();
            }
        }

        protected override void Start()
        {
            hole.editCallback += GeometyUpdateFlagUp;
        }

        public HoleInfo Hole => hole;


        public Vector2 Size => new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }
}