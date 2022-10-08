using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.Utils
{
    public class QuaternionAnimationInfo : AnimationInfo<object, Quaternion>
    {
        public QuaternionAnimationInfo(Quaternion from, Quaternion to, float duration,
            Func<float, float> easingFunction, AnimationCallback<object, Quaternion> callback, object target = null) :
            base(from, to, duration, easingFunction, callback, target)
        {
        }

        protected override Quaternion Lerp(Quaternion from, Quaternion to, float t)
        {
            return Quaternion.Lerp(from, to, t);
        }
    }

    public class Vector3SlerpAnimationInfo : AnimationInfo<object, Vector3>
    {
        public Vector3SlerpAnimationInfo(Vector3 from, Vector3 to, float duration, Func<float, float> easingFunction,
            AnimationCallback<object, Vector3> callback, object target = null) :
            base(from, to, duration, easingFunction, callback, target)
        {
        }

        protected override Vector3 Lerp(Vector3 from, Vector3 to, float t)
        {
            return Vector3.Slerp(from, to, t);
        }
    }

    public class Vector3AnimationInfo : AnimationInfo<object, Vector3>
    {
        public Vector3AnimationInfo(Vector3 from, Vector3 to, float duration, Func<float, float> easingFunction,
            AnimationCallback<object, Vector3> callback, object target = null) :
            base(from, to, duration, easingFunction, callback, target)
        {
        }

        protected override Vector3 Lerp(Vector3 from, Vector3 to, float t)
        {
            return Vector3.Lerp(from, to, t);
        }
    }

    public class FloatAnimationInfo : AnimationInfo<object, float>
    {
        public FloatAnimationInfo(float from, float to, float duration, Func<float, float> easingFunction,
            AnimationCallback<object, float> callback, object target = null) :
            base(from, to, duration, easingFunction, callback, target)
        {
        }

        protected override float Lerp(float from, float to, float t)
        {
            return to * t + from * (1 - t);
        }
    }

    public delegate void AnimationCallback<TObj, TValue>(TObj obj, TValue value, float t, bool completed);

    public abstract class AnimationInfo<TObj, TValue> : IAnimationInfo
    {
        private readonly AnimationCallback<TObj, TValue> m_callback;

        private readonly float m_duration;
        private readonly Func<float, float> m_easingFunction;
        private readonly TValue m_from;

        private float m_t;

        private readonly TObj m_target;
        private readonly TValue m_to;

        public AnimationInfo(TValue from, TValue to, float duration, Func<float, float> easingFunction,
            AnimationCallback<TObj, TValue> callback, TObj target)
        {
            if (callback == null) throw new ArgumentNullException("callback");

            if (easingFunction == null) throw new ArgumentNullException("easingFunction");

            m_target = target;
            m_from = from;
            m_to = to;
            m_duration = duration;
            m_callback = callback;
            m_easingFunction = easingFunction;
        }

        float IAnimationInfo.Duration => m_duration;

        float IAnimationInfo.T
        {
            get => m_t;
            set
            {
                m_t = value;
                if (m_t < 0) m_t = 0;

                if (!float.IsNaN(m_t))
                {
                    var completed = m_t >= m_duration;
                    var t = completed ? 1 : m_easingFunction(m_t / m_duration);
                    var tValue = Lerp(m_from, m_to, t);
                    m_callback(m_target, tValue, m_t, completed);
                }
            }
        }

        public bool InProgress => m_t > 0 && m_t < m_duration;

        public void Abort()
        {
            m_t = float.NaN;
        }

        public static float EaseLinear(float t)
        {
            return t;
        }

        public static float EaseInQuad(float t)
        {
            return t * t;
        }

        public static float EaseOutQuad(float t)
        {
            return t * (2 - t);
        }

        public static float EaseInOutQuad(float t)
        {
            return t < .5 ? 2 * t * t : -1 + (4 - 2 * t) * t;
        }

        public static float EaseInCubic(float t)
        {
            return t * t * t;
        }

        public static float EaseOutCubic(float t)
        {
            return --t * t * t + 1;
        }

        public static float EaseInOutCubic(float t)
        {
            return t < .5 ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
        }

        public static float EaseInQuart(float t)
        {
            return t * t * t * t;
        }

        public static float EaseOutQuart(float t)
        {
            return 1 - --t * t * t * t;
        }

        public static float EaseInOutQuart(float t)
        {
            return t < .5 ? 8 * t * t * t * t : 1 - 8 * --t * t * t * t;
        }

        public static float ElasticEaseIn(float t)
        {
            return Mathf.Sin(13 * Mathf.PI * 2 * t) * Mathf.Pow(2, 10 * (t - 1));
        }

        public static float ElasticEaseOut(float t)
        {
            return Mathf.Sin(-13 * Mathf.PI * 2 * (t + 1)) * Mathf.Pow(2, -10 * t) + 1;
        }

        public static float ElasticEaseInOut(float t)
        {
            if (t < 0.5)
                return 0.5f * Mathf.Sin(13 * Mathf.PI * 2 * (2 * t)) * Mathf.Pow(2, 10 * (2 * t - 1));
            return 0.5f * (Mathf.Sin(-13 * Mathf.PI * 2 * (2 * t - 1 + 1)) * Mathf.Pow(2, -10 * (2 * t - 1)) + 2);
        }

        protected abstract TValue Lerp(TValue from, TValue to, float t);
    }

    public interface IAnimationInfo
    {
        float Duration { get; }

        float T { get; set; }

        bool InProgress { get; }

        void Abort();
    }

    [ExecuteInEditMode]
    public class Run : MonoBehaviour
    {
        private List<IAnimationInfo> m_animations;

        public static Run Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Debug.LogWarning("Another instance of Animation already exist");
            Instance = this;
            m_animations = new List<IAnimationInfo>();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Instance == null)
            {
                Instance = this;
                m_animations = new List<IAnimationInfo>();
            }
#endif
            for (var i = 0; i < m_animations.Count; ++i)
            {
                var animation = m_animations[i];
                animation.T += Time.deltaTime;
                if (animation.T >= animation.Duration) m_animations.Remove(animation);
            }
        }

        public void Animation(IAnimationInfo animation)
        {
            if (m_animations.Contains(animation)) return;

            m_animations.Add(animation);
        }

        public void Remove(IAnimationInfo animation)
        {
            m_animations.Remove(animation);
        }
    }
}