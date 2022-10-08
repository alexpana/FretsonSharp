using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.UIControls
{
    public class ItemExpandingArgs : EventArgs
    {
        public ItemExpandingArgs(object item)
        {
            Item = item;
        }

        public object Item { get; }

        public IEnumerable Children { get; set; }
    }

    public class TreeViewItemDataBindingArgs : ItemDataBindingArgs
    {
        public bool HasChildren { get; set; }
    }

    public class TreeView : ItemsControl<TreeViewItemDataBindingArgs>
    {
        public int Indent = 20;

        private bool m_expandSilently;
        public event EventHandler<ItemExpandingArgs> ItemExpanding;

        protected override void OnEnableOverride()
        {
            base.OnEnableOverride();
            TreeViewItem.ParentChanged += OnTreeViewItemParentChanged;
        }

        protected override void OnDisableOverride()
        {
            base.OnDisableOverride();
            TreeViewItem.ParentChanged -= OnTreeViewItemParentChanged;
        }

        public void AddChild(object parent, object item)
        {
            if (parent == null)
            {
                Add(item);
            }
            else
            {
                var parentContainer = (TreeViewItem)GetItemContainer(parent);
                if (parentContainer == null) return;

                var index = -1;
                if (parentContainer.IsExpanded)
                {
                    if (parentContainer.HasChildren)
                    {
                        var lastChild = parentContainer.LastChild();
                        index = IndexOf(lastChild.Item) + 1;
                    }
                    else
                    {
                        index = IndexOf(parentContainer.Item) + 1;
                    }
                }
                else
                {
                    parentContainer.CanExpand = true;
                }

                if (index > -1)
                {
                    var addedItem = (TreeViewItem)Insert(index, item);
                    addedItem.Parent = parentContainer;
                }
            }
        }

        public void ChangeParent(object parent, object item)
        {
            if (IsDropInProgress) return;

            var dragItem = GetItemContainer(item);
            if (dragItem == null) return;

            var dropTarget = GetItemContainer(parent);
            ItemContainer[] dragItems = { dragItem };
            if (CanDrop(dragItems, dropTarget)) Drop(dragItems, dropTarget, ItemDropAction.SetLastChild);
        }

        public void Expand(TreeViewItem item)
        {
            if (m_expandSilently) return;

            if (ItemExpanding != null)
            {
                var args = new ItemExpandingArgs(item.Item);
                ItemExpanding(this, args);

                var children = args.Children;
                var containerIndex = item.transform.GetSiblingIndex();
                var itemIndex = IndexOf(item.Item);

                item.CanExpand = children != null;

                if (item.CanExpand)
                {
                    foreach (var childItem in children)
                    {
                        containerIndex++;
                        itemIndex++;

                        var childContainer = (TreeViewItem)InstantiateItemContainer(containerIndex);
                        childContainer.Parent = item;
                        childContainer.Item = childItem;

                        InsertItem(itemIndex, childItem);
                        DataBindItem(childItem, childContainer);
                    }

                    UpdateSelectedItemIndex();
                }
            }
        }

        public void Collapse(TreeViewItem item)
        {
            var containerIndex = item.transform.GetSiblingIndex();
            var itemIndex = IndexOf(item.Item);

            if (SelectedItems != null)
            {
                var selectedItems = SelectedItems.OfType<object>().ToList();
                var refContainerIndex = containerIndex + 1;
                var refItemIndex = itemIndex + 1;
                Unselect(selectedItems, item, ref refContainerIndex, ref refItemIndex);
                SelectedItems = selectedItems;
            }

            Collapse(item, containerIndex + 1, itemIndex + 1);
        }

        private void Unselect(List<object> selectedItems, TreeViewItem item, ref int containerIndex, ref int itemIndex)
        {
            while (true)
            {
                var child = (TreeViewItem)GetItemContainer(containerIndex);
                if (child == null || child.Parent != item) break;
                containerIndex++;
                itemIndex++;
                selectedItems.Remove(child.Item);
                Unselect(selectedItems, child, ref containerIndex, ref itemIndex);
            }
        }

        private void Collapse(TreeViewItem item, int containerIndex, int itemIndex)
        {
            while (true)
            {
                var child = (TreeViewItem)GetItemContainer(containerIndex);
                if (child == null || child.Parent != item) break;

                Collapse(child, containerIndex + 1, itemIndex + 1);
                RemoveItemAt(itemIndex);
                DestroyItemContainer(containerIndex);
            }
        }

        protected override ItemContainer InstantiateItemContainerOverride(GameObject container)
        {
            var itemContainer = container.GetComponent<TreeViewItem>();
            if (itemContainer == null)
            {
                itemContainer = container.AddComponent<TreeViewItem>();
                itemContainer.gameObject.name = "TreeViewItem";
            }

            return itemContainer;
        }

        protected override void DestroyItem(object item)
        {
            var itemContainer = (TreeViewItem)GetItemContainer(item);
            if (itemContainer != null)
            {
                Collapse(itemContainer);
                base.DestroyItem(item);
                if (itemContainer.Parent != null && !itemContainer.Parent.HasChildren)
                    itemContainer.Parent.CanExpand = false;
            }
        }

        protected override void DataBindItem(object item, ItemContainer itemContainer)
        {
            var args = new TreeViewItemDataBindingArgs();
            args.Item = item;
            args.ItemPresenter = itemContainer.gameObject;
            RaiseItemDataBinding(args);

            var treeViewItem = (TreeViewItem)itemContainer;
            treeViewItem.CanExpand = args.HasChildren;
        }

        protected override bool CanDrop(ItemContainer[] dragItems, ItemContainer dropTarget)
        {
            if (!base.CanDrop(dragItems, dropTarget)) return false;

            var tvDropTarget = (TreeViewItem)dropTarget;
            if (tvDropTarget == null) return true;

            foreach (var dragItem in dragItems)
            {
                var tvDragItem = (TreeViewItem)dragItem;
                if (tvDropTarget.IsDescendantOf(tvDragItem)) return false;
            }

            return true;
        }

        private void OnTreeViewItemParentChanged(object sender, ParentChangedEventArgs e)
        {
            var tvItem = (TreeViewItem)sender;
            if (!CanHandleEvent(tvItem)) return;

            var oldParent = e.OldParent;
            if (oldParent != null && !oldParent.HasChildren) oldParent.CanExpand = false;

            if (DropMarker.Action != ItemDropAction.SetLastChild && DropMarker.Action != ItemDropAction.None) return;

            var tvDropTarget = e.NewParent;
            if (tvDropTarget != null)
            {
                if (tvDropTarget.CanExpand)
                {
                    tvDropTarget.IsExpanded = true;
                }
                else
                {
                    tvDropTarget.CanExpand = true;
                    m_expandSilently = true;
                    tvDropTarget.IsExpanded = true;
                    m_expandSilently = false;
                }
            }

            var dragItemChild = tvItem.FirstChild();
            TreeViewItem lastChild = null;
            if (tvDropTarget != null)
            {
                lastChild = tvDropTarget.LastChild();
                if (lastChild == null) lastChild = tvDropTarget;
            }
            else
            {
                lastChild = (TreeViewItem)LastItemContainer();
            }

            if (lastChild != tvItem) DropItemAfter(lastChild, tvItem);

            if (dragItemChild != null) MoveSubtree(tvItem, dragItemChild);
        }

        private void MoveSubtree(TreeViewItem parent, TreeViewItem child)
        {
            var parentSiblingIndex = parent.transform.GetSiblingIndex();
            var siblingIndex = child.transform.GetSiblingIndex();
            var incrementSiblingIndex = false;
            if (parentSiblingIndex < siblingIndex) incrementSiblingIndex = true;

            var prev = parent;
            while (child != null && child.IsDescendantOf(parent))
            {
                if (prev == child) break;
                DropItemAfter(prev, child);
                prev = child;
                if (incrementSiblingIndex) siblingIndex++;
                child = (TreeViewItem)GetItemContainer(siblingIndex);
            }
        }

        protected override void Drop(ItemContainer[] dragItems, ItemContainer dropTarget, ItemDropAction action)
        {
            var tvDropTarget = (TreeViewItem)dropTarget;
            if (action == ItemDropAction.SetLastChild)
                for (var i = 0; i < dragItems.Length; ++i)
                {
                    var tvDragItem = (TreeViewItem)dragItems[i];
                    tvDragItem.Parent = tvDropTarget;
                }
            else if (action == ItemDropAction.SetPrevSibling)
                for (var i = 0; i < dragItems.Length; ++i)
                {
                    var tvDragItem = (TreeViewItem)dragItems[i];
                    var dragItemChild = tvDragItem.FirstChild();

                    DropItemBefore(tvDropTarget, tvDragItem);
                    if (dragItemChild != null) MoveSubtree(tvDragItem, dragItemChild);

                    tvDragItem.Parent = tvDropTarget.Parent;
                }
            else if (action == ItemDropAction.SetNextSibling)
                for (var i = dragItems.Length - 1; i >= 0; --i)
                {
                    var tvDragItem = (TreeViewItem)dragItems[i];
                    var dragItemChild = tvDragItem.FirstChild();

                    DropItemAfter(tvDropTarget, tvDragItem);
                    if (dragItemChild != null) MoveSubtree(tvDragItem, dragItemChild);

                    tvDragItem.Parent = tvDropTarget.Parent;
                }

            UpdateSelectedItemIndex();
        }
    }
}