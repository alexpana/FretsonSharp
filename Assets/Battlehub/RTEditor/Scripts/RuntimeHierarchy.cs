using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battlehub.UIControls;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Battlehub.RTEditor
{
    public class RuntimeHierarchy : MonoBehaviour
    {
        public GameObject TreeViewPrefab;
        public Color DisabledItemColor = new(0.5f, 0.5f, 0.5f);
        public Color EnabledItemColor = new(0.2f, 0.2f, 0.2f);

        private bool m_lockSelection;
        private TreeView m_treeView;
        public Type TypeCriteria = typeof(GameObject);

        private void Start()
        {
            if (!TreeViewPrefab)
            {
                Debug.LogError("Set TreeViewPrefab field");
                return;
            }

            m_treeView = Instantiate(TreeViewPrefab).GetComponent<TreeView>();
            m_treeView.transform.SetParent(transform, false);

            m_treeView.ItemDataBinding += OnItemDataBinding;
            m_treeView.SelectionChanged += OnSelectionChanged;
            m_treeView.ItemsRemoved += OnItemsRemoved;
            m_treeView.ItemExpanding += OnItemExpanding;
            m_treeView.ItemBeginDrag += OnItemBeginDrag;
            m_treeView.ItemDrop += OnItemDrop;
            m_treeView.ItemEndDrag += OnItemEndDrag;

            RuntimeSelection.SelectionChanged += OnRuntimeSelectionChanged;
#if UNITY_EDITOR
            Selection.selectionChanged += OnEditorSelectionChanged;
#endif

            var filtered = new HashSet<GameObject>();
            var objects = Resources.FindObjectsOfTypeAll<GameObject>();
            for (var i = 0; i < objects.Length; ++i)
            {
                var obj = objects[i];
                if (obj == null) continue;

                if (!RuntimePrefabs.IsPrefab(obj.transform))
                {
                    if (TypeCriteria == typeof(GameObject))
                    {
                        filtered.Add(obj);
                    }
                    else
                    {
                        var component = obj.GetComponent(TypeCriteria);
                        if (component)
                            if (!filtered.Contains(component.gameObject))
                                filtered.Add(component.gameObject);
                    }
                }
            }

            m_treeView.Items = filtered.Where(f => f.transform.parent == null && CanExposeToEditor(f))
                .OrderBy(t => t.transform.GetSiblingIndex());

            ExposeToEditor.Awaked += OnObjectAwaked;
            ExposeToEditor.Started += OnObjectStarted;
            ExposeToEditor.Enabled += OnObjectEnabled;
            ExposeToEditor.Disabled += OnObjectDisabled;
            ExposeToEditor.Destroyed += OnObjectDestroyed;
            ExposeToEditor.ParentChanged += OnParentChanged;
            ExposeToEditor.NameChanged += OnNameChanged;
        }

        private void OnDestroy()
        {
            if (!m_treeView) return;
            m_treeView.ItemDataBinding -= OnItemDataBinding;
            m_treeView.SelectionChanged -= OnSelectionChanged;
            m_treeView.ItemsRemoved -= OnItemsRemoved;
            m_treeView.ItemExpanding -= OnItemExpanding;
            m_treeView.ItemBeginDrag -= OnItemBeginDrag;
            m_treeView.ItemDrop -= OnItemDrop;
            m_treeView.ItemEndDrag -= OnItemEndDrag;

            RuntimeSelection.SelectionChanged -= OnRuntimeSelectionChanged;
#if UNITY_EDITOR
            Selection.selectionChanged -= OnEditorSelectionChanged;
#endif

            ExposeToEditor.Awaked -= OnObjectAwaked;
            ExposeToEditor.Started -= OnObjectStarted;
            ExposeToEditor.Enabled -= OnObjectEnabled;
            ExposeToEditor.Disabled -= OnObjectDisabled;
            ExposeToEditor.Destroyed -= OnObjectDestroyed;
            ExposeToEditor.ParentChanged -= OnParentChanged;
            ExposeToEditor.NameChanged -= OnNameChanged;
        }

        private void OnApplicationQuit()
        {
            ExposeToEditor.Awaked -= OnObjectAwaked;
            ExposeToEditor.Started -= OnObjectStarted;
            ExposeToEditor.Enabled -= OnObjectEnabled;
            ExposeToEditor.Disabled -= OnObjectDisabled;
            ExposeToEditor.Destroyed -= OnObjectDestroyed;
            ExposeToEditor.ParentChanged -= OnParentChanged;
            ExposeToEditor.NameChanged -= OnNameChanged;
        }

        private bool CanExposeToEditor(GameObject go)
        {
            var exposeToEditor = go.GetComponent<ExposeToEditor>();
            return exposeToEditor != null;
        }

        private void OnItemExpanding(object sender, ItemExpandingArgs e)
        {
            var gameObject = (GameObject)e.Item;
            var exposeToEditor = gameObject.GetComponent<ExposeToEditor>();

            if (exposeToEditor.ChildCount > 0)
            {
                e.Children = exposeToEditor.GetChildren().Select(obj => obj.gameObject);

                //This line is required to syncronize selection, runtime selection and treeview selection
                OnTreeViewSelectionChanged(m_treeView.SelectedItems, m_treeView.SelectedItems);
            }
        }

        private void OnEditorSelectionChanged()
        {
            if (m_lockSelection) return;
            m_lockSelection = true;

#if UNITY_EDITOR
            RuntimeSelection.activeObject = Selection.activeGameObject;
            RuntimeSelection.objects = Selection.objects;
            m_treeView.SelectedItems = Selection.gameObjects;
#endif

            m_lockSelection = false;
        }

        private void OnRuntimeSelectionChanged(Object[] unselected)
        {
            if (m_lockSelection) return;
            m_lockSelection = true;

#if UNITY_EDITOR
            if (RuntimeSelection.objects == null)
            {
                Selection.objects = new Object[0];
            }
            else
            {
                Selection.activeObject = RuntimeSelection.activeObject;
                Selection.objects = RuntimeSelection.objects;
            }
#endif
            m_treeView.SelectedItems = RuntimeSelection.gameObjects;

            m_lockSelection = false;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnTreeViewSelectionChanged(e.OldItems, e.NewItems);
        }

        private void OnTreeViewSelectionChanged(IEnumerable oldItems, IEnumerable newItems)
        {
            if (m_lockSelection) return;

            m_lockSelection = true;

            if (newItems == null) newItems = new GameObject[0];

#if UNITY_EDITOR
            Selection.objects = newItems.OfType<GameObject>().ToArray();
#endif

            RuntimeSelection.objects = newItems.OfType<GameObject>().ToArray();

            m_lockSelection = false;
        }

        private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
        {
            for (var i = 0; i < e.Items.Length; ++i)
            {
                var go = (GameObject)e.Items[i];
                if (go != null) Destroy(go);
            }
        }

        private void OnItemDataBinding(object sender, TreeViewItemDataBindingArgs e)
        {
            var dataItem = e.Item as GameObject;
            if (dataItem != null)
            {
                var text = e.ItemPresenter.GetComponentInChildren<Text>(true);
                text.text = dataItem.name;
                if (dataItem.activeInHierarchy)
                    text.color = EnabledItemColor;
                else
                    text.color = DisabledItemColor;

                e.HasChildren = dataItem.GetComponent<ExposeToEditor>().ChildCount > 0;
            }
        }

        private void OnItemBeginDrag(object sender, ItemDragArgs e)
        {
        }

        private void OnItemDrop(object sender, ItemDropArgs e)
        {
            if (e.IsExternal)
            {
                if (e.DragItems != null)
                    for (var i = 0; i < e.DragItems.Length; ++i)
                    {
                        var prefab = e.DragItems[i] as GameObject;
                        if (prefab != null)
                            if (RuntimePrefabs.IsPrefab(prefab.transform))
                            {
                                var prefabInstance = Instantiate(prefab);
                                var exposeToEditor = prefabInstance.GetComponent<ExposeToEditor>();
                                if (exposeToEditor != null) exposeToEditor.SetName(prefab.name);
                                prefabInstance.transform.position = prefab.transform.position;
                                prefabInstance.transform.rotation = prefab.transform.rotation;
                                prefabInstance.transform.localScale = prefab.transform.localScale;
                                RuntimeSelection.activeGameObject = prefabInstance;
                            }
                    }
            }
            else
            {
                var dropT = ((GameObject)e.DropTarget).transform;
                if (e.Action == ItemDropAction.SetLastChild)
                    for (var i = 0; i < e.DragItems.Length; ++i)
                    {
                        var dragT = ((GameObject)e.DragItems[i]).transform;
                        dragT.SetParent(dropT, true);
                        dragT.SetAsLastSibling();
                    }
                else if (e.Action == ItemDropAction.SetNextSibling)
                    for (var i = 0; i < e.DragItems.Length; ++i)
                    {
                        var dragT = ((GameObject)e.DragItems[i]).transform;
                        if (dragT.parent != dropT.parent) dragT.SetParent(dropT.parent, true);

                        var siblingIndex = dropT.GetSiblingIndex();
                        dragT.SetSiblingIndex(siblingIndex + 1);
                    }
                else if (e.Action == ItemDropAction.SetPrevSibling)
                    for (var i = 0; i < e.DragItems.Length; ++i)
                    {
                        var dragT = ((GameObject)e.DragItems[i]).transform;
                        if (dragT.parent != dropT.parent) dragT.SetParent(dropT.parent, true);

                        var siblingIndex = dropT.GetSiblingIndex();
                        dragT.SetSiblingIndex(siblingIndex);
                    }
            }
        }

        private void OnItemEndDrag(object sender, ItemDragArgs e)
        {
        }

        private void OnObjectAwaked(ExposeToEditor obj)
        {
            GameObject parent = null;
            if (obj.Parent != null) parent = obj.Parent.gameObject;
            m_treeView.AddChild(parent, obj.gameObject);
        }

        private void OnObjectStarted(ExposeToEditor obj)
        {
        }

        private void OnObjectEnabled(ExposeToEditor obj)
        {
            var tvItem = (TreeViewItem)m_treeView.GetItemContainer(obj.gameObject);
            if (tvItem == null) return;
            var text = tvItem.GetComponentInChildren<Text>();
            text.color = EnabledItemColor;
        }

        private void OnObjectDisabled(ExposeToEditor obj)
        {
            var tvItem = (TreeViewItem)m_treeView.GetItemContainer(obj.gameObject);
            if (tvItem == null) return;
            var text = tvItem.GetComponentInChildren<Text>();
            text.color = DisabledItemColor;
        }

        private void OnObjectDestroyed(ExposeToEditor obj)
        {
            m_treeView.Remove(obj.gameObject);
        }

        private void OnParentChanged(ExposeToEditor obj, ExposeToEditor oldParent, ExposeToEditor newParent)
        {
            GameObject parent = null;
            if (newParent != null) parent = newParent.gameObject;

            m_treeView.ChangeParent(parent, obj.gameObject);
        }

        private void OnNameChanged(ExposeToEditor obj)
        {
            var tvItem = (TreeViewItem)m_treeView.GetItemContainer(obj.gameObject);
            if (tvItem == null) return;
            var text = tvItem.GetComponentInChildren<Text>();
            text.text = obj.gameObject.name;
        }
    }
}