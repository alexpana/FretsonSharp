using UnityEngine;

namespace Shapes
{
    public class AmmoBar : MonoBehaviour
    {
        public int totalBullets = 20;
        public int bullets = 15;

        [Header("Style")] [Range(0, 1f)] public float bulletThicknessScale = 1f;
        [Range(0, 0.5f)] public float bulletEjectScale = 0.5f;

        [Header("Animation")] public float bulletDisappearTime = 1f;
        [Range(0, ShapesMath.TAU)] public float bulletEjectAngSpeed = 0.5f;
        [Range(0, ShapesMath.TAU)] public float ejectRotSpeedVariance = 1f;
        public AnimationCurve bulletEjectX = AnimationCurve.Constant(0, 1, 0);
        public AnimationCurve bulletEjectY = AnimationCurve.Constant(0, 1, 0);

        private float[] bulletFireTimes;
        public bool HasBulletsLeft => bullets > 0;

        private void Awake()
        {
            bulletFireTimes = new float[totalBullets];
        }

        private Vector2 GetBulletEjectPos(Vector2 origin, float t)
        {
            var ejectAnimPos = new Vector2(bulletEjectX.Evaluate(t), bulletEjectY.Evaluate(t));
            return origin + ejectAnimPos * bulletEjectScale;
        }

        public void Fire()
        {
            bulletFireTimes[--bullets] = Time.time;
        }

        public void Reload()
        {
            bullets = totalBullets;
        }

        public void DrawBar(FpsController fpsController, float barRadius)
        {
            var barThickness = fpsController.ammoBarThickness;
            var ammoBarOutlineThickness = fpsController.ammoBarOutlineThickness;
            var angRadMin = -fpsController.ammoBarAngularSpanRad / 2;
            var angRadMax = fpsController.ammoBarAngularSpanRad / 2;

            // draw bullets
            Draw.LineEndCaps = LineEndCap.Round;
            var innerRadius = barRadius - barThickness / 2;
            var bulletThickness =
                innerRadius * fpsController.ammoBarAngularSpanRad / totalBullets * bulletThicknessScale;
            for (var i = 0; i < totalBullets; i++)
            {
                var t = i / (totalBullets - 1f);
                var angRad = Mathf.Lerp(angRadMin, angRadMax, t);
                var dir = ShapesMath.AngToDir(angRad);
                var origin = dir * barRadius;
                var offset = dir * (barThickness / 2f - ammoBarOutlineThickness * 1.5f);

                float alpha = 1;
                var hasBeenFired = i >= bullets;
                if (hasBeenFired && Application.isPlaying)
                {
                    var timePassed = Time.time - bulletFireTimes[i];
                    var tFade = Mathf.Clamp01(timePassed / bulletDisappearTime);
                    alpha = 1f - tFade;
                    origin = GetBulletEjectPos(origin, tFade);
                    var angle = timePassed * (bulletEjectAngSpeed + Mathf.Cos(i * 92372.8f) * ejectRotSpeedVariance);
                    offset = ShapesMath.Rotate(offset, angle);
                }

                var a = origin + offset;
                var b = origin - offset;
                Draw.Line(a, b, bulletThickness, new Color(1, 1, 1, alpha));
            }

            FpsController.DrawRoundedArcOutline(Vector2.zero, barRadius, barThickness, ammoBarOutlineThickness,
                angRadMin, angRadMax);
        }
    }
}