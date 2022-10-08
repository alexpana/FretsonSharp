using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
#endif

namespace Battlehub.RTHandles
{
    public enum RuntimeHandleAxis
    {
        None,
        X,
        Y,
        Z,
        XY,
        XZ,
        YZ,
        Screen,
        Free
    }


    public static class RuntimeHandles
    {
        public static readonly Color32 XColor = new(187, 70, 45, 255);
        public static readonly Color32 XColorTransparent = new(187, 70, 45, 128);
        public static readonly Color32 YColor = new(139, 206, 74, 255);
        public static readonly Color32 YColorTransparent = new(139, 206, 74, 128);
        public static readonly Color32 ZColor = new(55, 115, 244, 255);
        public static readonly Color32 ZColorTransparent = new(55, 115, 244, 128);
        public static readonly Color32 AltColor = new(192, 192, 192, 224);
        public static readonly Color32 SelectionColor = new(239, 238, 64, 255);
        private static readonly Mesh Arrows;
        private static readonly Mesh SelectionArrowY;
        private static readonly Mesh SelectionArrowX;
        private static readonly Mesh SelectionArrowZ;

        private static readonly Mesh SelectionCube;
        private static readonly Mesh CubeX;
        private static readonly Mesh CubeY;
        private static readonly Mesh CubeZ;
        private static readonly Mesh CubeUniform;

        private static readonly Mesh SceneGizmoSelectedAxis;
        private static readonly Mesh SceneGizmoXAxis;
        private static readonly Mesh SceneGizmoYAxis;
        private static readonly Mesh SceneGizmoZAxis;
        private static readonly Mesh SceneGizmoCube;
        private static readonly Mesh SceneGizmoSelectedCube;
        private static readonly Mesh SceneGizmoQuad;

        private static readonly Material ShapesMaterialZTest;
        private static readonly Material ShapesMaterialZTest2;
        private static readonly Material ShapesMaterialZTest3;
        private static readonly Material ShapesMaterialZTest4;
        private static readonly Material ShapesMaterialZTestOffset;
        private static readonly Material ShapesMaterial;
        private static readonly Material LinesMaterial;
        private static readonly Material LinesClipMaterial;
        private static readonly Material LinesBillboardMaterial;
        private static readonly Material XMaterial;
        private static readonly Material YMaterial;
        private static readonly Material ZMaterial;
        private static readonly Material GridMaterial;

        static RuntimeHandles()
        {
            LinesMaterial = new Material(Shader.Find("Battlehub/RTHandles/VertexColor"));
            LinesMaterial.color = Color.white;

            LinesClipMaterial = new Material(Shader.Find("Battlehub/RTHandles/VertexColorClip"));
            LinesClipMaterial.color = Color.white;

            LinesBillboardMaterial = new Material(Shader.Find("Battlehub/RTHandles/VertexColorBillboard"));
            LinesBillboardMaterial.color = Color.white;

            ShapesMaterial = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterial.color = Color.white;

            ShapesMaterialZTest = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTest.color = new Color(1, 1, 1, 0);
            ShapesMaterialZTest.SetFloat("_ZTest", (float)CompareFunction.LessEqual);
            ShapesMaterialZTest.SetFloat("_ZWrite", 1.0f);

            ShapesMaterialZTestOffset = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTestOffset.color = new Color(1, 1, 1, 1);
            ShapesMaterialZTestOffset.SetFloat("_ZTest", (float)CompareFunction.LessEqual);
            ShapesMaterialZTestOffset.SetFloat("_ZWrite", 1.0f);
            ShapesMaterialZTestOffset.SetFloat("_OFactors", -1.0f);
            ShapesMaterialZTestOffset.SetFloat("_OUnits", -1.0f);

            ShapesMaterialZTest2 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTest2.color = new Color(1, 1, 1, 0);
            ShapesMaterialZTest2.SetFloat("_ZTest", (float)CompareFunction.LessEqual);
            ShapesMaterialZTest2.SetFloat("_ZWrite", 1.0f);

            ShapesMaterialZTest3 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTest3.color = new Color(1, 1, 1, 0);
            ShapesMaterialZTest3.SetFloat("_ZTest", (float)CompareFunction.LessEqual);
            ShapesMaterialZTest3.SetFloat("_ZWrite", 1.0f);

            ShapesMaterialZTest4 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTest4.color = new Color(1, 1, 1, 0);
            ShapesMaterialZTest4.SetFloat("_ZTest", (float)CompareFunction.LessEqual);
            ShapesMaterialZTest4.SetFloat("_ZWrite", 1.0f);

            XMaterial = new Material(Shader.Find("Battlehub/RTHandles/Billboard"));
            XMaterial.color = Color.white;
            XMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.x");
            YMaterial = new Material(Shader.Find("Battlehub/RTHandles/Billboard"));
            YMaterial.color = Color.white;
            YMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.y");
            ZMaterial = new Material(Shader.Find("Battlehub/RTHandles/Billboard"));
            ZMaterial.color = Color.white;
            ZMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.z");

            GridMaterial = new Material(Shader.Find("Battlehub/RTHandles/Grid"));
            GridMaterial.color = Color.white;

            var selectionArrowMesh = CreateConeMesh(SelectionColor);

            var yArrow = new CombineInstance();
            yArrow.mesh = selectionArrowMesh;
            yArrow.transform = Matrix4x4.TRS(Vector3.up, Quaternion.identity, Vector3.one);
            SelectionArrowY = new Mesh();
            SelectionArrowY.CombineMeshes(new[] { yArrow }, true);
            SelectionArrowY.RecalculateNormals();

            var xArrow = new CombineInstance();
            xArrow.mesh = selectionArrowMesh;
            xArrow.transform = Matrix4x4.TRS(Vector3.right, Quaternion.AngleAxis(-90, Vector3.forward), Vector3.one);
            SelectionArrowX = new Mesh();
            SelectionArrowX.CombineMeshes(new[] { xArrow }, true);
            SelectionArrowX.RecalculateNormals();

            var zArrow = new CombineInstance();
            zArrow.mesh = selectionArrowMesh;
            zArrow.transform = Matrix4x4.TRS(Vector3.forward, Quaternion.AngleAxis(90, Vector3.right), Vector3.one);
            SelectionArrowZ = new Mesh();
            SelectionArrowZ.CombineMeshes(new[] { zArrow }, true);
            SelectionArrowZ.RecalculateNormals();

            yArrow.mesh = CreateConeMesh(YColor);
            xArrow.mesh = CreateConeMesh(XColor);
            zArrow.mesh = CreateConeMesh(ZColor);
            Arrows = new Mesh();
            Arrows.CombineMeshes(new[] { yArrow, xArrow, zArrow }, true);
            Arrows.RecalculateNormals();

            SelectionCube = CreateCubeMesh(SelectionColor, 0.1f, 0.1f, 0.1f);
            CubeX = CreateCubeMesh(XColor, 0.1f, 0.1f, 0.1f);
            CubeY = CreateCubeMesh(YColor, 0.1f, 0.1f, 0.1f);
            CubeZ = CreateCubeMesh(ZColor, 0.1f, 0.1f, 0.1f);
            CubeUniform = CreateCubeMesh(AltColor, 0.1f, 0.1f, 0.1f);

            SceneGizmoSelectedAxis = CreateSceneGizmoHalfAxis(SelectionColor, Quaternion.AngleAxis(90, Vector3.right));
            SceneGizmoXAxis = CreateSceneGizmoAxis(XColor, AltColor, Quaternion.AngleAxis(-90, Vector3.forward));
            SceneGizmoYAxis = CreateSceneGizmoAxis(YColor, AltColor, Quaternion.identity);
            SceneGizmoZAxis = CreateSceneGizmoAxis(ZColor, AltColor, Quaternion.AngleAxis(90, Vector3.right));
            SceneGizmoCube = CreateCubeMesh(AltColor);
            SceneGizmoSelectedCube = CreateCubeMesh(SelectionColor);
            SceneGizmoQuad = CreateQuadMesh();
        }

        private static Mesh CreateQuadMesh(float quadWidth = 1, float cubeHeight = 1)
        {
            var vertice_0 = new Vector3(-quadWidth * .5f, -cubeHeight * .5f, 0);
            var vertice_1 = new Vector3(quadWidth * .5f, -cubeHeight * .5f, 0);
            var vertice_2 = new Vector3(-quadWidth * .5f, cubeHeight * .5f, 0);
            var vertice_3 = new Vector3(quadWidth * .5f, cubeHeight * .5f, 0);

            Vector3[] vertices =
            {
                vertice_2, vertice_3, vertice_1, vertice_0
            };

            int[] triangles =
            {
                // Cube Bottom Side Triangles
                3, 1, 0,
                3, 2, 1
            };

            Vector2[] uvs =
            {
                new(1, 0),
                new(0, 0),
                new(0, 1),
                new(1, 1)
            };

            var quadMesh = new Mesh();
            quadMesh.name = "quad";
            quadMesh.vertices = vertices;
            quadMesh.triangles = triangles;
            quadMesh.uv = uvs;
            quadMesh.RecalculateNormals();
            return quadMesh;
        }

        private static Mesh CreateCubeMesh(Color color, float cubeLength = 1, float cubeWidth = 1, float cubeHeight = 1)
        {
            var vertice_0 = new Vector3(-cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
            var vertice_1 = new Vector3(cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
            var vertice_2 = new Vector3(cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
            var vertice_3 = new Vector3(-cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
            var vertice_4 = new Vector3(-cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
            var vertice_5 = new Vector3(cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
            var vertice_6 = new Vector3(cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
            var vertice_7 = new Vector3(-cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
            Vector3[] vertices =
            {
                // Bottom Polygon
                vertice_0, vertice_1, vertice_2, vertice_3,
                // Left Polygon
                vertice_7, vertice_4, vertice_0, vertice_3,
                // Front Polygon
                vertice_4, vertice_5, vertice_1, vertice_0,
                // Back Polygon
                vertice_6, vertice_7, vertice_3, vertice_2,
                // Right Polygon
                vertice_5, vertice_6, vertice_2, vertice_1,
                // Top Polygon
                vertice_7, vertice_6, vertice_5, vertice_4
            };

            int[] triangles =
            {
                // Cube Bottom Side Triangles
                3, 1, 0,
                3, 2, 1,
                // Cube Left Side Triangles
                3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
                3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
                // Cube Front Side Triangles
                3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
                3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
                // Cube Back Side Triangles
                3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
                3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
                // Cube Rigth Side Triangles
                3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
                3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
                // Cube Top Side Triangles
                3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
                3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5
            };

            var colors = new Color[vertices.Length];
            for (var i = 0; i < colors.Length; ++i) colors[i] = color;

            var cubeMesh = new Mesh();
            cubeMesh.name = "cube";
            cubeMesh.vertices = vertices;
            cubeMesh.triangles = triangles;
            cubeMesh.colors = colors;
            cubeMesh.RecalculateNormals();
            return cubeMesh;
        }

        private static Mesh CreateConeMesh(Color color)
        {
            var segmentsCount = 12;
            var size = 1.0f / 5;

            var vertices = new Vector3[segmentsCount * 3 + 1];
            var triangles = new int[segmentsCount * 6];
            var colors = new Color[vertices.Length];
            for (var i = 0; i < colors.Length; ++i) colors[i] = color;

            var radius = size / 2.6f;
            var height = size;
            var deltaAngle = Mathf.PI * 2.0f / segmentsCount;

            var y = -height;

            vertices[vertices.Length - 1] = new Vector3(0, -height, 0);
            for (var i = 0; i < segmentsCount; i++)
            {
                var angle = i * deltaAngle;
                var x = Mathf.Cos(angle) * radius;
                var z = Mathf.Sin(angle) * radius;

                vertices[i] = new Vector3(x, y, z);
                vertices[segmentsCount + i] = new Vector3(0, 0.01f, 0);
                vertices[2 * segmentsCount + i] = vertices[i];
            }

            for (var i = 0; i < segmentsCount; i++)
            {
                triangles[i * 6] = i;
                triangles[i * 6 + 1] = segmentsCount + i;
                triangles[i * 6 + 2] = (i + 1) % segmentsCount;

                triangles[i * 6 + 3] = vertices.Length - 1;
                triangles[i * 6 + 4] = 2 * segmentsCount + i;
                triangles[i * 6 + 5] = 2 * segmentsCount + (i + 1) % segmentsCount;
            }

            var cone = new Mesh();
            cone.name = "Cone";
            cone.vertices = vertices;
            cone.triangles = triangles;
            cone.colors = colors;

            return cone;
        }

        private static Mesh CreateSceneGizmoHalfAxis(Color color, Quaternion rotation)
        {
            const float scale = 0.1f;
            var cone1 = CreateConeMesh(color);

            var cone1Combine = new CombineInstance();
            cone1Combine.mesh = cone1;
            cone1Combine.transform =
                Matrix4x4.TRS(Vector3.up * scale, Quaternion.AngleAxis(180, Vector3.right), Vector3.one);

            var result = new Mesh();
            result.CombineMeshes(new[] { cone1Combine }, true);

            var rotateCombine = new CombineInstance();
            rotateCombine.mesh = result;
            rotateCombine.transform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);

            result = new Mesh();
            result.CombineMeshes(new[] { rotateCombine }, true);
            result.RecalculateNormals();
            return result;
        }

        private static Mesh CreateSceneGizmoAxis(Color axisColor, Color altColor, Quaternion rotation)
        {
            const float scale = 0.1f;
            var cone1 = CreateConeMesh(axisColor);
            var cone2 = CreateConeMesh(altColor);

            var cone1Combine = new CombineInstance();
            cone1Combine.mesh = cone1;
            cone1Combine.transform =
                Matrix4x4.TRS(Vector3.up * scale, Quaternion.AngleAxis(180, Vector3.right), Vector3.one);

            var cone2Combine = new CombineInstance();
            cone2Combine.mesh = cone2;
            cone2Combine.transform = Matrix4x4.TRS(Vector3.down * scale, Quaternion.identity, Vector3.one);

            var result = new Mesh();
            result.CombineMeshes(new[] { cone1Combine, cone2Combine }, true);

            var rotateCombine = new CombineInstance();
            rotateCombine.mesh = result;
            rotateCombine.transform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);

            result = new Mesh();
            result.CombineMeshes(new[] { rotateCombine }, true);
            result.RecalculateNormals();
            return result;
        }

        public static float GetScreenScale(Vector3 position, Camera camera)
        {
            float h = camera.pixelHeight;
            if (camera.orthographic) return camera.orthographicSize * 2f / h * 90;

            var transform = camera.transform;
            var distance = Vector3.Dot(position - transform.position, transform.forward);
            var scale = 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            return scale / h * 90;
        }

        private static void DoAxes(Vector3 position, Matrix4x4 transform, RuntimeHandleAxis selectedAxis)
        {
            var x = Vector3.right;
            var y = Vector3.up;
            var z = Vector3.forward;

            x = transform.MultiplyVector(x);
            y = transform.MultiplyVector(y);
            z = transform.MultiplyVector(z);

            GL.Color(selectedAxis != RuntimeHandleAxis.X ? XColor : SelectionColor);
            GL.Vertex(position);
            GL.Vertex(position + x);
            GL.Color(selectedAxis != RuntimeHandleAxis.Y ? YColor : SelectionColor);
            GL.Vertex(position);
            GL.Vertex(position + y);
            GL.Color(selectedAxis != RuntimeHandleAxis.Z ? ZColor : SelectionColor);
            GL.Vertex(position);
            GL.Vertex(position + z);
        }


        public static void DoPositionHandle(Vector3 position, Quaternion rotation,
            RuntimeHandleAxis selectedAxis = RuntimeHandleAxis.None)
        {
            var screenScale = GetScreenScale(position, Camera.current);
            var transform = Matrix4x4.TRS(position, rotation, new Vector3(screenScale, screenScale, screenScale));

            LinesMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            DoAxes(position, transform, selectedAxis);

            const float s = 0.2f;
            var x = Vector3.right * s;
            var y = Vector3.up * s;
            var z = Vector3.forward * s;

            var camera = Camera.current;
            var toCam = camera.transform.position - position;

            var fx = Mathf.Sign(Vector3.Dot(toCam, x));
            var fy = Mathf.Sign(Vector3.Dot(toCam, y));
            var fz = Mathf.Sign(Vector3.Dot(toCam, z));

            x.x *= fx;
            y.y *= fy;
            z.z *= fz;

            var xy = x + y;
            var xz = x + z;
            var yz = y + z;

            x = transform.MultiplyPoint(x);
            y = transform.MultiplyPoint(y);
            z = transform.MultiplyPoint(z);
            xy = transform.MultiplyPoint(xy);
            xz = transform.MultiplyPoint(xz);
            yz = transform.MultiplyPoint(yz);

            GL.Color(selectedAxis != RuntimeHandleAxis.XZ ? YColor : SelectionColor);
            GL.Vertex(position);
            GL.Vertex(z);
            GL.Vertex(z);
            GL.Vertex(xz);
            GL.Vertex(xz);
            GL.Vertex(x);
            GL.Vertex(x);
            GL.Vertex(position);
            GL.Color(selectedAxis != RuntimeHandleAxis.XY ? ZColor : SelectionColor);
            GL.Vertex(position);
            GL.Vertex(y);
            GL.Vertex(y);
            GL.Vertex(xy);
            GL.Vertex(xy);
            GL.Vertex(x);
            GL.Vertex(x);
            GL.Vertex(position);
            GL.Color(selectedAxis != RuntimeHandleAxis.YZ ? XColor : SelectionColor);
            GL.Vertex(position);
            GL.Vertex(y);
            GL.Vertex(y);
            GL.Vertex(yz);
            GL.Vertex(yz);
            GL.Vertex(z);
            GL.Vertex(z);
            GL.Vertex(position);
            GL.End();

            GL.Begin(GL.QUADS);
            GL.Color(YColorTransparent);
            GL.Vertex(position);
            GL.Vertex(z);
            GL.Vertex(xz);
            GL.Vertex(x);
            GL.Color(ZColorTransparent);
            GL.Vertex(position);
            GL.Vertex(y);
            GL.Vertex(xy);
            GL.Vertex(x);
            GL.Color(XColorTransparent);
            GL.Vertex(position);
            GL.Vertex(y);
            GL.Vertex(yz);
            GL.Vertex(z);
            GL.End();

            ShapesMaterial.SetPass(0);
            Graphics.DrawMeshNow(Arrows, transform);
            if (selectedAxis == RuntimeHandleAxis.X)
                Graphics.DrawMeshNow(SelectionArrowX, transform);
            else if (selectedAxis == RuntimeHandleAxis.Y)
                Graphics.DrawMeshNow(SelectionArrowY, transform);
            else if (selectedAxis == RuntimeHandleAxis.Z) Graphics.DrawMeshNow(SelectionArrowZ, transform);
        }

        public static void DoRotationHandle(Quaternion rotation, Vector3 position,
            RuntimeHandleAxis selectedAxis = RuntimeHandleAxis.None)
        {
            var screenScale = GetScreenScale(position, Camera.current);
            float radius = 1;
            var scale = new Vector3(screenScale, screenScale, screenScale);
            var xTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            var yTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.AngleAxis(-90, Vector3.right),
                Vector3.one);
            var zTranform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);
            var objToWorld = Matrix4x4.TRS(position, Quaternion.identity, scale);

            LinesClipMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            GL.Color(selectedAxis != RuntimeHandleAxis.X ? XColor : SelectionColor);
            DrawCircle(xTranform, radius);
            GL.Color(selectedAxis != RuntimeHandleAxis.Y ? YColor : SelectionColor);
            DrawCircle(yTranform, radius);
            GL.Color(selectedAxis != RuntimeHandleAxis.Z ? ZColor : SelectionColor);
            DrawCircle(zTranform, radius);
            GL.End();

            GL.PopMatrix();

            LinesBillboardMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            GL.Color(selectedAxis != RuntimeHandleAxis.Free ? AltColor : SelectionColor);
            DrawCircle(Matrix4x4.identity, radius);
            GL.Color(selectedAxis != RuntimeHandleAxis.Screen ? AltColor : SelectionColor);
            DrawCircle(Matrix4x4.identity, radius * 1.1f);
            GL.End();

            GL.PopMatrix();
        }

        private static void DrawCircle(Matrix4x4 transform, float radius)
        {
            const int pointsPerCircle = 32;
            var angle = 0.0f;
            var z = 0.0f;
            var prevPoint = transform.MultiplyPoint(new Vector3(radius, 0, z));
            for (var i = 0; i < pointsPerCircle; i++)
            {
                GL.Vertex(prevPoint);
                angle += 2 * Mathf.PI / pointsPerCircle;
                var x = radius * Mathf.Cos(angle);
                var y = radius * Mathf.Sin(angle);
                var point = transform.MultiplyPoint(new Vector3(x, y, z));
                GL.Vertex(point);
                prevPoint = point;
            }
        }

        public static void DoScaleHandle(Vector3 scale, Vector3 position, Quaternion rotation,
            RuntimeHandleAxis selectedAxis = RuntimeHandleAxis.None)
        {
            var sScale = GetScreenScale(position, Camera.current);
            var linesTransform = Matrix4x4.TRS(position, rotation, scale * sScale);

            LinesMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            DoAxes(position, linesTransform, selectedAxis);
            GL.End();

            var rotM = Matrix4x4.TRS(Vector3.zero, rotation, scale);
            ShapesMaterial.SetPass(0);
            var screenScale = new Vector3(sScale, sScale, sScale);
            var xOffset = rotM.MultiplyVector(Vector3.right) * sScale;
            var yOffset = rotM.MultiplyVector(Vector3.up) * sScale;
            var zOffset = rotM.MultiplyVector(Vector3.forward) * sScale;
            if (selectedAxis == RuntimeHandleAxis.X)
            {
                Graphics.DrawMeshNow(SelectionCube, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeY, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeZ, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeUniform, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
            else if (selectedAxis == RuntimeHandleAxis.Y)
            {
                Graphics.DrawMeshNow(CubeX, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(SelectionCube, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeZ, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeUniform, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
            else if (selectedAxis == RuntimeHandleAxis.Z)
            {
                Graphics.DrawMeshNow(CubeX, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeY, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(SelectionCube, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeUniform, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
            else if (selectedAxis == RuntimeHandleAxis.Free)
            {
                Graphics.DrawMeshNow(CubeX, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeY, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeZ, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(SelectionCube, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
            else
            {
                Graphics.DrawMeshNow(CubeX, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeY, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeZ, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(CubeUniform, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
        }

        public static void DoSceneGizmo(Vector3 position, Quaternion rotation, Vector3 selection, float gizmoScale,
            float xAlpha = 1.0f, float yAlpha = 1.0f, float zAlpha = 1.0f)
        {
            var sScale = GetScreenScale(position, Camera.current) * gizmoScale;
            var screenScale = new Vector3(sScale, sScale, sScale);

            const float billboardScale = 0.125f;
            var billboardOffset = 0.4f;
            if (Camera.current.orthographic) billboardOffset = 0.42f;

            const float cubeScale = 0.15f;

            if (selection != Vector3.zero)
            {
                if (selection == Vector3.one)
                {
                    ShapesMaterialZTestOffset.SetPass(0);
                    Graphics.DrawMeshNow(SceneGizmoSelectedCube,
                        Matrix4x4.TRS(position, rotation, screenScale * cubeScale));
                }
                else
                {
                    if ((xAlpha == 1.0f || xAlpha == 0.0f) &&
                        (yAlpha == 1.0f || yAlpha == 0.0f) &&
                        (zAlpha == 1.0f || zAlpha == 0.0f))
                    {
                        ShapesMaterialZTestOffset.SetPass(0);
                        Graphics.DrawMeshNow(SceneGizmoSelectedAxis,
                            Matrix4x4.TRS(position, rotation * Quaternion.LookRotation(selection, Vector3.up),
                                screenScale));
                    }
                }
            }

            ShapesMaterialZTest.SetPass(0);
            ShapesMaterialZTest.color = Color.white;
            Graphics.DrawMeshNow(SceneGizmoCube, Matrix4x4.TRS(position, rotation, screenScale * cubeScale));
            if (xAlpha == 1.0f && yAlpha == 1.0f && zAlpha == 1.0f)
            {
                Graphics.DrawMeshNow(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, screenScale));
                Graphics.DrawMeshNow(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, screenScale));
                Graphics.DrawMeshNow(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, screenScale));
            }
            else
            {
                if (xAlpha < 1)
                {
                    ShapesMaterialZTest3.SetPass(0);
                    ShapesMaterialZTest3.color = new Color(1, 1, 1, yAlpha);
                    Graphics.DrawMeshNow(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest4.SetPass(0);
                    ShapesMaterialZTest4.color = new Color(1, 1, 1, zAlpha);
                    Graphics.DrawMeshNow(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest2.SetPass(0);
                    ShapesMaterialZTest2.color = new Color(1, 1, 1, xAlpha);
                    Graphics.DrawMeshNow(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    XMaterial.SetPass(0);
                }
                else if (yAlpha < 1)
                {
                    ShapesMaterialZTest4.SetPass(0);
                    ShapesMaterialZTest4.color = new Color(1, 1, 1, zAlpha);
                    Graphics.DrawMeshNow(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest2.SetPass(0);
                    ShapesMaterialZTest2.color = new Color(1, 1, 1, xAlpha);
                    Graphics.DrawMeshNow(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest3.SetPass(0);
                    ShapesMaterialZTest3.color = new Color(1, 1, 1, yAlpha);
                    Graphics.DrawMeshNow(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, screenScale));
                }
                else
                {
                    ShapesMaterialZTest2.SetPass(0);
                    ShapesMaterialZTest2.color = new Color(1, 1, 1, xAlpha);
                    Graphics.DrawMeshNow(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest3.SetPass(0);
                    ShapesMaterialZTest3.color = new Color(1, 1, 1, yAlpha);
                    Graphics.DrawMeshNow(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest4.SetPass(0);
                    ShapesMaterialZTest4.color = new Color(1, 1, 1, zAlpha);
                    Graphics.DrawMeshNow(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, screenScale));
                }
            }


            XMaterial.SetPass(0);
            XMaterial.color = new Color(1, 1, 1, xAlpha);
            DragSceneGizmoAxis(position, rotation, Vector3.right, gizmoScale, billboardScale, billboardOffset, sScale);

            YMaterial.SetPass(0);
            YMaterial.color = new Color(1, 1, 1, yAlpha);
            DragSceneGizmoAxis(position, rotation, Vector3.up, gizmoScale, billboardScale, billboardOffset, sScale);

            ZMaterial.SetPass(0);
            ZMaterial.color = new Color(1, 1, 1, zAlpha);
            DragSceneGizmoAxis(position, rotation, Vector3.forward, gizmoScale, billboardScale, billboardOffset,
                sScale);
        }

        private static void DragSceneGizmoAxis(Vector3 position, Quaternion rotation, Vector3 axis, float gizmoScale,
            float billboardScale, float billboardOffset, float sScale)
        {
            Vector3 reflectionOffset;

            reflectionOffset = Vector3.Reflect(Camera.current.transform.forward, axis) * 0.1f;
            var dotAxis = Vector3.Dot(Camera.current.transform.forward, axis);
            if (dotAxis > 0)
            {
                if (Camera.current.orthographic)
                    reflectionOffset += axis * dotAxis * 0.4f;
                else
                    reflectionOffset = axis * dotAxis * 0.7f;
            }
            else
            {
                if (Camera.current.orthographic)
                    reflectionOffset -= axis * dotAxis * 0.1f;
                else
                    reflectionOffset = Vector3.zero;
            }


            var pos = position + (axis + reflectionOffset) * billboardOffset * sScale;
            var scale = GetScreenScale(pos, Camera.current) * gizmoScale;
            var scaleVector = new Vector3(scale, scale, scale);
            Graphics.DrawMeshNow(SceneGizmoQuad, Matrix4x4.TRS(pos, rotation, scaleVector * billboardScale));
        }

        public static float GetGridFarPlane()
        {
            var h = Camera.current.transform.position.y;
            var d = CountOfDigits(h);
            var spacing = Mathf.Pow(10, d - 1);
            return spacing * 150;
        }


        public static void DrawGrid()
        {
            var cameraPosition = Camera.current.transform.position;

            var h = cameraPosition.y;
            h = Mathf.Abs(h);
            h = Mathf.Max(1, h);

            var d = CountOfDigits(h);

            var spacing = Mathf.Pow(10, d - 1);
            var nextSpacing = Mathf.Pow(10, d);
            var nextNextSpacing = Mathf.Pow(10, d + 1);

            var alpha = 1.0f - (h - spacing) / (nextSpacing - spacing);
            var alpha2 = (h * 10 - nextSpacing) / (nextNextSpacing - nextSpacing);

            DrawGrid(cameraPosition, spacing, alpha, h * 20);
            DrawGrid(cameraPosition, nextSpacing, alpha2, h * 20);
        }

        private static void DrawGrid(Vector3 cameraPosition, float spacing, float alpha, float fadeDisance)
        {
            cameraPosition.y = 0.0f;

            GridMaterial.SetFloat("_FadeDistance", fadeDisance);
            GridMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            GL.Color(new Color(1, 1, 1, 0.1f * alpha));

            cameraPosition.x = Mathf.Floor(cameraPosition.x / spacing) * spacing;
            cameraPosition.z = Mathf.Floor(cameraPosition.z / spacing) * spacing;

            for (var i = -150; i < 150; ++i)
            {
                GL.Vertex(cameraPosition + new Vector3(i * spacing, 0, -150 * spacing));
                GL.Vertex(cameraPosition + new Vector3(i * spacing, 0, 150 * spacing));

                GL.Vertex(cameraPosition + new Vector3(-150 * spacing, 0, i * spacing));
                GL.Vertex(cameraPosition + new Vector3(150 * spacing, 0, i * spacing));
            }

            GL.End();
        }

        public static float CountOfDigits(float number)
        {
            return number == 0 ? 1.0f : Mathf.Ceil(Mathf.Log10(Mathf.Abs(number) + 0.5f));
        }
    }
}