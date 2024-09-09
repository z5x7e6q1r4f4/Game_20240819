using System;
using System.Reflection;
using System.Linq;

namespace Main
{
    public class DisableValueDebugAttribute : Attribute { }
    partial class ValueExtension
    {
        public static IDisposable EnableDebug(this ICollectionReadonly collection, string name = null)
        {
            return Disposable.Create(
                collection.AfterAdd.Order(int.MinValue).EnableDebug(name),
                collection.AfterRemove.Order(int.MinValue).EnableDebug(name)
                );
        }
        public static IDisposable EnableDebug(this IPropertyReadonly property, string name = null)
        {
            return property.AfterSet.Order(int.MinValue).EnableDebug(name);
        }
        public static IDisposable EnableDebugAllValueFrom(object obj)
        {
            using var disposables = Reuse.ReuseList<IDisposable>.Get();
            var type = obj.GetType();
            while (type != null)
            {
                foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (fieldInfo.GetCustomAttribute<DisableValueDebugAttribute>() != null) continue;
                    var value = fieldInfo.GetValue(obj);
                    if (value is IPropertyReadonly property)
                    {
                        disposables.Add(property.EnableDebug($"<color=white>{obj}</color> <color=green>{fieldInfo.Name()}</color>"));
                        continue;
                    }
                    if (value is ICollectionReadonly collection)
                    {
                        disposables.Add(collection.EnableDebug($"<color=white>{obj}</color> <color=green>{fieldInfo.Name()}</color>"));
                        continue;
                    }
                }
                type = type.BaseType;
            }
            return Disposable.Create(disposables.AsEnumerable());
        }
    }
}