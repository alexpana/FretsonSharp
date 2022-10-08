using System;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class WorldDataMesh : MonoBehaviour
    {
        public bool updateInUpdate = true;

        private IEnumerable<IMesh> _mesh;

        private int bufferSize = 100;
        private Color[] colorBuffer;

        private bool m_geometryUpdateFlag = false;

        private MeshFilter m_mf;
        private int[] tBuffer;
        private Vector2[] uvBuffer;
        private Vector3[] vBuffer;

        private IEnumerable<IMesh> Mesh => _mesh ?? (_mesh = MeshFactory);

        protected abstract IEnumerable<IMesh> MeshFactory { get; }

        private MeshFilter Mf => m_mf ?? (m_mf = GetComponent<MeshFilter>());

        protected virtual void Awake()
        {
            AllocateBuffer(bufferSize);
        }

        public void Reset()
        {
            var mf = GetComponent<MeshFilter>();

            if (mf.sharedMesh == null) MakeNewMesh();
        }

        private void Update()
        {
            m_geometryUpdateFlag = true;
        }

        public void LateUpdate()
        {
            if (m_geometryUpdateFlag)
            {
                UpdateGeometry();
                m_geometryUpdateFlag = false;
            }
        }

        public void MakeNewMesh()
        {
            var mf = GetComponent<MeshFilter>();

            var mesh = new Mesh();
            mesh.name = name;
            mf.mesh = mesh;
        }


        public void UpdateGeometry()
        {
            var mf = GetComponent<MeshFilter>();

            mf.sharedMesh.Clear();

            var vc = 0;
            var tc = 0;

            foreach (var mesh in Mesh)
            {
                foreach (var t in mesh.Triangles)
                {
                    if (tc > tBuffer.Length - 1)
                        ExtendBuffer();

                    tBuffer[tc] = vc + t;
                    tc++;
                }

                foreach (var v in mesh.Vertices)
                {
                    if (vc > vBuffer.Length - 1)
                        ExtendBuffer();

                    vBuffer[vc] = v.position;
                    uvBuffer[vc] = v.uv;
                    colorBuffer[vc] = v.color;
                    vc++;
                }
            }

            while (vc < vBuffer.Length)
            {
                vBuffer[vc] = Vector3.zero;
                uvBuffer[vc] = Vector2.zero;
                colorBuffer[vc] = Color.white;
                vc++;
            }

            while (tc < vBuffer.Length)
            {
                tBuffer[tc] = 0;
                tc++;
            }

            mf.sharedMesh.vertices = vBuffer;

            mf.sharedMesh.uv = uvBuffer;
            mf.sharedMesh.triangles = tBuffer;
            mf.sharedMesh.colors = colorBuffer;
            mf.sharedMesh.RecalculateNormals();
            mf.sharedMesh.RecalculateTangents();
            mf.sharedMesh.RecalculateBounds();
        }

        private void AllocateBuffer(int size)
        {
            vBuffer = new Vector3[size];
            uvBuffer = new Vector2[size];
            colorBuffer = new Color[size];
            tBuffer = new int[size * 6];
        }

        private void ExtendBuffer()
        {
            var tempV = vBuffer;
            var tempUV = uvBuffer;
            var tempC = colorBuffer;
            var tempT = tBuffer;
            //Debug.Log("Size double up : " + bufferSize);
            bufferSize *= 2;
            AllocateBuffer(bufferSize);

            Array.Copy(tempV, vBuffer, tempV.Length);
            Array.Copy(tempUV, uvBuffer, tempUV.Length);
            Array.Copy(tempC, colorBuffer, tempC.Length);
            Array.Copy(tempT, tBuffer, tempT.Length);
        }


        public void GeometyUpdateFlagUp()
        {
            m_geometryUpdateFlag = true;
        }
    }
}