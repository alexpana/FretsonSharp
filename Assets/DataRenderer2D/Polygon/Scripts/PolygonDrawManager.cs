using System.Collections.Generic;

namespace geniikw.DataRenderer2D.Polygon
{
    public class PolygonDrawerManager
    {
        private readonly IMeshDrawer _hole;
        private readonly IMeshDrawer _normal;
        private readonly IPolygon _target;

        public PolygonDrawerManager(IPolygon target,
            IMeshDrawer normal, IMeshDrawer hole)
        {
            _target = target;
            _normal = normal;
            _hole = hole;
        }

        public IEnumerable<IMesh> Draw()
        {
            var polyGon = _target.Polygon;

            if (polyGon.count < 3)
                yield break;

            //var cc = polyGon.startAngle == 0 && polyGon.endAngle == 1;

            if (polyGon.type == PolygonType.ZigZag)
                foreach (var m in _normal.Draw())
                    yield return m;

            else if (polyGon.type >= PolygonType.Hole)
                foreach (var m in _hole.Draw())
                    yield return m;
        }
    }
}