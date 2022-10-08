using System.Collections.Generic;
using UnityEngine;

namespace Manabreak.Core
{
    public class ObjectPool
    {
        public int InstanceCount = 0;
        public GameObject ObjectType;
        public List<GameObject> PooledObjects;
        public int PoolID;
        public List<GameObject> UnpooledObjects;

        public void InitPool(GameObject Object, int poolID, int NumToSpawn)
        {
            ObjectType = Object;
            PoolID = poolID;
            PooledObjects = new List<GameObject>();
            UnpooledObjects = new List<GameObject>();

            if (NumToSpawn > 0)
                for (var i = 0; i < NumToSpawn; i++)
                {
                    var obj = CreatePooledObject(Object);
                    PooledObjects.Add(obj);

                    // Tell poolable that it has been returned to pool
                    NotifyReturnToPool(obj);

                    obj.SetActive(false);
                }
        }

        private GameObject CreatePooledObject(GameObject ObjectPrefab)
        {
            var obj = Object.Instantiate(ObjectPrefab);
            // Find or add the pooled object component
            var poolComp = obj.GetComponent<PooledObjectComponent>();
            if (poolComp == null) poolComp = obj.AddComponent<PooledObjectComponent>();

            // Assign it this pools ID
            poolComp.PoolID = PoolID;
            poolComp.InstanceNumber = InstanceCount++;
            return obj;
        }

        public GameObject GetObjectFromPool(Vector3 Position, Quaternion Rotation, Vector3 Scale,
            bool ExpandPool = true)
        {
            GameObject obj = null;

            // Try to get pooled object
            if (PooledObjects.Count > 0)
            {
                obj = PooledObjects[0];
                PooledObjects.RemoveAt(0);
                UnpooledObjects.Add(obj);
            }
            // Create a new object if allowed
            else if (ExpandPool)
            {
                obj = CreatePooledObject(ObjectType);
                UnpooledObjects.Add(obj);
            }

            obj.transform.position = Position;
            obj.transform.rotation = Rotation;
            obj.transform.localScale = Scale;

            obj.SetActive(true);

            // Tell poolable that it has been spawned from pool
            NotifySpawnedFromPool(obj);

            return obj;
        }

        public bool ReturnObjectToPool(GameObject Object)
        {
            if (Object == null) return false;

            if (UnpooledObjects.Contains(Object)) UnpooledObjects.Remove(Object);

            PooledObjects.Add(Object);

            // Tell poolable that it has been returned to pool
            NotifyReturnToPool(Object);

            Object.SetActive(false);

            return true;
        }

        public bool IsObjectInPool(GameObject Object)
        {
            return PooledObjects.Contains(Object);
        }

        public bool IsObjectFromPool(GameObject Object)
        {
            return UnpooledObjects.Contains(Object);
        }

        public void EmptyPool()
        {
            RepoolAll();

            for (var i = 0; i < PooledObjects.Count; i++) Object.Destroy(PooledObjects[i]);

            PooledObjects.Clear();
            UnpooledObjects.Clear();
            InstanceCount = 0;
        }

        public void RepoolAll()
        {
            while (UnpooledObjects.Count > 0)
                // If this object is null, remove it manually
                if (!ReturnObjectToPool(UnpooledObjects[0]))
                    UnpooledObjects.RemoveAt(0);
        }

        protected void NotifySpawnedFromPool(GameObject Object)
        {
            var poolables = new List<IPoolableObject>(Object.GetComponents<IPoolableObject>());
            if (poolables.Count > 0)
                foreach (var p in poolables)
                    p.OnSpawnedFromPool();

            var pooledObj = Object.GetComponent<PooledObjectComponent>();
            if (pooledObj)
                if (pooledObj.OnSpawnedFromPool != null)
                    pooledObj.OnSpawnedFromPool.Invoke(pooledObj);

            var subPoolables = Object.GetComponentsInChildren<IPoolableObject>();
            if (subPoolables != null)
                foreach (var subPoolable in subPoolables)
                    if (!poolables.Contains(subPoolable))
                        subPoolable.OnSpawnedFromPool();
        }

        protected void NotifyReturnToPool(GameObject Object)
        {
            var poolables = new List<IPoolableObject>(Object.GetComponents<IPoolableObject>());
            if (poolables != null && poolables.Count > 0)
                foreach (var p in poolables)
                    p.OnReturnedToPool();

            var pooledObj = Object.GetComponent<PooledObjectComponent>();
            if (pooledObj)
                if (pooledObj.OnReturnedToPool != null)
                    pooledObj.OnReturnedToPool.Invoke(pooledObj);

            var subPoolables = Object.GetComponentsInChildren<IPoolableObject>();
            if (subPoolables != null)
                foreach (var subPoolable in subPoolables)
                    if (!poolables.Contains(subPoolable))
                        subPoolable.OnReturnedToPool();
        }
    }

    public interface IPoolableObject
    {
        void OnSpawnedFromPool();

        void OnReturnedToPool();
    }
}