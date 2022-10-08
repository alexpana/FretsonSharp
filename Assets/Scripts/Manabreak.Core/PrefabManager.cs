using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    [CreateAssetMenu(menuName = "RTS/PrefabManager")]
    public class PrefabManager : ScriptableObject
    {
        [TableList] public List<PrefabDefinition> Prefabs;

        public List<PrefabDefinition> GetPrefabsWithComponent<T>()
        {
            var result = new List<PrefabDefinition>();
            foreach (var prefabDefinition in Prefabs)
            {
                if (prefabDefinition.Object == null) continue;

                if (prefabDefinition.Object.GetComponentInChildren(typeof(T)) != null) result.Add(prefabDefinition);
            }

            return result;
        }

        public GameObject GetPrefab(int id)
        {
            foreach (var prefabDefinition in Prefabs.Where(prefabDefinition => prefabDefinition.Id == id))
                return GetVariant(prefabDefinition);

            throw new PrefabNotFoundException(name);
        }

        public GameObject GetPrefab(string prefabName)
        {
            foreach (var prefabDefinition in Prefabs.Where(prefabDefinition =>
                         prefabDefinition.Object.name == prefabName)) return GetVariant(prefabDefinition);

            throw new PrefabNotFoundException(prefabName);
        }

        private static GameObject GetVariant(PrefabDefinition definition)
        {
            var prefabCount = 1 + definition.Variants.Length;
            var selected = Random.Range(0, prefabCount);
            if (selected == 0) return definition.Object;

            return definition.Variants[selected - 1];
        }

        public class PrefabNotFoundException : Exception
        {
            public PrefabNotFoundException(string prefabName) : base($"Could not find prefab with name {prefabName}")
            {
            }
        }

        [Serializable]
        public class PrefabDefinition
        {
            [TableColumnWidth(40, false)] public int Id;
            public GameObject Object;
            public GameObject[] Variants;
            [TableColumnWidth(40, false)] public bool AutoInstantiate = false;
            [TableColumnWidth(80, false)] public int PoolSize = 0;
        }
    }
}