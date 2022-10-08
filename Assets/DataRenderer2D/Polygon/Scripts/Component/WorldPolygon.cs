using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Polygon
{
    public class WorldPolygon : WorldDataMesh, IPolygon, IUnitSize
    {
        public PolygonData data;

        protected override IEnumerable<IMesh> MeshFactory => PolygonFactory.Create(this, this);

        public PolygonData Polygon => data;

        public Vector2 Size => Vector2.one;
    }
}