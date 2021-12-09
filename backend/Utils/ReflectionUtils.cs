
using System;
using System.Reflection;

namespace ISO810_ERP.Utils;

public static class ReflectionUtils
{
    public static object? GetPropertyValue(object obj, string propertyName)
    {
        var type = obj.GetType();
        var property = type.GetProperty(propertyName);
        return property?.GetValue(obj);
    }

    public static void SetPropertyValue(object obj, string propertyName, object? value)
    {
        var type = obj.GetType();
        var property = type.GetProperty(propertyName);
        property?.SetValue(obj, value);
    }

    public static bool HasProperty(object obj, string propertyName)
    {
        var type = obj.GetType();
        var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        return property != null;
    }

    public static PropertyInfo? GetPropertyIgnoreCase(Type type, string propertyName)
    {
        return type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
    }
}