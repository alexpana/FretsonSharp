using System;

namespace Core
{
    public class AutoBind : Attribute
    {
        public readonly string Name;

        public AutoBind(string name = null)
        {
            Name = name;
        }
    }
}