using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [ExecuteAlways]
    public class ShapesTextPool : MonoBehaviour
    {
        private const int ALLOCATION_COUNT_WARNING = 500;
        private const int ALLOCATION_COUNT_CAP = 1000;
        private static ShapesTextPool instance;
        private readonly Dictionary<int, TextMeshPro> elementsActive = new();
        private readonly Stack<TextMeshPro> elementsPassive = new();

        private int ElementCount => elementsPassive.Count + elementsActive.Count;
        public TextMeshPro ImmediateModeElement => GetElement(-1);

        public static int InstanceElementCount => InstanceExists ? Instance.ElementCount : 0;
        public static int InstanceElementCountActive => InstanceExists ? Instance.elementsActive.Count : 0;
        public static bool InstanceExists => instance != null;

        public static ShapesTextPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ShapesTextPool>();
                    if (instance == null)
                        instance = CreatePool();
                }

                return instance;
            }
        }

        private void OnEnable()
        {
            ClearData();
            instance = this;
        }

        private void OnDisable()
        {
            ClearData();
        }

        private static ShapesTextPool CreatePool()
        {
            var holder = new GameObject("Shapes Text Pool");
            if (Application.isPlaying)
                DontDestroyOnLoad(holder); // might be a lil gross, not sure
            var text = holder.AddComponent<ShapesTextPool>();
            holder.hideFlags = HideFlags.HideAndDontSave;
            return text;
        }

        private void ClearData()
        {
            // clear any residual children if things reload
            for (var i = transform.childCount - 1; i >= 0; i--)
                transform.GetChild(i).gameObject.DestroyBranched();
            elementsPassive.Clear();
            elementsActive.Clear();
        }

        public TextMeshPro GetElement(int id)
        {
            if (elementsActive.TryGetValue(id, out var tmp) == false)
                tmp = AllocateElement(id);
            return tmp;
        }

        public TextMeshPro AllocateElement(int id)
        {
            TextMeshPro elem = null;
            // try find non-null passive elements
            while (elem == null && elementsPassive.Count > 0)
                elem = elementsPassive.Pop();

            // if no passive elment found, create it
            if (elem == null)
                elem = CreateElement(id);

            // assign it to the active list
            elementsActive.Add(id, elem);
            return elem;
        }

        public void ReleaseElement(int id)
        {
            if (elementsActive.TryGetValue(id, out var tmp))
            {
                elementsActive.Remove(id);
                elementsPassive.Push(tmp);
            }
        }

        private TextMeshPro CreateElement(int id)
        {
            var totalCount = ElementCount;
            if (totalCount > ALLOCATION_COUNT_CAP)
            {
                Debug.LogError(
                    $"Text element allocation cap of {ALLOCATION_COUNT_CAP} reached. You are probably leaking and not properly disposing text elements");
                return null;
            }

            if (totalCount > ALLOCATION_COUNT_WARNING)
                Debug.LogWarning(
                    $"Allocating more than {ALLOCATION_COUNT_WARNING} text elements. You are probably leaking and not properly disposing text objects");

            var elem = new GameObject(id == -1 ? "Immediate Mode Text" : id.ToString());
            elem.transform.SetParent(transform, false);
            elem.transform.localPosition = Vector3.zero;
            elem.hideFlags = HideFlags.HideAndDontSave;

            var tmp = elem.AddComponent<TextMeshPro>();
            tmp.enableWordWrapping = false;
            tmp.overflowMode = TextOverflowModes.Overflow;

            // mesh renderer should exist now due to TMP requiring the component
            tmp.GetComponent<MeshRenderer>().enabled = false;

            return tmp;
        }
    }
}