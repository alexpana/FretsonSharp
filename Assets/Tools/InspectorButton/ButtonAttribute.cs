using System;

namespace EditorCools
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ButtonAttribute : Attribute
    {
        public readonly bool HasRow;
        public readonly string Name;
        public readonly string Row;
        public readonly float Space;

        public ButtonAttribute(string name = default, string row = default, float space = default)
        {
            Row = row;
            HasRow = !string.IsNullOrEmpty(Row);
            Name = name;
            Space = space;
        }
    }
}