#define ENABLE_STOPWATCH

using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Battlehub.Utils
{
    public class EditorStopwatch
    {
        public static EditorStopwatch Main;

#if UNITY_EDITOR && ENABLE_STOPWATCH
        private readonly Stopwatch m_stopwatch = new();
#endif

        static EditorStopwatch()
        {
            Main = new EditorStopwatch();
        }

        public void Start()
        {
#if UNITY_EDITOR && ENABLE_STOPWATCH
            m_stopwatch.Reset();
            m_stopwatch.Start();
#endif
        }

        public void Stop(string output)
        {
#if UNITY_EDITOR && ENABLE_STOPWATCH
            m_stopwatch.Stop();
            Debug.Log(m_stopwatch.ElapsedMilliseconds + " ms " + output);

#endif
        }
    }
}