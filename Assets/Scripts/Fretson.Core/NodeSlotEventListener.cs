using Core;
using Core.PubSub;
using DG.Tweening;
using Messaging;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeSlotEventListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private IPublisher<NodeSlotHoveredEvent> _publisher;
    private Tween _tween;

    private void Start()
    {
        _publisher = DI.ResolvePublisher<NodeSlotHoveredEvent>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tween?.Complete();
        _tween = transform.parent.DOScale(1.4f, 0.1f).From(1.0f).SetEase(Ease.OutQuad);

        _publisher.Publish(new NodeSlotHoveredEvent
        {
            Slot = GetComponentInParent<NodeSlot>()
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tween?.Complete();
        _tween = transform.parent.DOScale(1.0f, 0.1f).From(1.4f).SetEase(Ease.OutQuad);

        _publisher.Publish(new NodeSlotHoveredEvent
        {
            Slot = null
        });
    }

    public class NodeSlotHoveredEvent : IMessage
    {
        public NodeSlot Slot;
    }
}