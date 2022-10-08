using Manabreak.Core;
using UnityEngine;

namespace Core
{
    [Component]
    [DefaultExecutionOrder(-10)]
    public class PoolInitializerComponent : MonoBehaviour
    {
        private void Start()
        {
            var prefabManager = DI.Resolve<PrefabManager>();
            if (prefabManager == null) return;

            ObjectPoolManager.Clear();
            foreach (var definition in prefabManager.Prefabs)
                if (definition.PoolSize > 0)
                    ObjectPoolManager.InitPoolForGameobject(definition.Object, definition.PoolSize);
        }
    }
}