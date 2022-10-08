using System.Collections.Generic;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    internal static class ArrayPool<T>
    {
        private static readonly Stack<T[]> pool = new();

        public static T[] Alloc(int maxCount)
        {
            return pool.Count == 0 ? new T[maxCount] : pool.Pop();
        }

        public static void Free(T[] obj)
        {
            pool.Push(obj);
        }
    }

    internal static class ObjectPool<T> where T : new()
    {
        private static readonly Stack<T> pool = new();

        public static T Alloc()
        {
            return pool.Count == 0 ? new T() : pool.Pop();
        }

        public static void Free(T obj)
        {
            pool.Push(obj);
        }
    }

    internal static class ListPool<T> where T : new()
    {
        private static readonly Stack<List<T>> pool = new();

        public static List<T> Alloc()
        {
            return pool.Count == 0 ? new List<T>() : pool.Pop();
        }

        public static void Free(List<T> list)
        {
            list.Clear();
            pool.Push(list);
        }
    }
}