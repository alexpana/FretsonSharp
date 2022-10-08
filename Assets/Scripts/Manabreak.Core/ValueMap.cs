using UnityEngine;

namespace Core
{
    /// <summary>
    ///     Interface describing an object that can hold a 2D array of generic values.
    ///     Used in multiple places that need to store data on a grid. Primary implementation is in
    ///     Manabreak.Mangudai.Graphics.ColorMap where the backing store for this data is both a grid
    ///     of values and a mapped color texture usable in-game.
    /// </summary>
    public interface ValueMap<T>
    {
        public void Clear();
        public void SetValue(int x, int y, T value);
        public void SetValue(RectInt rect, T value);
        public void ResetValue(int x, int y);
        public void Apply();
    }
}