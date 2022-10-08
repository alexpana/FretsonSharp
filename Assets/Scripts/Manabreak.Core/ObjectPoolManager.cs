using System.Collections.Generic;
using UnityEngine;

namespace Manabreak.Core
{
    public class ObjectPoolManager
    {
        public static Dictionary<GameObject, ObjectPool> ObjectPoolsByPrefab = new();
        public static Dictionary<int, ObjectPool> ObjectPoolsByID = new();
        public static int NumPools = 0;

        public static void Clear()
        {
            ObjectPoolsByPrefab.Clear();
            ObjectPoolsByID.Clear();
            NumPools = 0;
        }

        /// <summary>
        ///     Initializes a new pool for the given gameobject prefab and fills it with pooled instances.
        /// </summary>
        /// <param name="ObjectPrefab">The object prefab.</param>
        /// <param name="NumToSpawn">The number of instances to fill the pool with.</param>
        public static void InitPoolForGameobject(GameObject ObjectPrefab, int NumToSpawn)
        {
            Debug.Log($"Initializing pool for {ObjectPrefab}");
            var pool = new ObjectPool();
            pool.InitPool(ObjectPrefab, NumPools, NumToSpawn);
            ObjectPoolsByPrefab.Add(ObjectPrefab, pool);
            ObjectPoolsByID.Add(NumPools, pool);
            NumPools++;
        }

        /// <summary>
        ///     Spawns the provided prefab from pool and returns the component type specified.  If no pool for that prefab exists,
        ///     one will be created.
        /// </summary>
        /// <typeparam name="T">Component type to return.</typeparam>
        /// <param name="ObjectPrefab">The object prefab.</param>
        /// <returns></returns>
        public static T Spawn<T>(GameObject ObjectPrefab)
        {
            var pooledObject = Spawn(ObjectPrefab);
            if (pooledObject != null) return pooledObject.GetComponent<T>();

            return default;
        }

        /// <summary>
        ///     Spawns the provided prefab from pool and returns the component type specified.  If no pool for that prefab exists,
        ///     one will be created.
        /// </summary>
        /// <typeparam name="T">Component type to return</typeparam>
        /// <param name="ObjectPrefab">The object prefab.</param>
        /// <param name="Position">The position.</param>
        /// <returns></returns>
        public static T Spawn<T>(GameObject ObjectPrefab, Vector3 Position)
        {
            var pooledObject = Spawn(ObjectPrefab, Position);
            if (pooledObject != null) return pooledObject.GetComponent<T>();

            return default;
        }

        /// <summary>
        ///     Spawns the provided prefab from pool and returns the component type specified.  If no pool for that prefab exists,
        ///     one will be created.
        /// </summary>
        /// <typeparam name="T">Component type to return</typeparam>
        /// <param name="ObjectPrefab">The object prefab.</param>
        /// <param name="Position">The position.</param>
        /// <param name="Rotation">The rotation.</param>
        /// <returns></returns>
        public static T Spawn<T>(GameObject ObjectPrefab, Vector3 Position, Quaternion Rotation)
        {
            var pooledObject = Spawn(ObjectPrefab, Position, Rotation);
            if (pooledObject != null) return pooledObject.GetComponent<T>();

            return default;
        }

        /// <summary>
        ///     Spawns the provided prefab from pool and returns the component type specified.  If no pool for that prefab exists,
        ///     one will be created.
        /// </summary>
        /// <typeparam name="T">Component type to return</typeparam>
        /// <param name="ObjectPrefab">The object prefab.</param>
        /// <param name="Position">The position.</param>
        /// <param name="Rotation">The rotation.</param>
        /// <param name="Scale">The scale.</param>
        /// <returns></returns>
        public static T Spawn<T>(GameObject ObjectPrefab, Vector3 Position, Quaternion Rotation, Vector3 Scale)
        {
            var pooledObject = Spawn(ObjectPrefab, Position, Rotation, Scale);
            if (pooledObject != null) return pooledObject.GetComponent<T>();

            return default;
        }

        /// <summary>
        ///     Spawns the provided prefab from pool.  If no pool for that prefab exists, one will be created.
        ///     Note that the OnSpawnedFromPool interface call on the pooled object will be called after the transform is set.
        /// </summary>
        /// <param name="ObjectPrefab">The prefab to spawn.</param>
        /// <param name="Position">The world position to spawn at.</param>
        /// <param name="Rotation">The world rotation to spawn at.</param>
        /// <param name="Scale">The local scale to spawn at.</param>
        /// <returns>An instance of the prefab.</returns>
        public static GameObject Spawn(GameObject ObjectPrefab, Vector3 Position, Quaternion Rotation, Vector3 Scale)
        {
            GameObject pooledObject = null;
            // If we already have a pool for this object
            if (ObjectPoolsByPrefab.ContainsKey(ObjectPrefab))
            {
                var pool = ObjectPoolsByPrefab[ObjectPrefab];
                pooledObject = pool.GetObjectFromPool(Position, Rotation, Scale);
            }

            // Otherwise, create a new pool for this prefab
            else
            {
                var pool = new ObjectPool();
                pool.InitPool(ObjectPrefab, NumPools, 1);
                ObjectPoolsByPrefab.Add(ObjectPrefab, pool);
                ObjectPoolsByID.Add(NumPools, pool);
                NumPools++;

                pooledObject = pool.GetObjectFromPool(Position, Rotation, Scale);
            }

            return pooledObject;
        }

        /// <summary>
        ///     Spawns the provided prefab from pool.  If no pool for that prefab exists, one will be created.
        ///     Local scale defaults to prefab's local scale.
        ///     Note that the OnSpawnedFromPool interface call on the pooled object will be called after the transform is set.
        /// </summary>
        /// <param name="ObjectPrefab">The prefab to spawn.</param>
        /// <param name="Position">The world position to spawn at.</param>
        /// <param name="Rotation">The world rotation to spawn at.</param>
        /// <returns>An instance of the prefab.</returns>
        public static GameObject Spawn(GameObject ObjectPrefab, Vector3 Position, Quaternion Rotation)
        {
            var pooledObject = Spawn(ObjectPrefab, Position, Rotation, ObjectPrefab.transform.localScale);

            return pooledObject;
        }

        /// <summary>
        ///     Spawns the provided prefab from pool.  If no pool for that prefab exists, one will be created.
        ///     Rotation defaults to Identity and local scale defaults to prefab's local scale.
        ///     Note that the OnSpawnedFromPool interface call on the pooled object will be called after the position is set.
        /// </summary>
        /// <param name="ObjectPrefab">The prefab to spawn.</param>
        /// <param name="Position">The world position to spawn at.</param>
        /// <returns>An instance of the prefab.</returns>
        public static GameObject Spawn(GameObject ObjectPrefab, Vector3 Position)
        {
            var pooledObject = Spawn(ObjectPrefab, Position, Quaternion.identity, ObjectPrefab.transform.localScale);

            return pooledObject;
        }

        /// <summary>
        ///     Spawns the provided prefab from pool.  If no pool for that prefab exists, one will be created.
        ///     Position defaults to Vector3.zero, rotation defaults to Identity and local scale defaults to prefab's local scale.
        /// </summary>
        /// <param name="ObjectPrefab">The prefab to spawn.</param>
        /// <returns>An instance of the prefab.</returns>
        public static GameObject Spawn(GameObject ObjectPrefab)
        {
            var pooledObject = Spawn(ObjectPrefab, Vector3.zero, Quaternion.identity,
                ObjectPrefab.transform.localScale);

            return pooledObject;
        }

        /// <summary>
        ///     Returns the gameobject instance to its pool.  If the gameobject wasnt spawned via the pooling system, it does
        ///     nothing.
        /// </summary>
        /// <param name="Object">The object instance to repool.</param>
        /// <returns>True if the instance was successfully repooled, False if the instance was not spawned via the pooling system.</returns>
        public static bool Release(GameObject Object)
        {
            var pooledComp = Object.GetComponent<PooledObjectComponent>();

            // If this object was not spawned from the pool system, abort
            if (pooledComp == null) return false;

            if (ObjectPoolsByID.ContainsKey(pooledComp.PoolID))
            {
                var pool = ObjectPoolsByID[pooledComp.PoolID];
                pool.ReturnObjectToPool(Object);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Determines whether the given gameobject instance is currently in a pool.
        /// </summary>
        /// <param name="Object">The gameobject.</param>
        /// <returns>
        ///     <c>true</c> if the gameobject is currently in a pool; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsObjectInPool(GameObject Object)
        {
            var pooledComp = Object.GetComponent<PooledObjectComponent>();

            // If this object was not spawned from the pool system, its not in any pool
            if (pooledComp == null)
                return false;
            if (pooledComp.PoolID < 0) return false;

            if (ObjectPoolsByID.ContainsKey(pooledComp.PoolID))
                return ObjectPoolsByID[pooledComp.PoolID].IsObjectInPool(Object);

            return false;
        }

        /// <summary>
        ///     Determines whether the given gameobject instance was created from a pool
        /// </summary>
        /// <param name="Object">The gameobject.</param>
        /// <returns>
        ///     <c>true</c> if the gameobject is currently in a pool; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsObjectFromPool(GameObject Object)
        {
            var pooledComp = Object.GetComponent<PooledObjectComponent>();

            // If this object was not spawned from the pool system, its not in any pool
            if (pooledComp == null)
                return false;
            if (pooledComp.PoolID < 0) return false;

            if (ObjectPoolsByID.ContainsKey(pooledComp.PoolID))
                return ObjectPoolsByID[pooledComp.PoolID].IsObjectFromPool(Object);

            return false;
        }

        /// <summary>
        ///     Repools all active instances of the given type.
        /// </summary>
        /// <param name="ObjectPrefab">The object prefab.</param>
        public static void RepoolAllOfType(GameObject ObjectPrefab)
        {
            var pool = GetPoolForObject(ObjectPrefab);
            if (pool != null) pool.RepoolAll();
        }

        /// <summary>
        ///     Repools all active instances managed by the pooling system.
        /// </summary>
        public static void RepoolAll()
        {
            foreach (var Entry in ObjectPoolsByID) Entry.Value.RepoolAll();
        }

        /// <summary>
        ///     Destroys all pooled and/or active instances of the given prefab.  Returns them to pool before destroying.
        /// </summary>
        /// <param name="ObjectPrefab">The object prefab.</param>
        public static void EmptyPoolByType(GameObject ObjectPrefab)
        {
            var pool = GetPoolForObject(ObjectPrefab);
            if (pool != null) pool.EmptyPool();
        }

        /// <summary>
        ///     Destroys all pooled and/or active instances of all pools.  Returns them to pool before destroying.
        /// </summary>
        public static void EmptyAllPools()
        {
            foreach (var Entry in ObjectPoolsByID) Entry.Value.EmptyPool();
        }

        /// <summary>
        ///     Gets the pool for the given object.
        /// </summary>
        /// <param name="Object">The object, either the prefab or an instance.</param>
        /// <returns>The ObjectPool handling this gameobject type</returns>
        public static ObjectPool GetPoolForObject(GameObject Object)
        {
            // If the provided object is a prefab
            if (ObjectPoolsByPrefab.ContainsKey(Object)) return ObjectPoolsByPrefab[Object];

            // If the provided object is an instance
            var pooledComp = Object.GetComponent<PooledObjectComponent>();

            // If this object was not spawned from the pool system, abort
            if (pooledComp == null) return null;

            if (ObjectPoolsByID.ContainsKey(pooledComp.PoolID)) return ObjectPoolsByID[pooledComp.PoolID];

            return null;
        }

        public static List<GameObject> GetAllUnpooledInstancesOfType(GameObject ObjectPrefab)
        {
            var pool = GetPoolForObject(ObjectPrefab);
            if (pool == null) return null;

            return pool.UnpooledObjects;
        }
    }
}