using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Core.ScriptableObjectArchitecture
{
    /// <summary>
    ///     This class implements the IGameEventListener interface and exposes a GameEvent that we can populate within the
    ///     inspector. When this GameEvent's Raise() method is fired externally, this class will invoke a UnityEvent.
    /// </summary>
    public class UnityEventGameEventListener : MonoBehaviour, IGameEventListenable
    {
        [SerializeField] private GameEvent m_GameEvent;

        [SerializeField] private UnityEvent m_Response;

        private void OnEnable()
        {
            Assert.IsNotNull(GameEvent, "Assign this GameEvent within the editor!");

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEvent.DeregisterListener(this);
        }

        public GameEvent GameEvent
        {
            get => m_GameEvent;
            set => m_GameEvent = value;
        }

        public void EventRaised()
        {
            m_Response.Invoke();
        }
    }
}