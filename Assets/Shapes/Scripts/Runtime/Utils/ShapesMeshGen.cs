// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/

// Uncomment this below if you want more detailed breakdowns over what exactly fails in polygon creation.
// This is disabled by default for performance reasons.
// #define DEBUG_POLYGON_CREATION

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shapes
{
    public static class ShapesMeshGen
    {
        private static bool generatingClockwisePolygon; // assigned in GenPolygonMesh, used by EarClipPoint

        private static bool SamePosition(Vector3 a, Vector3 b)
        {
            var delta = Mathf.Max(Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Abs(b.y - a.y)), Mathf.Abs(b.z - a.z));
            return delta < 0.00001f;
        }

        public static void GenPolylineMesh(Mesh mesh, IList<PolylinePoint> path, bool closed, PolylineJoins joins,
            bool flattenZ, bool useColors)
        {
            mesh.Clear(); // todo maybe not always do this you know?

            var pointCount = path.Count;

            if (pointCount < 2)
                return;
            if (pointCount == 2 && closed)
                closed = false;

            var firstPoint = path[0];
            var lastPoint = path[path.Count - 1];

            // if the last point is at the same place as the first and it's closed, ignore the last point
            if ((closed || pointCount == 2) && SamePosition(firstPoint.point, lastPoint.point))
            {
                pointCount--; // ignore last point
                if (pointCount < 2) // check point count again
                    return;
                lastPoint = path[path.Count - 2]; // second last point technically
            }

            // only mitered joints can be in the same submesh at the moment
            var separateJoinMesh = joins.HasJoinMesh();
            var isSimpleJoin = joins.HasSimpleJoin(); // only used when join meshes exist
            var vertsPerPathPoint = separateJoinMesh ? 5 : 2;
            var trianglesPerSegment = separateJoinMesh ? 4 : 2;
            var vertexCount = pointCount * vertsPerPathPoint;
            var vertexCountTotal = vertexCount;
            var segmentCount = closed ? pointCount : pointCount - 1;
            var triangleCount = segmentCount * trianglesPerSegment;
            var triangleIndexCount = triangleCount * 3;

            // Joins mesh data
            int[] meshJoinsTriangles = default;
            int joinVertsPerJoin = default;
            if (separateJoinMesh)
            {
                joinVertsPerJoin = isSimpleJoin ? 3 : 5;
                var joinCount = closed ? pointCount : pointCount - 2;
                var joinTrianglesPerJoin = isSimpleJoin ? 1 : 3;
                var joinTriangleIndexCount = joinCount * joinTrianglesPerJoin * 3;
                var vertexCountJoins = joinCount * joinVertsPerJoin;
                vertexCountTotal += vertexCountJoins;
                meshJoinsTriangles = new int[joinTriangleIndexCount];
            }


            var meshColors = useColors ? new Color[vertexCountTotal] : null;
            var meshVertices = new Vector3[vertexCountTotal];

#if UNITY_2019_3_OR_NEWER
            var meshUv0 = new Vector4[vertexCountTotal]; // UVs for masking. z contains endpoint status, w is thickness
            var meshUv1Prevs = new Vector3[vertexCountTotal];
            var meshUv2Nexts = new Vector3[vertexCountTotal];
#else
			// List<> is the only supported vec3 UV assignment method prior to Unity 2019.3
			List<Vector4> meshUv0 = new List<Vector4>( new Vector4[vertexCountTotal] );
			List<Vector3> meshUv1Prevs = new List<Vector3>( new Vector3[vertexCountTotal] );
			List<Vector3> meshUv2Nexts = new List<Vector3>( new Vector3[vertexCountTotal] );
#endif


            var meshTriangles = new int[triangleIndexCount];


            // indices used per triangle
            int iv0, iv1, iv2 = 0, iv3 = 0, iv4 = 0;
            int ivj0 = 0, ivj1 = 0, ivj2 = 0, ivj3 = 0, ivj4 = 0;
            var triId = 0;
            var triIdJoin = 0;
            for (var i = 0; i < pointCount; i++)
            {
                var isLast = i == pointCount - 1;
                var isFirst = i == 0;
                var makeJoin = closed || (!isLast && !isFirst);
                var isEndpoint = closed == false && (isFirst || isLast);
                float uvEndpointValue = isEndpoint ? isFirst ? -1 : 1 : 0;
                var pathThickness = path[i].thickness;

                void SetUv0(Vector4[] uvArr, float uvEndpointVal, float pathThicc, int id, float x, float y)
                {
                    uvArr[id] = new Vector4(x, y, uvEndpointVal, pathThicc);
                }


                // Indices & verts
                var vert = flattenZ ? new Vector3(path[i].point.x, path[i].point.y, 0f) : path[i].point;
                var color = useColors ? path[i].color.ColorSpaceAdjusted() : default;
                iv0 = i * vertsPerPathPoint;
                if (separateJoinMesh)
                {
                    iv1 = iv0 + 1; // "prev" outer
                    iv2 = iv0 + 2; // "next" outer
                    iv3 = iv0 + 3; // "prev" inner
                    iv4 = iv0 + 4; // "next" inner
                    meshVertices[iv0] = vert;
                    meshVertices[iv1] = vert;
                    meshVertices[iv2] = vert;
                    meshVertices[iv3] = vert;
                    meshVertices[iv4] = vert;
                    if (useColors)
                    {
                        meshColors[iv0] = color;
                        meshColors[iv1] = color;
                        meshColors[iv2] = color;
                        meshColors[iv3] = color;
                        meshColors[iv4] = color;
                    }


                    // joins mesh
                    if (makeJoin)
                    {
                        var joinIndex = closed ? i : i - 1; // Skip first if open
                        ivj0 = joinIndex * joinVertsPerJoin + vertexCount;
                        ivj1 = ivj0 + 1;
                        ivj2 = ivj0 + 2;
                        ivj3 = ivj0 + 3;
                        ivj4 = ivj0 + 4;
                        meshVertices[ivj0] = vert;
                        meshVertices[ivj1] = vert;
                        meshVertices[ivj2] = vert;
                        if (useColors)
                        {
                            meshColors[ivj0] = color;
                            meshColors[ivj1] = color;
                            meshColors[ivj2] = color;
                        }

                        if (isSimpleJoin == false)
                        {
                            meshVertices[ivj3] = vert;
                            meshVertices[ivj4] = vert;
                            if (useColors)
                            {
                                meshColors[ivj3] = color;
                                meshColors[ivj4] = color;
                            }
                        }
                    }
                }
                else
                {
                    iv1 = iv0 + 1; // Inner vert
                    meshVertices[iv0] = vert;
                    meshVertices[iv1] = vert;
                    if (useColors)
                    {
                        meshColors[iv0] = color;
                        meshColors[iv1] = color;
                    }
                }


                // Setting up next/previous positions
                Vector3 prevPos;
                Vector3 nextPos;
                if (i == 0)
                {
                    prevPos = closed ? lastPoint.point : firstPoint.point * 2 - path[1].point; // Mirror second point
                    nextPos = path[i + 1].point;
                }
                else if (i == pointCount - 1)
                {
                    prevPos = path[i - 1].point;
                    nextPos = closed
                        ? firstPoint.point
                        : path[pointCount - 1].point * 2 - path[pointCount - 2].point; // Mirror second last point
                }
                else
                {
                    prevPos = path[i - 1].point;
                    nextPos = path[i + 1].point;
                }

                void SetPrevNext(int atIndex)
                {
                    meshUv1Prevs[atIndex] = prevPos;
                    meshUv2Nexts[atIndex] = nextPos;
                }

                SetPrevNext(iv0);
                SetPrevNext(iv1);
                if (separateJoinMesh)
                {
                    SetPrevNext(iv2);
                    SetPrevNext(iv3);
                    SetPrevNext(iv4);
                    if (makeJoin)
                    {
                        SetPrevNext(ivj0);
                        SetPrevNext(ivj1);
                        SetPrevNext(ivj2);
                        if (isSimpleJoin == false)
                        {
                            SetPrevNext(ivj3);
                            SetPrevNext(ivj4);
                        }
                    }
                }

                if (separateJoinMesh)
                {
                    SetUv0(meshUv0, uvEndpointValue, pathThickness, iv0, 0, 0);
                    SetUv0(meshUv0, uvEndpointValue, pathThickness, iv1, -1, -1);
                    SetUv0(meshUv0, uvEndpointValue, pathThickness, iv2, -1, 1);
                    SetUv0(meshUv0, uvEndpointValue, pathThickness, iv3, 1, -1);
                    SetUv0(meshUv0, uvEndpointValue, pathThickness, iv4, 1, 1);
                    if (makeJoin)
                    {
                        SetUv0(meshUv0, uvEndpointValue, pathThickness, ivj0, 0, 0);
                        if (isSimpleJoin)
                        {
                            SetUv0(meshUv0, uvEndpointValue, pathThickness, ivj1, 1, -1);
                            SetUv0(meshUv0, uvEndpointValue, pathThickness, ivj2, 1, 1);
                        }
                        else
                        {
                            SetUv0(meshUv0, uvEndpointValue, pathThickness, ivj1, 1, -1);
                            SetUv0(meshUv0, uvEndpointValue, pathThickness, ivj2, -1, -1);
                            SetUv0(meshUv0, uvEndpointValue, pathThickness, ivj3, -1, 1);
                            SetUv0(meshUv0, uvEndpointValue, pathThickness, ivj4, 1, 1);
                        }
                    }
                }
                else
                {
                    SetUv0(meshUv0, uvEndpointValue, pathThickness, iv0, -1, i);
                    SetUv0(meshUv0, uvEndpointValue, pathThickness, iv1, 1, i);
                }


                if (isLast == false || closed)
                {
                    // clockwise order
                    void AddQuad(int a, int b, int c, int d)
                    {
                        meshTriangles[triId++] = a;
                        meshTriangles[triId++] = b;
                        meshTriangles[triId++] = c;
                        meshTriangles[triId++] = c;
                        meshTriangles[triId++] = d;
                        meshTriangles[triId++] = a;
                    }

                    if (separateJoinMesh)
                    {
                        var rootCenter = iv0;
                        var rootOuter = iv2;
                        var rootInner = iv4;
                        var nextCenter = isLast ? 0 : rootCenter + vertsPerPathPoint;
                        var nextOuter = nextCenter + 1;
                        var nextInner = nextCenter + 3;
                        AddQuad(rootCenter, rootOuter, nextOuter, nextCenter);
                        AddQuad(nextCenter, nextInner, rootInner, rootCenter);

                        if (makeJoin)
                        {
                            meshJoinsTriangles[triIdJoin++] = ivj0;
                            meshJoinsTriangles[triIdJoin++] = ivj1;
                            meshJoinsTriangles[triIdJoin++] = ivj2;

                            if (isSimpleJoin == false)
                            {
                                meshJoinsTriangles[triIdJoin++] = ivj2;
                                meshJoinsTriangles[triIdJoin++] = ivj3;
                                meshJoinsTriangles[triIdJoin++] = ivj0;

                                meshJoinsTriangles[triIdJoin++] = ivj0;
                                meshJoinsTriangles[triIdJoin++] = ivj3;
                                meshJoinsTriangles[triIdJoin++] = ivj4;
                            }
                        }
                    }
                    else
                    {
                        var rootOuter = iv0;
                        var rootInner = iv1;
                        var nextOuter = isLast ? 0 : rootOuter + vertsPerPathPoint;
                        var nextInner = nextOuter + 1;
                        AddQuad(rootInner, rootOuter, nextOuter, nextInner);
                    }
                }
            }

            // assign to segments mesh
            mesh.vertices = meshVertices;
            mesh.subMeshCount = 2;
            mesh.SetTriangles(meshTriangles, 0);
            mesh.SetTriangles(meshJoinsTriangles, 1);
            mesh.SetUVs(0, meshUv0);
            mesh.SetUVs(1, meshUv1Prevs);
            mesh.SetUVs(2, meshUv2Nexts);
            if (useColors)
                mesh.colors = meshColors;
        }

        public static void GenPolygonMesh(Mesh mesh, List<Vector2> path, PolygonTriangulation triangulation)
        {
            // kinda have to do this, the algorithm relies on knowing this
            generatingClockwisePolygon = ShapesMath.PolygonSignedArea(path) > 0;
            var clockwiseSign = generatingClockwisePolygon ? 1f : -1f;

#if DEBUG_POLYGON_CREATION
			List<string> debugString = new List<string>();
			debugString.Add( "Polygon creation process:" );
#endif

            mesh.Clear(); // todo maybe not always do this you know?
            var pointCount = path.Count;
            if (pointCount < 2)
                return;

            var triangleCount = pointCount - 2;
            var triangleIndexCount = triangleCount * 3;
            var meshTriangles = new int[triangleIndexCount];

            if (triangulation == PolygonTriangulation.FastConvexOnly)
            {
                var tri = 0;
                for (var i = 0; i < triangleCount; i++)
                {
                    meshTriangles[tri++] = i + 2;
                    meshTriangles[tri++] = i + 1;
                    meshTriangles[tri++] = 0;
                }
            }
            else
            {
                var pointsLeft = new List<EarClipPoint>(pointCount);
                for (var i = 0; i < pointCount; i++)
                    pointsLeft.Add(new EarClipPoint(i, new Vector2(path[i].x, path[i].y)));
                for (var i = 0; i < pointCount; i++)
                {
                    // update prev/next connections
                    var p = pointsLeft[i];
                    p.prev = pointsLeft[(i + pointCount - 1) % pointCount];
                    p.next = pointsLeft[(i + 1) % pointCount];
                }

                var tri = 0;
                int countLeft;
                var safeguard = 1000000;
                while ((countLeft = pointsLeft.Count) >= 3 && safeguard-- > 0)
                {
#if DEBUG_POLYGON_CREATION
					debugString.Add( $"------- Searching for convex points... -------" );
#endif
                    //for( int k = 0; k < pointsLeft.Count * 2; k++ ) {
                    if (countLeft == 3)
                    {
                        // final triangle
                        meshTriangles[tri++] = pointsLeft[2].vertIndex;
                        meshTriangles[tri++] = pointsLeft[1].vertIndex;
                        meshTriangles[tri++] = pointsLeft[0].vertIndex;
                        break;
                    }

                    // iterate until we find a convex vertex
                    var foundConvex = false;
                    for (var i = 0; i < countLeft; i++)
                    {
                        var p = pointsLeft[i];
                        if (p.ReflexState == ReflexState.Convex)
                        {
                            // it's convex! now make sure there are no reflex points inside
#if DEBUG_POLYGON_CREATION
							debugString.Add( $"{p.vertIndex} is convex, testing:" );
#endif
                            var canClipEar = true;
                            var idPrev = (i + countLeft - 1) % countLeft;
                            var idNext = (i + 1) % countLeft;
                            for (var j = 0; j < countLeft; j++)
                            {
                                if (j == i) continue; // skip self
                                if (j == idPrev) continue; // skip next
                                if (j == idNext) continue; // skip prev
                                if (pointsLeft[j].ReflexState ==
                                    ReflexState.Reflex) // found a reflex point, make sure it's outside the triangle
                                    if (ShapesMath.PointInsideTriangle(p.next.pt, p.pt, p.prev.pt, pointsLeft[j].pt, 0f,
                                            clockwiseSign * -0.0001f))
                                    {
#if DEBUG_POLYGON_CREATION
										debugString.Add( $"<color=#fa0>[{pointsLeft[j].vertIndex} is inside [{p.next.vertIndex},{p.vertIndex},{p.prev.vertIndex}]</color>" );
#endif
                                        canClipEar = false; // it's inside, rip
                                        break;
                                    }
                            }

                            if (canClipEar)
                            {
#if DEBUG_POLYGON_CREATION
								debugString.Add( $"<color=#af2>[{p.next.vertIndex},{p.vertIndex},{p.prev.vertIndex}] created</color>" );
#endif
                                meshTriangles[tri++] = p.next.vertIndex;
                                meshTriangles[tri++] = p.vertIndex;
                                meshTriangles[tri++] = p.prev.vertIndex;
                                p.next.MarkReflexUnknown();
                                p.prev.MarkReflexUnknown();
                                (p.next.prev, p.prev.next) = (p.prev, p.next); // update prev/next
                                pointsLeft.RemoveAt(i);
                                foundConvex = true;
                                break; // stop search for more convex edges, restart loop
                            }
                        }
                    }

                    // no convex found??
                    if (foundConvex == false)
                    {
                        var s =
                            "Invalid polygon triangulation - no convex edges found. Your polygon is likely self-intersecting.\n";
                        s += "Failed point set:\n";
                        s += string.Join("\n", pointsLeft.Select(p => $"[{p.vertIndex}]: {p.ReflexState}"));
#if DEBUG_POLYGON_CREATION
						s += "\n";
						debugString.Add( $"<color=#f33>No convex points found</color>" );
						s += string.Join( "\n", debugString );
#endif
                        Debug.LogError(s);
                        goto breakBoth;
                    }
                }

                breakBoth:

                if (safeguard < 1)
                    Debug.LogError(
                        "Polygon triangulation failed, please report a bug (Shapes/Report Bug) with this exact case included");
            }

            // assign to segments mesh
            var verts3D = new List<Vector3>(pointCount);
            for (var i = 0; i < pointCount; i++)
                verts3D.Add(path[i]);
            mesh.SetVertices(verts3D);
            mesh.subMeshCount = 1;
            mesh.SetTriangles(meshTriangles, 0);
        }


        public static void CreateDisc(Mesh mesh, int segmentsPerFullTurn, float radius)
        {
            GenerateDiscMesh(mesh, segmentsPerFullTurn, false, false, radius, 0f, 0f, 0f);
        }

        public static void CreateCircleSector(Mesh mesh, int segmentsPerFullTurn, float radius, float angRadiansStart,
            float angRadiansEnd)
        {
            GenerateDiscMesh(mesh, segmentsPerFullTurn, true, false, radius, 0f, angRadiansStart, angRadiansEnd);
        }

        public static void CreateAnnulus(Mesh mesh, int segmentsPerFullTurn, float radius, float radiusInner)
        {
            GenerateDiscMesh(mesh, segmentsPerFullTurn, true, false, radius, radiusInner, 0f, 0f);
        }

        public static void CreateAnnulusSector(Mesh mesh, int segmentsPerFullTurn, float radius, float radiusInner,
            float angRadiansStart, float angRadiansEnd)
        {
            GenerateDiscMesh(mesh, segmentsPerFullTurn, true, false, radius, radiusInner, angRadiansStart,
                angRadiansEnd);
        }

        private static void GenerateDiscMesh(Mesh mesh, int segmentsPerFullTurn, bool hasSector, bool hasInnerRadius,
            float radius, float radiusInner, float angRadiansStart, float angRadiansEnd)
        {
            var gizmoAngStart = hasSector ? angRadiansStart : 0f;
            var gizmoAngEnd = hasSector ? angRadiansEnd : ShapesMath.TAU;
            var turnSpan = Mathf.Abs(gizmoAngEnd - gizmoAngStart) / ShapesMath.TAU;
            var segmentCount = Mathf.Max(1, Mathf.RoundToInt(turnSpan * segmentsPerFullTurn));
            var gizmoOutermostRadius = Mathf.Max(radius, radiusInner);
            var apothemOuter = Mathf.Cos(0.5f * Mathf.Abs(gizmoAngEnd - gizmoAngStart) / segmentCount) *
                               gizmoOutermostRadius;
            var gizmoRadiusOuter = gizmoOutermostRadius * 2 - apothemOuter; // Adjust by apothem to fit better!
            var gizmoRadiusInner = hasInnerRadius ? Mathf.Min(radius, radiusInner) : 0f;

            // Generate mesh
            var triangleCount = segmentCount * 2 * 2; // 2(trisperquad) * 2(doublesided)
            var vertCount = (segmentCount + 1) * 2;

            var triIndices = new int[triangleCount * 3];
            var vertices = new Vector3[vertCount];
            var normals = new Vector3[vertCount];

            for (var i = 0; i < segmentCount + 1; i++)
            {
                var t = i / (float)segmentCount;
                var ang = Mathf.Lerp(gizmoAngStart, gizmoAngEnd, t);
                var dir = ShapesMath.AngToDir(ang);
                var iRoot = i * 2;
                var iInner = iRoot + 1;
                vertices[iRoot] = dir * gizmoRadiusOuter;
                vertices[iInner] = dir * gizmoRadiusInner;
                normals[iRoot] = Vector3.forward;
                normals[iInner] = Vector3.forward;
            }

            var tri = 0;
            for (var i = 0; i < segmentCount; i++)
            {
                var iRoot = i * 2;
                var iInner = iRoot + 1;
                var iNextOuter = iRoot + 2;
                var iNextInner = iRoot + 3;

                void DblTri(int a, int b, int c)
                {
                    triIndices[tri++] = a;
                    triIndices[tri++] = b;
                    triIndices[tri++] = c;
                    triIndices[tri++] = c;
                    triIndices[tri++] = b;
                    triIndices[tri++] = a;
                }

                DblTri(iRoot, iNextInner, iNextOuter);
                DblTri(iRoot, iInner, iNextInner);
            }

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = triIndices;
            mesh.RecalculateBounds();
        }

        private enum ReflexState
        {
            Unknown,
            Reflex,
            Convex
        }

        private class EarClipPoint
        {
            public EarClipPoint next;

            public EarClipPoint prev;
            public readonly Vector2 pt;
            private ReflexState reflex = ReflexState.Unknown;
            public readonly int vertIndex;

            public EarClipPoint(int vertIndex, Vector2 pt)
            {
                this.vertIndex = vertIndex;
                this.pt = pt;
            }

            public ReflexState ReflexState
            {
                get
                {
                    if (reflex == ReflexState.Unknown)
                    {
                        var dirNext = ShapesMath.Dir(pt, next.pt);
                        var dirPrev = ShapesMath.Dir(prev.pt, pt);
                        var cwSign = generatingClockwisePolygon ? 1 : -1;
                        reflex = cwSign * ShapesMath.Determinant(dirPrev, dirNext) >= -0.001f
                            ? ReflexState.Reflex
                            : ReflexState.Convex;
                    }

                    return reflex;
                }
            }

            public void MarkReflexUnknown()
            {
                reflex = ReflexState.Unknown;
            }
        }
    }
}