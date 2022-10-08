using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class DropdownAttribute : PropertyAttribute
{
    public enum MethodLocation
    {
        PropertyClass,
        StaticClass
    }

    public DropdownAttribute(string methodName)
    {
        Location = MethodLocation.PropertyClass;
        MethodName = methodName;
    }

    public DropdownAttribute(Type methodOwner, string methodName)
    {
        Location = MethodLocation.StaticClass;
        MethodOwnerType = methodOwner;
        MethodName = methodName;
    }

    public MethodLocation Location { get; }
    public string MethodName { get; }
    public Type MethodOwnerType { get; }
}