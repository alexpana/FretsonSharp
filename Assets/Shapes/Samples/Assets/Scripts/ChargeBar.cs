using System;
using UnityEngine;

namespace Shapes
{
    public class ChargeBar : MonoBehaviour
    {
        [Header("Gameplay")] [SerializeField] private float chargeSpeed = 1;
        [SerializeField] private float chargeDecaySpeed = 1;

        [Header("Style")] public Color tickColor = Color.white;
        public Gradient chargeFillGradient;
        [Range(0, 0.1f)] public float tickSizeSmol = 0.1f;
        [Range(0, 0.1f)] public float tickSizeLorge = 0.1f;
        [Range(0, 0.05f)] public float tickTickness;
        [Range(0, 0.5f)] public float fontSize = 0.1f;
        [Range(0, 0.5f)] public float fontSizeLorge = 0.1f;
        [Range(0, 0.1f)] public float percentLabelOffset = 0.1f;
        [Range(0, 0.4f)] public float fontGrowRangePrev = 0.1f;
        [Range(0, 0.4f)] public float fontGrowRangeNext = 0.1f;


        [Header("Animation")] public AnimationCurve chargeFillCurve;
        public AnimationCurve animChargeShakeMagnitude = AnimationCurve.Linear(0, 0, 1, 1);
        [Range(0, 0.05f)] public float chargeShakeMagnitude = 0.1f;
        public float chargeShakeSpeed = 1;
        private float charge;
        [NonSerialized] public bool isCharging = false;


        public void UpdateCharge()
        {
            if (isCharging)
                charge += chargeSpeed * Time.deltaTime;
            else
                charge -= chargeDecaySpeed * Time.deltaTime;
            charge = Mathf.Clamp01(charge);
        }

        public void DrawBar(FpsController fpsController, float barRadius)
        {
            // get some data
            var barThickness = fpsController.ammoBarThickness;
            var ammoBarOutlineThickness = fpsController.ammoBarOutlineThickness;
            var angRadMin = -fpsController.ammoBarAngularSpanRad / 2;
            var angRadMax = fpsController.ammoBarAngularSpanRad / 2;
            var angRadMinLeft = angRadMin + ShapesMath.TAU / 2;
            var angRadMaxLeft = angRadMax + ShapesMath.TAU / 2;
            var outerRadius = barRadius + barThickness / 2;

            var chargeAnim = chargeFillCurve.Evaluate(charge);

            // charge bar shake:
            var chargeMag = animChargeShakeMagnitude.Evaluate(chargeAnim) * chargeShakeMagnitude;
            var origin = fpsController.GetShake(chargeShakeSpeed, chargeMag); // do shake here
            var chargeAngRad = Mathf.Lerp(angRadMaxLeft, angRadMinLeft, chargeAnim);
            var chargeColor = chargeFillGradient.Evaluate(chargeAnim);
            Draw.Arc(origin, fpsController.ammoBarRadius, barThickness, angRadMaxLeft, chargeAngRad, chargeColor);

            var movingLeftPos = origin + ShapesMath.AngToDir(chargeAngRad) * barRadius;
            var bottomLeftPos = origin + ShapesMath.AngToDir(angRadMaxLeft) * barRadius;

            // bottom fill
            Draw.Disc(bottomLeftPos, barThickness / 2f, chargeColor);

            // ticks
            const int tickCount = 7;

            Draw.LineEndCaps = LineEndCap.None;
            for (var i = 0; i < tickCount; i++)
            {
                var t = i / (tickCount - 1f);
                var angRad = Mathf.Lerp(angRadMaxLeft, angRadMinLeft, t);
                var dir = ShapesMath.AngToDir(angRad);
                var a = origin + dir * outerRadius;
                var lorge = i % 3 == 0;
                var b = a + dir * (lorge ? tickSizeLorge : tickSizeSmol);
                Draw.Line(a, b, tickTickness, tickColor);

                // scale based on distance to real value
                var chargeDelta = t - chargeAnim;
                var growRange = chargeDelta < 0 ? fontGrowRangePrev : fontGrowRangeNext;
                var tFontScale = 1f - ShapesMath.SmoothCos01(Mathf.Clamp01(Mathf.Abs(chargeDelta) / growRange));
                var fontScale = ShapesMath.Eerp(fontSize, fontSizeLorge, tFontScale);
                Draw.FontSize = fontScale;
                var labelPos = a + dir * percentLabelOffset;
                var pct = Mathf.RoundToInt(t * 100) + "%";
                var rotation = Quaternion.Euler(0, 0, (angRad + ShapesMath.TAU / 2) * Mathf.Rad2Deg);
                Draw.Text(labelPos, rotation, pct, TextAlign.Right);
            }

            // moving dot
            Draw.Disc(movingLeftPos, barThickness / 2f + ammoBarOutlineThickness / 2f);
            Draw.Disc(movingLeftPos, barThickness / 2f - ammoBarOutlineThickness / 2f, chargeColor);

            FpsController.DrawRoundedArcOutline(origin, barRadius, barThickness, ammoBarOutlineThickness, angRadMinLeft,
                angRadMaxLeft);

            Draw.LineEndCaps = LineEndCap.Round;

            // glow
            Draw.BlendMode = ShapesBlendMode.Additive;
            Draw.Disc(movingLeftPos, barThickness * 2, DiscColors.Radial(chargeColor, Color.clear));
            Draw.BlendMode = ShapesBlendMode.Transparent;
        }
    }
}