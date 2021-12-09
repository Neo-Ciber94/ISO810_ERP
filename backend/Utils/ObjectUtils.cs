
using System.Linq;

namespace ISO810_ERP.Utils;

public static class ObjectUtils
{
    /// <summary>
    /// Copies all the non-null properties from object source to object target.
    /// 
    /// <para>
    /// This function is do not check if the properties type matches but only the names.
    /// </para>
    /// </summary>
    /// <typeparam name="TSource">Type of the object to copy</typeparam>
    /// <typeparam name="TTarget">Type of the object to copy</typeparam>
    public static TTarget UpdateNonNullProperties<TSource, TTarget>(TSource source, TTarget target) where TSource : class where TTarget : class
    {
        var sourceProperties = source.GetType().GetProperties();
        var targetProperties = target.GetType().GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var targetProperty = targetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

            if (targetProperty != null && sourceProperty.GetValue(source) != null)
            {
                targetProperty.SetValue(target, sourceProperty.GetValue(source));
            }
        }

        return target;
    }
}