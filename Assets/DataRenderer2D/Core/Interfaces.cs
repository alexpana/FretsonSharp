using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    public interface IUnitSize
    {
        Vector2 Size { get; }
    }

    public interface IMesh
    {
        IEnumerable<Vertex> Vertices { get; }
        IEnumerable<int> Triangles { get; }
    }

    public interface IMeshDrawer
    {
        IEnumerable<IMesh> Draw();
    }
}