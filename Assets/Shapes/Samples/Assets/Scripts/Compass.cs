using UnityEngine;

namespace Shapes
{
    public class Compass : MonoBehaviour
    {
        public Vector2 position;
        public float width = 1f;
        [Range(0, 0.01f)] public float lineThickness = 0.1f;
        [Range(0.1f, 2f)] public float bendRadius = 1f;
        [Range(0.05f, ShapesMath.TAU * 0.49f)] public float fieldOfView = ShapesMath.TAU / 4;

        [Header("Ticks")] public int ticksPerQuarterTurn = 12;
        [Range(0, 0.2f)] public float tickSize = 0.1f;
        [Range(0f, 1f)] public float tickEdgeFadeFraction = 0.1f;
        [Range(0.01f, 0.26f)] public float fontSizeTickLabel = 1f;
        [Range(0, 0.1f)] public float tickLabelOffset = 0.01f;

        [Header("Degree Marker")] [Range(0.01f, 0.26f)]
        public float fontSizeLookLabel = 1f;

        public Vector2 lookAngLabelOffset;
        [Range(0, 0.05f)] public float triangleNootSize = 0.1f;

        private readonly string[] directionLabels = { "S", "W", "N", "E" };

        public void DrawCompass(Vector3 worldDir)
        {
            // prepare all variables
            var compArcOrigin = position + Vector2.down * bendRadius;
            var angUiMin = ShapesMath.TAU * 0.25f - width / 2 / bendRadius;
            var angUiMax = ShapesMath.TAU * 0.25f + width / 2 / bendRadius;
            var dirWorld = new Vector2(worldDir.x, worldDir.z).normalized;
            var lookAng = ShapesMath.DirToAng(dirWorld);
            var angWorldMin = lookAng + fieldOfView / 2;
            var angWorldMax = lookAng - fieldOfView / 2;
            var labelPos = compArcOrigin + Vector2.up * bendRadius + lookAngLabelOffset * 0.1f;
            var lookLabel = Mathf.RoundToInt(-lookAng * Mathf.Rad2Deg + 180f) + "°";

            // prepare draw state
            Draw.LineEndCaps = LineEndCap.Square;
            Draw.Thickness = lineThickness;

            // draw the horizontal line/arc of the compass
            Draw.Arc(compArcOrigin, bendRadius, lineThickness, angUiMin, angUiMax, ArcEndCap.Round);

            // draw the look angle label
            Draw.FontSize = fontSizeLookLabel;
            Draw.Text(labelPos, lookLabel, TextAlign.Center);

            // triangle arrow
            var trianglePos = compArcOrigin + Vector2.up * (bendRadius + 0.01f);
            Draw.RegularPolygon(trianglePos, 3, triangleNootSize, -ShapesMath.TAU / 4);

            // draw ticks
            var tickCount = (ticksPerQuarterTurn - 1) * 4;
            for (var i = 0; i < tickCount; i++)
            {
                var t = i / (float)tickCount;
                var ang = ShapesMath.TAU * t;
                var cardinal = i % (tickCount / 4) == 0;

                string label = null;
                if (cardinal)
                {
                    var angInt = Mathf.RoundToInt((1f - t) * 4);
                    label = directionLabels[angInt % 4];
                }

                var tCompass = ShapesMath.InverseLerpAngleRad(angWorldMax, angWorldMin, ang);
                if (tCompass < 1f && tCompass > 0f)
                    DrawTick(ang, cardinal ? 0.8f : 0.5f, label);
            }

            void DrawTick(float worldAng, float size, string label = null)
            {
                var tCompass = ShapesMath.InverseLerpAngleRad(angWorldMax, angWorldMin, worldAng);
                var uiAng = Mathf.Lerp(angUiMin, angUiMax, tCompass);
                var uiDir = ShapesMath.AngToDir(uiAng);
                var a = compArcOrigin + uiDir * bendRadius;
                var b = compArcOrigin + uiDir * (bendRadius - size * tickSize);
                var fade = Mathf.InverseLerp(0, tickEdgeFadeFraction, 1f - Mathf.Abs(tCompass * 2 - 1));
                Draw.Line(a, b, LineEndCap.None, new Color(1, 1, 1, fade));
                if (label != null)
                {
                    Draw.FontSize = fontSizeTickLabel;
                    var rotation = Quaternion.Euler(0, 0, (uiAng - ShapesMath.TAU / 4f) * Mathf.Rad2Deg);
                    Draw.Text(b - uiDir * tickLabelOffset, rotation, label, TextAlign.Center, new Color(1, 1, 1, fade));
                }
            }
        }
    }
}