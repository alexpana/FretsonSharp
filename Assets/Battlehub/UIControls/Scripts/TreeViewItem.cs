using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
    public class ParentChangedEventArgs : EventArgs
    {
        public ParentChangedEventArgs(TreeViewItem oldParent, TreeViewItem newParent)
        {
            OldParent = oldParent;
            NewParent = newParent;
        }

        public TreeViewItem OldParent { get; }

        public TreeViewItem NewParent { get; }
    }

    public class TreeViewItem : ItemContainer
    {
        [SerializeField] private HorizontalLayoutGroup m_itemLayout;

        private bool m_canExpand;

        private TreeViewExpander m_expander;

        private bool m_isExpanded;

        private TreeViewItem m_parent;

        private Toggle m_toggle;
        private TreeView m_treeView;

        public int Indent { get; private set; }

        public TreeViewItem Parent
        {
            get => m_parent;
            set
            {
                if (m_parent == value) return;

                var oldParent = m_parent;
                m_parent = value;
                if (m_parent != null && m_treeView != null && m_itemLayout != null)
                {
                    Indent = m_parent.Indent + m_treeView.Indent;
                    m_itemLayout.padding = new RectOffset(
                        Indent,
                        m_itemLayout.padding.right,
                        m_itemLayout.padding.top,
                        m_itemLayout.padding.bottom);


                    var siblingIndex = transform.GetSiblingIndex();
                    SetIndent(this, ref siblingIndex);
                }
                else
                {
                    Indent = 0;
                    if (m_itemLayout != null)
                        m_itemLayout.padding = new RectOffset(
                            Indent,
                            m_itemLayout.padding.right,
                            m_itemLayout.padding.top,
                            m_itemLayout.padding.bottom);
                }

                if (m_treeView != null)
                    if (ParentChanged != null)
                        ParentChanged(this, new ParentChangedEventArgs(oldParent, m_parent));
            }
        }

        public override bool IsSelected
        {
            get => base.IsSelected;
            set
            {
                if (base.IsSelected != value)
                {
                    m_toggle.isOn = value;
                    base.IsSelected = value;
                }
            }
        }

        public bool CanExpand
        {
            get => m_canExpand;
            set
            {
                if (m_canExpand != value)
                {
                    m_canExpand = value;
                    if (m_expander != null) m_expander.CanExpand = m_canExpand;
                    if (!m_canExpand) IsExpanded = false;
                }
            }
        }

        public bool IsExpanded
        {
            get => m_isExpanded;
            set
            {
                if (m_isExpanded != value)
                {
                    m_isExpanded = value && m_canExpand;
                    if (m_expander != null) m_expander.IsOn = value && m_canExpand;
                    if (m_treeView != null)
                    {
                        if (m_isExpanded)
                            m_treeView.Expand(this);
                        else
                            m_treeView.Collapse(this);
                    }
                }
            }
        }

        public bool HasChildren
        {
            get
            {
                var index = transform.GetSiblingIndex();
                var nextItem = (TreeViewItem)m_treeView.GetItemContainer(index + 1);
                return nextItem != null && nextItem.Parent == this;
            }
        }

        public static event EventHandler<ParentChangedEventArgs> ParentChanged;

        private void SetIndent(TreeViewItem parent, ref int siblingIndex)
        {
            while (true)
            {
                var child = (TreeViewItem)m_treeView.GetItemContainer(siblingIndex + 1);
                if (child == null) return;

                if (child.Parent != parent) return;

                child.Indent = parent.Indent + m_treeView.Indent;
                child.m_itemLayout.padding.left = child.Indent;

                siblingIndex++;
                SetIndent(child, ref siblingIndex);
            }
        }

        public bool IsDescendantOf(TreeViewItem parent)
        {
            if (parent == null) return true;

            var testItem = this;
            while (testItem != null)
            {
                if (parent == testItem) return true;

                testItem = testItem.Parent;
            }

            return false;
        }

        public TreeViewItem FirstChild()
        {
            if (!HasChildren) return null;

            var siblingIndex = transform.GetSiblingIndex();
            siblingIndex++;
            var child = (TreeViewItem)m_treeView.GetItemContainer(siblingIndex);

            Debug.Assert(child != null && child.Parent == this);

            return child;
        }

        public TreeViewItem NextChild(TreeViewItem currentChild)
        {
            if (currentChild == null) throw new ArgumentNullException("currentChild");

            var siblingIndex = currentChild.transform.GetSiblingIndex();
            siblingIndex++;
            var nextChild = (TreeViewItem)m_treeView.GetItemContainer(siblingIndex);
            while (nextChild != null && nextChild.IsDescendantOf(this))
            {
                if (nextChild.Parent == this) return nextChild;

                siblingIndex++;
                nextChild = (TreeViewItem)m_treeView.GetItemContainer(siblingIndex);
            }

            return null;
        }

        public TreeViewItem LastChild()
        {
            if (!HasChildren) return null;

            var siblingIndex = transform.GetSiblingIndex();

            TreeViewItem lastChild = null;
            while (true)
            {
                siblingIndex++;
                var child = (TreeViewItem)m_treeView.GetItemContainer(siblingIndex);
                if (child == null || child.Parent != this) return lastChild;

                lastChild = child;
            }
        }

        protected override void AwakeOverride()
        {
            m_toggle = GetComponent<Toggle>();
            m_toggle.interactable = false;
            m_toggle.isOn = IsSelected;

            m_expander = GetComponentInChildren<TreeViewExpander>();
            if (m_expander != null) m_expander.CanExpand = m_canExpand;
        }

        protected override void StartOverride()
        {
            m_treeView = GetComponentInParent<TreeView>();
            if (IsExpanded) m_treeView.Expand(this);

            if (Parent != null)
            {
                Indent = Parent.Indent + m_treeView.Indent;
                m_itemLayout.padding.left = Indent;
            }
        }
    }
}