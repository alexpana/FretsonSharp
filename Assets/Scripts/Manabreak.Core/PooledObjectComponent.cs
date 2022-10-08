using System;
using UnityEngine;

namespace Manabreak.Core
{
    public class PooledObjectComponent : MonoBehaviour
    {
        public int PoolID = -1;
        public int InstanceNumber = 0;
        public Action<PooledObjectComponent> OnReturnedToPool;
        public Action<PooledObjectComponent> OnSpawnedFromPool;
    }
}