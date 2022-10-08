using System.Collections.Generic;
using System.Linq;
using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTEditor
{
    public delegate void ExposeToEditorChangeEvent<T>(ExposeToEditor obj, T oldValue, T newValue);

    public delegate void ExposeToEditorEvent(ExposeToEditor obj);

    [DisallowMultipleComponent]
    public class ExposeToEditor : MonoBehaviour
    {
        public bool AddColliders = false;

        public bool DisableOnAwake = false;
        private bool m_applicationQuit;
        private readonly List<ExposeToEditor> m_children = new();

        private Collider[] m_colliders;
        private HierarchyItem m_hierarchyItem;

        private ExposeToEditor m_parent;
#if UNITY_EDITOR
        private SaveInPlayMode m_saveInPlayMode;
#endif
        public int ChildCount => m_children.Count;

        public ExposeToEditor Parent
        {
            get => m_parent;
            set
            {
                if (m_parent != value)
                {
                    var oldParent = m_parent;
                    m_parent = value;

                    if (oldParent != null) oldParent.m_children.Remove(this);

                    if (m_parent != null) m_parent.m_children.Add(this);

                    if (ParentChanged != null) ParentChanged(this, oldParent, m_parent);
                }
            }
        }

        private void Awake()
        {
            if (DisableOnAwake) gameObject.SetActive(false);

            var colliders = new List<Collider>();
            var filter = GetComponent<MeshFilter>();
            var rigidBody = GetComponent<Rigidbody>();

            var isRigidBody = rigidBody != null;
            if (filter != null)
                if (!isRigidBody && AddColliders)
                {
                    var collider = gameObject.AddComponent<MeshCollider>();
                    collider.convex = isRigidBody;
                    collider.sharedMesh = filter.mesh;
                    colliders.Add(collider);
                }

            var skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer != null)
                if (!isRigidBody && AddColliders)
                {
                    var collider = gameObject.AddComponent<MeshCollider>();
                    collider.convex = isRigidBody;
                    collider.sharedMesh = skinnedMeshRenderer.sharedMesh;
                    colliders.Add(collider);
                }

            m_colliders = colliders.ToArray();

            if (transform.parent != null)
            {
                var parent = transform.parent.GetComponentInParent<ExposeToEditor>();
                if (m_parent != parent)
                {
                    m_parent = parent;
                    if (m_parent != null) m_parent.m_children.Add(this);
                }
            }

            m_hierarchyItem = gameObject.GetComponent<HierarchyItem>();
            if (m_hierarchyItem == null) m_hierarchyItem = gameObject.AddComponent<HierarchyItem>();

            if (Awaked != null) Awaked(this);
        }

        private void Start()
        {
#if UNITY_EDITOR
            m_saveInPlayMode = GetComponentInParent<SaveInPlayMode>();

            if (m_saveInPlayMode != null)
            {
                m_saveInPlayMode.ScheduleDestroy(m_hierarchyItem);
                for (var i = 0; i < m_colliders.Length; ++i) m_saveInPlayMode.ScheduleDestroy(m_colliders[i]);
            }
#endif


            if (Started != null) Started(this);
        }

        private void Update()
        {
            if (TransformChanged != null)
                if (transform.hasChanged)
                {
                    transform.hasChanged = false;
                    if (TransformChanged != null) TransformChanged(this);
                }
        }

        private void OnEnable()
        {
            if (Enabled != null) Enabled(this);
        }

        private void OnDisable()
        {
            if (Disabled != null) Disabled(this);
        }

        private void OnDestroy()
        {
            if (!m_applicationQuit)
            {
                Parent = null;

#if UNITY_EDITOR
                if (m_saveInPlayMode == null)
#endif
                {
                    for (var i = 0; i < m_colliders.Length; ++i)
                    {
                        var collider = m_colliders[i];
                        if (collider != null) Destroy(collider);
                    }

                    if (m_hierarchyItem != null) Destroy(m_hierarchyItem);
                }

                if (Destroyed != null) Destroyed(this);
            }
        }

        private void OnApplicationQuit()
        {
            m_applicationQuit = true;
        }

        public static event ExposeToEditorEvent NameChanged;
        public static event ExposeToEditorEvent TransformChanged;
        public static event ExposeToEditorEvent Awaked;
        public static event ExposeToEditorEvent Started;
        public static event ExposeToEditorEvent Enabled;
        public static event ExposeToEditorEvent Disabled;
        public static event ExposeToEditorEvent Destroyed;
        public static event ExposeToEditorChangeEvent<ExposeToEditor> ParentChanged;

        public ExposeToEditor GetChild(int index)
        {
            return m_children[index];
        }

        public ExposeToEditor[] GetChildren()
        {
            return m_children.OrderBy(c => c.transform.GetSiblingIndex()).ToArray();
        }

        public void SetName(string name)
        {
            gameObject.name = name;
            if (NameChanged != null) NameChanged(this);
        }
    }
}