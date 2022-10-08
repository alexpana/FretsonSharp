using System.Collections.Generic;

namespace geniikw.DataRenderer2D
{
    public interface IJointBuilder
    {
        IEnumerable<IMesh> Build(Spline.Triple triple);
    }

    public interface IBezierBuilder
    {
        IEnumerable<IMesh> Build(Spline.LinePair pair);
    }

    public interface ICapBuilder
    {
        IEnumerable<IMesh> Build(Spline.LinePair pair, bool isEnd);
    }
}