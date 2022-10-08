using UnityEngine;

namespace Battlehub.UIControls
{
    [RequireComponent(typeof(RectTransform))]
    public class TreeViewDropMarker : ItemDropMarker
    {
        public GameObject ChildGraphics;
        private RectTransform m_siblingGraphicsRectTransform;
        private TreeView m_treeView;

        public override ItemDropAction Action
        {
            get => base.Action;
            set
            {
                base.Action = value;
                ChildGraphics.SetActive(base.Action == ItemDropAction.SetLastChild);
                SiblingGraphics.SetActive(base.Action != ItemDropAction.SetLastChild);
            }
        }

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_treeView = GetComponentInParent<TreeView>();
            m_siblingGraphicsRectTransform = SiblingGraphics.GetComponent<RectTransform>();
        }

        public override void SetTraget(ItemContainer item)
        {
            base.SetTraget(item);
            if (item == null) return;

            var tvItem = (TreeViewItem)item;
            if (tvItem != null)
                m_siblingGraphicsRectTransform.offsetMin =
                    new Vector2(tvItem.Indent, m_siblingGraphicsRectTransform.offsetMin.y);
            else
                m_siblingGraphicsRectTransform.offsetMin = new Vector2(0, m_siblingGraphicsRectTransform.offsetMin.y);
        }

        public override void SetPosition(Vector2 position)
        {
            if (Item == null) return;

            var rt = Item.RectTransform;
            var tvItem = (TreeViewItem)Item;
            Vector2 localPoint;

            Camera camera = null;
            if (ParentCanvas.renderMode == RenderMode.WorldSpace) camera = m_treeView.Camera;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, position, camera, out localPoint))
            {
                if (localPoint.y > -rt.rect.height / 4)
                {
                    Action = ItemDropAction.SetPrevSibling;
                    RectTransform.position = rt.position;
                }
                else if (localPoint.y < rt.rect.height / 4 - rt.rect.height && !tvItem.HasChildren)
                {
                    Action = ItemDropAction.SetNextSibling;
                    RectTransform.position = rt.position - new Vector3(0, rt.rect.height, 0);
                }
                else
                {
                    Action = ItemDropAction.SetLastChild;
                    RectTransform.position = rt.position;
                }
            }
        }
    }
}