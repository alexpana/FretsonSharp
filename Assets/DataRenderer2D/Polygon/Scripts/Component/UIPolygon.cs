using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Polygon
{
    public class UIPolygon : UIDataMesh, IPolygon, IUnitSize
    {
        public PolygonData data;

        protected override IEnumerable<IMesh> DrawerFactory => PolygonFactory.Create(this, this);

        public PolygonData Polygon => data;

        public Vector2 Size => new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }
}