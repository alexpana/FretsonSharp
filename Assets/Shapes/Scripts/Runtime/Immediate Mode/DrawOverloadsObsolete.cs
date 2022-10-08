﻿// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/

using System;
using UnityEngine;

namespace Shapes
{
    // Obsolete/deprecated stuff
    public static partial class Draw
    {
        private const string OBS_DASH =
            "As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle";

        private const string OBS_FILL =
            "As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill";

        private const string OBS_REGPOLRENAME =
            "For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead";

        private const string OBS_TRIRENAME =
            "For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead";

        private const string JOINER = ". In addition: ";
        private const string OBS_REGPOLRENAME_AND_FILL = OBS_FILL + JOINER + OBS_REGPOLRENAME;

        private const string OBS_DISC_GRADIENT_PREFIX =
            "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.";

        private const string OBS_DISC_GRADIENT_DISC_RADIAL =
            OBS_DISC_GRADIENT_PREFIX + "Disc( ..., DiscColors.Radial(...) )";

        private const string OBS_DISC_GRADIENT_DISC_ANGULAR =
            OBS_DISC_GRADIENT_PREFIX + "Disc( ..., DiscColors.Angular(...) )";

        private const string OBS_DISC_GRADIENT_DISC_BILINEAR =
            OBS_DISC_GRADIENT_PREFIX + "Disc( ..., DiscColors.Bilinear(...) )";

        private const string OBS_DISC_GRADIENT_RING_RADIAL =
            OBS_DISC_GRADIENT_PREFIX + "Ring( ..., DiscColors.Radial(...) )";

        private const string OBS_DISC_GRADIENT_RING_ANGULAR =
            OBS_DISC_GRADIENT_PREFIX + "Ring( ..., DiscColors.Angular(...) )";

        private const string OBS_DISC_GRADIENT_RING_BILINEAR =
            OBS_DISC_GRADIENT_PREFIX + "Ring( ..., DiscColors.Bilinear(...) )";

        private const string OBS_DISC_GRADIENT_PIE_RADIAL =
            OBS_DISC_GRADIENT_PREFIX + "Pie( ..., DiscColors.Radial(...) )";

        private const string OBS_DISC_GRADIENT_PIE_ANGULAR =
            OBS_DISC_GRADIENT_PREFIX + "Pie( ..., DiscColors.Angular(...) )";

        private const string OBS_DISC_GRADIENT_PIE_BILINEAR =
            OBS_DISC_GRADIENT_PREFIX + "Pie( ..., DiscColors.Bilinear(...) )";

        private const string OBS_DISC_GRADIENT_ARC_RADIAL =
            OBS_DISC_GRADIENT_PREFIX + "Arc( ..., DiscColors.Radial(...) )";

        private const string OBS_DISC_GRADIENT_ARC_ANGULAR =
            OBS_DISC_GRADIENT_PREFIX + "Arc( ..., DiscColors.Angular(...) )";

        private const string OBS_DISC_GRADIENT_ARC_BILINEAR =
            OBS_DISC_GRADIENT_PREFIX + "Arc( ..., DiscColors.Bilinear(...) )";

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, float thickness, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, LineEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, LineEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, LineEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, LineEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, LineEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, LineEndCap endCaps,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness,
            LineEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness,
            LineEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness,
            LineEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void PolygonFill(PolygonPath path)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void PolygonFill(PolygonPath path, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void PolygonFill(PolygonPath path, PolygonTriangulation triangulation)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void PolygonFill(PolygonPath path, PolygonTriangulation triangulation, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void PolygonFillLinear(PolygonPath path, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void PolygonFillLinear(PolygonPath path, PolygonTriangulation triangulation, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void PolygonFillRadial(PolygonPath path, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void PolygonFillRadial(PolygonPath path, PolygonTriangulation triangulation, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, float angle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, float angle,
            float roundness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, float angle,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, float angle,
            float roundness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, float angle,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, float angle,
            float roundness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, float roundness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, float angle,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, float angle,
            float roundness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, float roundness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow()
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(float radius, float thickness, float angle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(float radius, float thickness, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(float radius, float thickness, float angle, float roundness,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, float radius, float thickness, float angle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, float radius, float thickness, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME, true)]
        public static void RegularPolygonHollow(int sideCount, float radius, float thickness, float angle,
            float roundness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, float radius, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, float radius, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, float radius, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, float radius, float angle, float roundness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, int sideCount, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, float angle, float roundness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, float angle, float roundness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle,
            float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, float angle, float roundness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle,
            float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill()
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(float radius, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(float radius, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(float radius, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(float radius, float angle, float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(int sideCount, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(int sideCount, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(int sideCount, float radius, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(int sideCount, float radius, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(int sideCount, float radius, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFill(int sideCount, float radius, float angle, float roundness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, float angle,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, float angle,
            float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness,
            float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness,
            float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness,
            float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness,
            float angle, float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angle, float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angle, float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill()
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(float radius, float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(float radius, float thickness, float angle, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(float radius, float thickness, float angle, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(float radius, float thickness, float angle, float roundness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, float radius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, float angle)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, float angle,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, float angle,
            float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, float angle,
            float roundness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, float radius, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, float radius, float angle, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, float radius, float angle, float roundness,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, int sideCount, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, int sideCount, float radius, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, int sideCount, float radius, float angle,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, int sideCount, float radius, float angle,
            float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, float radius, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, float radius, float angle,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, float radius, float angle,
            float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, int sideCount, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, float radius, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, float radius, float angle,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, float radius, float angle,
            float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, int sideCount, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(Vector3 fillStart, Vector3 fillEnd, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(float radius, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(float radius, float angle, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(float radius, float angle, float roundness, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(int sideCount, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(int sideCount, float radius, float angle, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillLinear(int sideCount, float radius, float angle, float roundness,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, float radius, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, float radius, float thickness, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, float radius, float thickness, float angle,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, float radius, float thickness, float angle,
            float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, float radius, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, float radius, float thickness,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, float radius, float thickness,
            float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, float radius, float thickness,
            float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, float radius, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, float radius, float thickness,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, float radius, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, float radius, float thickness,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(Vector3 fillStart, Vector3 fillEnd, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(float radius, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(float radius, float thickness, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(float radius, float thickness, float angle, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(float radius, float thickness, float angle, float roundness,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(int sideCount, Vector3 fillStart, Vector3 fillEnd,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(int sideCount, float radius, Vector3 fillStart,
            Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(int sideCount, float radius, float thickness,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(int sideCount, float radius, float thickness, float angle,
            Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillLinear(int sideCount, float radius, float thickness, float angle,
            float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, float radius, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, float radius, float angle, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, float radius, float angle, float roundness,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, int sideCount, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, int sideCount, float radius, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, int sideCount, float radius, float angle,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, int sideCount, float radius, float angle,
            float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, float radius, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, float radius, float angle,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, float radius, float angle,
            float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, int sideCount, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, float radius, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, float radius, float angle,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, float radius, float angle,
            float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, int sideCount, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(float radius, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(float radius, float angle, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(float radius, float angle, float roundness, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(int sideCount, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(int sideCount, float radius, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(int sideCount, float radius, float angle, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RegularPolygonFillRadial(int sideCount, float radius, float angle, float roundness,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, float radius, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, float radius, float thickness,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, float radius, float thickness, float angle,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, float radius, float thickness, float angle,
            float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, float radius, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, float radius, float thickness,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, float radius, float thickness,
            float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, float radius, float thickness,
            float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, float radius, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, float radius, float thickness,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius,
            float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, float radius, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, float radius, float thickness,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius,
            float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(Vector3 fillOrigin, float fillRadius, Color fillColorStart,
            Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(float radius, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(float radius, float thickness, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(float radius, float thickness, float angle,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(float radius, float thickness, float angle, float roundness,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(int sideCount, Vector3 fillOrigin, float fillRadius,
            Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(int sideCount, float radius, Vector3 fillOrigin,
            float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(int sideCount, float radius, float thickness,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(int sideCount, float radius, float thickness, float angle,
            Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_REGPOLRENAME_AND_FILL, true)]
        public static void RegularPolygonHollowFillRadial(int sideCount, float radius, float thickness, float angle,
            float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd,
            FillSpace fillSpace = FillSpace.Local)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_RADIAL, true)]
        public static void DiscGradientRadial(Vector3 pos, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_RADIAL, true)]
        public static void DiscGradientRadial(Vector3 pos, float radius, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_RADIAL, true)]
        public static void DiscGradientRadial(Vector3 pos, Vector3 normal, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_RADIAL, true)]
        public static void DiscGradientRadial(Vector3 pos, Vector3 normal, float radius, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_RADIAL, true)]
        public static void DiscGradientRadial(Vector3 pos, Quaternion rot, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_RADIAL, true)]
        public static void DiscGradientRadial(Vector3 pos, Quaternion rot, float radius, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_RADIAL, true)]
        public static void DiscGradientRadial(Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_RADIAL, true)]
        public static void DiscGradientRadial(float radius, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_ANGULAR, true)]
        public static void DiscGradientAngular(Vector3 pos, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_ANGULAR, true)]
        public static void DiscGradientAngular(Vector3 pos, float radius, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_ANGULAR, true)]
        public static void DiscGradientAngular(Vector3 pos, Vector3 normal, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_ANGULAR, true)]
        public static void DiscGradientAngular(Vector3 pos, Vector3 normal, float radius, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_ANGULAR, true)]
        public static void DiscGradientAngular(Vector3 pos, Quaternion rot, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_ANGULAR, true)]
        public static void DiscGradientAngular(Vector3 pos, Quaternion rot, float radius, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_ANGULAR, true)]
        public static void DiscGradientAngular(Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_ANGULAR, true)]
        public static void DiscGradientAngular(float radius, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_BILINEAR, true)]
        public static void DiscGradientBilinear(Vector3 pos, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_BILINEAR, true)]
        public static void DiscGradientBilinear(Vector3 pos, float radius, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_BILINEAR, true)]
        public static void DiscGradientBilinear(Vector3 pos, Vector3 normal, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_BILINEAR, true)]
        public static void DiscGradientBilinear(Vector3 pos, Vector3 normal, float radius, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_BILINEAR, true)]
        public static void DiscGradientBilinear(Vector3 pos, Quaternion rot, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_BILINEAR, true)]
        public static void DiscGradientBilinear(Vector3 pos, Quaternion rot, float radius, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_BILINEAR, true)]
        public static void DiscGradientBilinear(Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_DISC_BILINEAR, true)]
        public static void DiscGradientBilinear(float radius, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, DashStyle dashStyle)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, DashStyle dashStyle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, DashStyle dashStyle, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, DashStyle dashStyle, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed()
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(DashStyle dashStyle)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(DashStyle dashStyle, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(DashStyle dashStyle, float radius)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(DashStyle dashStyle, float radius, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(DashStyle dashStyle, float radius, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void RingDashed(DashStyle dashStyle, float radius, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, float radius, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, float radius, float thickness, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, Vector3 normal, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, Vector3 normal, float radius, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, Vector3 normal, float radius, float thickness,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, Quaternion rot, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, Quaternion rot, float radius, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Vector3 pos, Quaternion rot, float radius, float thickness,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(float radius, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL, true)]
        public static void RingGradientRadial(float radius, float thickness, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, float radius, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, float radius, float thickness, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, DashStyle dashStyle, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(float radius, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(float radius, float thickness, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(DashStyle dashStyle, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(DashStyle dashStyle, float radius, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_RADIAL + JOINER + OBS_DASH, true)]
        public static void RingGradientRadialDashed(DashStyle dashStyle, float radius, float thickness,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, float radius, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, float radius, float thickness, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, Vector3 normal, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, Vector3 normal, float radius, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, Vector3 normal, float radius, float thickness,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, Quaternion rot, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, Quaternion rot, float radius, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Vector3 pos, Quaternion rot, float radius, float thickness,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(float radius, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR, true)]
        public static void RingGradientAngular(float radius, float thickness, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, float radius, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, float radius, float thickness, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, DashStyle dashStyle, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(float radius, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(float radius, float thickness, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(DashStyle dashStyle, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(DashStyle dashStyle, float radius, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_ANGULAR + JOINER + OBS_DASH, true)]
        public static void RingGradientAngularDashed(DashStyle dashStyle, float radius, float thickness,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, float radius, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, float radius, float thickness, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, Vector3 normal, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, Vector3 normal, float radius, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, Vector3 normal, float radius, float thickness,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, Quaternion rot, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, Quaternion rot, float radius, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Vector3 pos, Quaternion rot, float radius, float thickness,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(float radius, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR, true)]
        public static void RingGradientBilinear(float radius, float thickness, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, float radius, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, float radius, float thickness, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(float radius, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(float radius, float thickness, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(DashStyle dashStyle, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(DashStyle dashStyle, float radius, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_RING_BILINEAR + JOINER + OBS_DASH, true)]
        public static void RingGradientBilinearDashed(DashStyle dashStyle, float radius, float thickness,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_RADIAL, true)]
        public static void PieGradientRadial(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_RADIAL, true)]
        public static void PieGradientRadial(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_RADIAL, true)]
        public static void PieGradientRadial(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_RADIAL, true)]
        public static void PieGradientRadial(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_RADIAL, true)]
        public static void PieGradientRadial(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_RADIAL, true)]
        public static void PieGradientRadial(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_RADIAL, true)]
        public static void PieGradientRadial(float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_RADIAL, true)]
        public static void PieGradientRadial(float radius, float angleRadStart, float angleRadEnd, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_ANGULAR, true)]
        public static void PieGradientAngular(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_ANGULAR, true)]
        public static void PieGradientAngular(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_ANGULAR, true)]
        public static void PieGradientAngular(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_ANGULAR, true)]
        public static void PieGradientAngular(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_ANGULAR, true)]
        public static void PieGradientAngular(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_ANGULAR, true)]
        public static void PieGradientAngular(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_ANGULAR, true)]
        public static void PieGradientAngular(float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_ANGULAR, true)]
        public static void PieGradientAngular(float radius, float angleRadStart, float angleRadEnd, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_BILINEAR, true)]
        public static void PieGradientBilinear(Vector3 pos, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_BILINEAR, true)]
        public static void PieGradientBilinear(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_BILINEAR, true)]
        public static void PieGradientBilinear(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_BILINEAR, true)]
        public static void PieGradientBilinear(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_BILINEAR, true)]
        public static void PieGradientBilinear(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_BILINEAR, true)]
        public static void PieGradientBilinear(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_BILINEAR, true)]
        public static void PieGradientBilinear(float angleRadStart, float angleRadEnd, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_PIE_BILINEAR, true)]
        public static void PieGradientBilinear(float radius, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart,
            float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart,
            float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float radius, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float radius, float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float radius, float thickness, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float radius, float thickness, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(float radius, float thickness, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart,
            float angleRadEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps)
        {
            _ = 0;
        }

        [Obsolete(OBS_DASH, true)]
        public static void ArcDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(float radius, float angleRadStart, float angleRadEnd, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(float radius, float thickness, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL, true)]
        public static void ArcGradientRadial(float radius, float thickness, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(float angleRadStart, float angleRadEnd, Color colorInner,
            Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(float radius, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_RADIAL + JOINER + OBS_DASH, true)]
        public static void ArcGradientRadialDashed(DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(float radius, float angleRadStart, float angleRadEnd, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(float radius, float thickness, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR, true)]
        public static void ArcGradientAngular(float radius, float thickness, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(float angleRadStart, float angleRadEnd, Color colorStart,
            Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(float radius, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_ANGULAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientAngularDashed(DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(float angleRadStart, float angleRadEnd, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(float radius, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(float radius, float thickness, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR, true)]
        public static void ArcGradientBilinear(float radius, float thickness, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius,
            float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(float angleRadStart, float angleRadEnd, Color colorInnerStart,
            Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(float radius, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(float radius, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(float radius, float thickness, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(float radius, float thickness, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd,
            ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(DashStyle dashStyle, float radius, float angleRadStart,
            float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd,
            Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_DISC_GRADIENT_ARC_BILINEAR + JOINER + OBS_DASH, true)]
        public static void ArcGradientBilinearDashed(DashStyle dashStyle, float radius, float thickness,
            float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart,
            Color colorInnerEnd, Color colorOuterEnd)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Rect rect)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Rect rect, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Rect rect, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Rect rect, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Rect rect, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Rect rect, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, Vector4 cornerRadii,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, Vector4 cornerRadii,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, Vector4 cornerRadii,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, Vector4 cornerRadii,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Rect rect)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Rect rect, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Rect rect, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Rect rect, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Rect rect, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Rect rect, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, Vector4 cornerRadii,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, Vector4 cornerRadii,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, Vector4 cornerRadii,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, float cornerRadius,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, Vector4 cornerRadii,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Rect rect, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Rect rect, float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Rect rect, float thickness, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Rect rect, float thickness, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Rect rect, float thickness, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Rect rect, float thickness, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness,
            GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness,
            float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness,
            float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness,
            Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness,
            Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot,
            float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot,
            float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot,
            float thickness, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot,
            float thickness, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot,
            float thickness, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot,
            float thickness, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            float thickness, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            float thickness, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            float thickness, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot,
            float thickness, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot,
            float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot,
            float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot,
            float thickness, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot,
            float thickness, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot,
            float thickness, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot,
            float thickness, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            float thickness, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            float thickness, float cornerRadius)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            float thickness, float cornerRadius, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            float thickness, Vector4 cornerRadii)
        {
            _ = 0;
        }

        [Obsolete(OBS_FILL, true)]
        public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot,
            float thickness, Vector4 cornerRadii, GradientFill fill)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, Color colorA, Color colorB, Color colorC)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, Color colorA, Color colorB,
            Color colorC)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness,
            Color color)
        {
            _ = 0;
        }

        [Obsolete(OBS_TRIRENAME, true)]
        public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness,
            Color colorA, Color colorB, Color colorC)
        {
            _ = 0;
        }
    }
}