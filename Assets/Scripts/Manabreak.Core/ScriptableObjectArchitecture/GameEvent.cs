using System.Collections.Generic;
using UnityEngine;

namespace Core.ScriptableObjectArchitecture
{
    /// <summary>
    ///     Class for encapsulating game-related events within ScriptableObject instances. This class defines a List of
    ///     GameEventListeners, which will be notified whenever this GameEvent's Raise() method is fired.
    /// </summary>
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        private readonly List<IGameEventListenable> m_Listeners = new();

        public void Raise()
        {
            for (var i = m_Listeners.Count - 1; i >= 0; i--)
            {
                if (m_Listeners[i] == null)
                {
                    m_Listeners.RemoveAt(i);
                    continue;
                }

                m_Listeners[i].EventRaised();
            }
        }

        public void RegisterListener(IGameEventListenable listener)
        {
            for (var i = 0; i < m_Listeners.Count; i++)
                if (m_Listeners[i] == listener)
                    return;

            m_Listeners.Add(listener);
        }

        public void DeregisterListener(IGameEventListenable listener)
        {
            m_Listeners.Remove(listener);
        }
    }
}