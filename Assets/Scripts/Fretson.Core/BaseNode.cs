using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fretson.Core
{
    public class BaseNode : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private Vector3 _startDragPosition;
        private Vector3 _startMousePosition;
        private bool _dragging = false;

        public NodeSlot[] Slots;

        private void Start()
        {
            if (Slots.Length == 0)
            {
                Slots = GetComponentsInChildren<NodeSlot>();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position += new Vector3(
                eventData.delta.x,
                eventData.delta.y,
                0);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _startDragPosition = transform.position;
            _startMousePosition = eventData.position;
            _dragging = true;
        }

        private void LateUpdate()
        {
            foreach (NodeSlot slot in Slots)
            {
                slot.PositionDirty = false;
            }

            if (_dragging)
            {
                foreach (NodeSlot slot in Slots)
                {
                    slot.PositionDirty = true;
                }

                transform.position = _startDragPosition + (Input.mousePosition - _startMousePosition);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = false;
        }
    }
}