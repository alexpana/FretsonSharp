using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.DataRenderer2D
{
    public abstract class UIDataMesh : Image, IMeshModifier
    {
        private IEnumerable<IMesh> _mesh;
        private bool m_geometryUpdateFlag = false;

        private IEnumerable<IMesh> Mesh => _mesh ?? (_mesh = DrawerFactory);

        protected abstract IEnumerable<IMesh> DrawerFactory { get; }

        protected override void Awake()
        {
            base.Awake();
        }

        public void LateUpdate()
        {
            if (m_geometryUpdateFlag)
            {
                UpdateGeometry();
                m_geometryUpdateFlag = false;
            }
        }

        public void ModifyMesh(Mesh mesh)
        {
            using (var vh = new VertexHelper(mesh))
            {
                ModifyMesh(vh);
                vh.FillMesh(mesh);
            }
        }

        public void ModifyMesh(VertexHelper verts)
        {
            verts.Clear();

            var buffer = new Queue<int>();

            foreach (var m in Mesh)
            {
                foreach (var t in m.Triangles)
                {
                    buffer.Enqueue(t);
                    if (buffer.Count == 3)
                    {
                        var c = verts.currentVertCount;
                        verts.AddTriangle(c + buffer.Dequeue(), c + buffer.Dequeue(), c + buffer.Dequeue());
                    }
                }

                foreach (var v in m.Vertices)
                    verts.AddVert(v.position, v.color, v.uv);
            }
        }

        public void GeometyUpdateFlagUp()
        {
            m_geometryUpdateFlag = true;
        }
    }
}