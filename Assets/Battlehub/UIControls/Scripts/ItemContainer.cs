using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
    public delegate void ItemEventHandler(ItemContainer sender, PointerEventData eventData);

    [RequireComponent(typeof(RectTransform), typeof(LayoutElement))]
    public class ItemContainer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
        IPointerExitHandler, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        public bool CanDrag = true;

        private bool m_isSelected;

        public LayoutElement LayoutElement { get; private set; }

        public RectTransform RectTransform { get; private set; }

        public virtual bool IsSelected
        {
            get => m_isSelected;
            set
            {
                if (m_isSelected != value)
                {
                    m_isSelected = value;
                    if (m_isSelected)
                    {
                        if (Selected != null) Selected(this, EventArgs.Empty);
                    }
                    else
                    {
                        if (Unselected != null) Unselected(this, EventArgs.Empty);
                    }
                }
            }
        }

        public object Item { get; set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            LayoutElement = GetComponent<LayoutElement>();
            AwakeOverride();
        }

        private void Start()
        {
            StartOverride();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;
            if (BeginDrag != null) BeginDrag(this, eventData);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;
            if (Drag != null) Drag(this, eventData);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            if (!CanDrag) return;
            if (Drop != null) Drop(this, eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;
            if (EndDrag != null) EndDrag(this, eventData);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (PointerDown != null) PointerDown(this, eventData);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (PointerEnter != null) PointerEnter(this, eventData);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (PointerExit != null) PointerExit(this, eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (PointerUp != null) PointerUp(this, eventData);
        }

        public static event EventHandler Selected;
        public static event EventHandler Unselected;
        public static event ItemEventHandler PointerDown;
        public static event ItemEventHandler PointerUp;
        public static event ItemEventHandler PointerEnter;
        public static event ItemEventHandler PointerExit;
        public static event ItemEventHandler BeginDrag;
        public static event ItemEventHandler Drag;
        public static event ItemEventHandler Drop;
        public static event ItemEventHandler EndDrag;

        protected virtual void AwakeOverride()
        {
        }

        protected virtual void StartOverride()
        {
        }
    }
}