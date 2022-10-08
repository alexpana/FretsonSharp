using UnityEngine;

namespace Battlehub.UIControls
{
    public enum ItemDropAction
    {
        None,
        SetLastChild,
        SetPrevSibling,
        SetNextSibling
    }

    [RequireComponent(typeof(RectTransform))]
    public class ItemDropMarker : MonoBehaviour
    {
        public GameObject SiblingGraphics;
        private ItemsControl m_itemsControl;

        protected Canvas ParentCanvas { get; private set; }

        public virtual ItemDropAction Action { get; set; }

        public RectTransform RectTransform { get; private set; }

        protected ItemContainer Item { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            SiblingGraphics.SetActive(true);
            ParentCanvas = GetComponentInParent<Canvas>();
            m_itemsControl = GetComponentInParent<ItemsControl>();
            AwakeOverride();
        }

        protected virtual void AwakeOverride()
        {
        }

        public virtual void SetTraget(ItemContainer item)
        {
            gameObject.SetActive(item != null);

            Item = item;
            if (Item == null) Action = ItemDropAction.None;
        }

        public virtual void SetPosition(Vector2 position)
        {
            if (Item == null) return;

            var rt = Item.RectTransform;
            Vector2 localPoint;

            Camera camera = null;
            if (ParentCanvas.renderMode == RenderMode.WorldSpace) camera = m_itemsControl.Camera;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, position, camera, out localPoint))
            {
                if (localPoint.y > -rt.rect.height / 2)
                {
                    Action = ItemDropAction.SetPrevSibling;
                    RectTransform.position = rt.position;
                }
                else
                {
                    Action = ItemDropAction.SetNextSibling;
                    RectTransform.position = rt.position - new Vector3(0, rt.rect.height, 0);
                }
            }
        }
    }
}