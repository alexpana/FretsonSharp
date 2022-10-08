using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    public class AboutWindow : EditorWindow
    {
        private const float DOOT_MAX_RADIUS = 0.95f;
        private static Doot[] doots;
        private static readonly Color colMain = new(1, 1, 1, 1f);
        private static readonly Color colFade = new(0f, 0.1f, 0.1f, 0f);
        private static readonly Color colF15 = new(1f, 0.0666666f, 0.333333f, 1f);
        private GUIStyle labelCentered;

        private GUIStyle labelStyle;
        private GUIStyle linkStyle;

        private float mouseDootT = 0f;

        [NonSerialized] private string newVersionAvailable = null;
        [NonSerialized] private UnityWebRequest req;
        private GUIStyle titleStyle;

        private GUIStyle LabelStyle => labelStyle ?? (labelStyle = new GUIStyle(GUI.skin.label)
            { active = { textColor = Color.white }, normal = { textColor = Color.white } });

        private GUIStyle LinkStyle =>
            linkStyle ?? (linkStyle = new GUIStyle(LabelStyle) { hover = { textColor = colF15 } });

        private GUIStyle TitleStyle => titleStyle ?? (titleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 26, alignment = TextAnchor.MiddleCenter, active = { textColor = Color.white },
            normal = { textColor = Color.white }
        });

        private GUIStyle LabelCentered => labelCentered ?? (labelCentered = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter, active = { textColor = Color.white },
            normal = { textColor = Color.white }
        });

        private bool WebRequestHasErrors
        {
            get
            {
#if UNITY_2020_1_OR_NEWER
                return req.result == UnityWebRequest.Result.ProtocolError ||
                       req.result == UnityWebRequest.Result.ConnectionError;
#else
				return req.isHttpError || req.isNetworkError;
#endif
            }
        }

        private void Update()
        {
            if (req != null && req.isDone)
            {
                if (WebRequestHasErrors == false)
                {
                    OnReceiveLatestVersion(req.downloadHandler.text);
                    req.Dispose();
                    req = null;
                }
                else if (WebRequestHasErrors)
                {
                    Debug.LogWarning($"Shapes failed to check for updates. Reason: {req.error}");
                    req.Dispose();
                    req = null;
                }
            }

            Repaint();
        }

        private void OnEnable()
        {
            req = UnityWebRequest.Get(ShapesInfo.LINK_LATEST_VERSION);
            req.SendWebRequest();

            const int count = 16;
            doots = new Doot[count];
            for (var i = 0; i < count; i++)
            {
                const float maxSpeed = 0.3f;
                const float minSpeed = 0.1f;
                var speed = Random.Range(minSpeed, maxSpeed) * Mathf.Sign(Random.value - 0.5f);
                doots[i] = new Doot(Random.value * ShapesMath.TAU, speed, Random.Range(0.5f, DOOT_MAX_RADIUS));
            }

            EditorApplication.update += Update;
        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }


        private void OnGUI()
        {
            if (Event.current.type == EventType.Repaint)
                DrawShapes();
            DrawText();
            if (Event.current.type == EventType.MouseDown)
            {
                GUI.FocusControl(null);
                Repaint();
            }
        }

        private void OnReceiveLatestVersion(string latestVersion)
        {
            int[] GetVersionNums(string str)
            {
                return str.Split('.').Select(int.Parse).ToArray();
            }

            var latest = GetVersionNums(latestVersion);
            var current = GetVersionNums(ShapesInfo.Version);
            newVersionAvailable = null;
            for (var i = 0; i < 3; i++)
            {
                if (current[i] > latest[i])
                    return; // local version is newer somehow. you're probably freya. or a hacker >:I
                if (latest[i] > current[i])
                {
                    newVersionAvailable = latestVersion;
                    return;
                }
            }
        }

        private void DrawText()
        {
            using (new CenterVertical())
            {
                GUI.color = colMain;

                if (newVersionAvailable != null)
                    using (ShapesUI.Horizontal)
                    {
                        using (new Center())
                        {
                            var t = (float)EditorApplication.timeSinceStartup;
                            var wave = ShapesMath.SmoothCos01(Mathf.PingPong(t, 0.5f) * 2);
                            wave = Mathf.Lerp(0.5f, 1f, wave);
                            GUI.color = Color.Lerp(Color.white, colF15, wave);
                            LinkLabel(newVersionAvailable + " now available", ShapesInfo.LINK_CHANGELOG);
                            GUI.color = Color.white;
                        }
                    }


                GUILayout.Label($"Shapes {ShapesInfo.Version}", TitleStyle);
                using (ShapesUI.Horizontal)
                {
                    using (new Center())
                    {
                        var year = Mathf.Max(DateTime.Now.Year, 2020); // just in case your computer clock is wonky~
                        GUILayout.Label($"© {year}", LabelStyle, GUILayout.ExpandWidth(false));
                        LinkLabel("Freya Holmér", ShapesInfo.LINK_TWITTER);
                    }
                }

                GUI.color = Color.white;
                GUILayout.Space(8);
                GUILayout.Label("made possible thanks to\nthe wonderful supporters on", LabelCentered,
                    GUILayout.ExpandWidth(true));
                using (ShapesUI.Horizontal)
                {
                    using (new Center())
                    {
                        LinkLabel("Patreon", ShapesInfo.LINK_PATREON);
                    }
                }

                GUI.color = colF15;
                GUILayout.Label("♥", TitleStyle, GUILayout.ExpandWidth(true));
                GUI.color = Color.white;
            }
        }

        private void DrawShapes()
        {
            var center = position.size / 2;
            var fitRadius = Mathf.Min(position.width, position.height) / 2 - 8;

            // set doot positions
            var t = (float)EditorApplication.timeSinceStartup / 2;
            foreach (var doot in doots)
            {
                var ang = doot.angSpeed * t * ShapesMath.TAU + doot.angOffset;
                var dir = ShapesMath.AngToDir(ang);
                doot.pos = dir * (fitRadius * doot.radialOffset);
            }

            // mouse doot~
            var mouseRawPos = Event.current.mousePosition - center;
            var maxRadius = fitRadius * DOOT_MAX_RADIUS;
            var mouseTargetPos = Vector2.ClampMagnitude(mouseRawPos, maxRadius);
            doots[0].pos = Vector2.Lerp(doots[0].pos, mouseTargetPos, mouseDootT);
            var mouseOver = mouseOverWindow == this;
            mouseDootT = Mathf.Lerp(mouseDootT, mouseOver ? 1f : 0f, 0.05f);


            Draw.Push(); // save state
            Draw.ResetAllDrawStates();

            // draw setup
            Draw.Matrix = Matrix4x4.TRS(new Vector3(center.x, center.y, 1f), Quaternion.identity, Vector3.one);
            Draw.BlendMode = ShapesBlendMode.Transparent;
            Draw.RadiusSpace = ThicknessSpace.Meters;
            Draw.ThicknessSpace = ThicknessSpace.Meters;
            Draw.LineGeometry = LineGeometry.Flat2D;
            Draw.LineEndCaps = LineEndCap.Round;

            // Drawing
            Draw.Ring(Vector3.zero, fitRadius, fitRadius * 0.1f, DiscColors.Radial(Color.black, new Color(0, 0, 0, 0)));
            Draw.Disc(Vector3.zero, fitRadius, Color.black);

            // edge noodles
            const int noodCount = 64;
            for (var i = 0; i < noodCount; i++)
            {
                var tDir = i / (float)noodCount;
                var tAng = ShapesMath.TAU * tDir;
                var dir = ShapesMath.AngToDir(tAng);
                if (Mathf.Abs(dir.y) > 0.75f)
                    continue;
                var root = dir * fitRadius;
                var distToNearest = float.MaxValue;
                for (var j = 0; j < doots.Length; j++)
                    distToNearest = Mathf.Min(distToNearest, Vector2.Distance(doots[j].pos, root));
                var distMod = Mathf.InverseLerp(fitRadius * 0.5f, fitRadius * 0.1f, distToNearest);
                var noodMaxOffset = fitRadius * (1 + 0.1f * distMod);
                Draw.Line(root, dir * noodMaxOffset, fitRadius * Mathf.Lerp(0.07f, 0.04f, distMod));
            }

            // ring
            Draw.Ring(Vector3.zero, fitRadius, fitRadius * 0.0125f, colMain);

            // connecting lines
            for (var i = 0; i < doots.Length; i++)
            {
                var a = doots[i].pos;
                for (var j = i; j < doots.Length; j++)
                {
                    var b = doots[j].pos;
                    var dist = Vector2.Distance(a, b);
                    var rangeValue = Mathf.InverseLerp(fitRadius * 1f, fitRadius * 0.02f, dist);
                    if (rangeValue > 0)
                    {
                        var col = Color.Lerp(colFade, colMain, rangeValue);
                        Draw.Line(a, b, fitRadius * 0.015f * rangeValue, LineEndCap.Round, col);
                    }
                }
            }

            // doots~
            foreach (var doot in doots)
            {
                Draw.BlendMode = ShapesBlendMode.Transparent;
                Draw.Disc(doot.pos, fitRadius * 0.025f, Color.black);
                Draw.Disc(doot.pos, fitRadius * 0.015f, colMain);
                Draw.BlendMode = ShapesBlendMode.Additive;
                var innerColor = colMain;
                innerColor.a = 0.25f;
                var outerColor = Color.clear;
                Draw.Disc(doot.pos, fitRadius * 0.18f, DiscColors.Radial(innerColor, outerColor));
            }

            Draw.BlendMode = ShapesBlendMode.Multiplicative;
            Draw.Disc(Vector3.zero, fitRadius * 0.5f, DiscColors.Radial(Color.black, Color.clear));

            Draw.Pop(); // restore state
        }


        private void LinkLabel(string label, string url)
        {
            var content = new GUIContent(label);
            if (GUILayout.Button(content, LinkStyle, GUILayout.ExpandWidth(false)))
                Application.OpenURL(url);
            var r = GUILayoutUtility.GetLastRect();
            EditorGUIUtility.AddCursorRect(r, MouseCursor.Link);
            Handles.BeginGUI();
            var c = GUI.color;
            c.a = 0.5f;
            Handles.color = c;
            var y = r.yMax - 1;
            float margin = 2;
            Handles.DrawLine(new Vector3(r.xMin + margin, y), new Vector3(r.xMax - margin, y));
            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private class Center : IDisposable
        {
            public Center()
            {
                Noot();
            }

            public void Dispose()
            {
                Noot();
            }

            private static void Noot()
            {
                GUILayout.Label(GUIContent.none, GUILayout.ExpandWidth(true));
            }
        }

        private class CenterVertical : IDisposable
        {
            public CenterVertical()
            {
                Noot();
            }

            public void Dispose()
            {
                Noot();
            }

            private static void Noot()
            {
                GUILayout.Label(GUIContent.none, GUILayout.ExpandHeight(true));
            }
        }

        private class Doot
        {
            public readonly float angOffset;
            public readonly float angSpeed;
            public Vector2 pos;
            public readonly float radialOffset;

            public Doot(float angOffset, float angSpeed, float radialOffset)
            {
                this.angOffset = angOffset;
                this.angSpeed = angSpeed;
                this.radialOffset = radialOffset;
            }
        }
    }
}