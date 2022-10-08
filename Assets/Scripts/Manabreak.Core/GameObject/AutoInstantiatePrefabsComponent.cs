using UnityEngine;

namespace Core
{
    [Component]
    [DefaultExecutionOrder(-10)]
    public class AutoInstantiatePrefabsComponent : MonoBehaviour
    {
        private void Start()
        {
            var prefabManager = DI.Resolve<PrefabManager>();
            if (prefabManager == null) return;

            foreach (var definition in prefabManager.Prefabs)
                if (definition.AutoInstantiate)
                    Instantiate(definition.Object);
        }
    }
}