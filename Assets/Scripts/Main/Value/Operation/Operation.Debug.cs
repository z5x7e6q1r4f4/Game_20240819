using System;
using UnityEngine;
using System.Reflection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    public class DisableRXsValueDebugAttribute : Attribute { }
    partial class Operation
    {
        public static IDisposable EnableDebug(this IObservableCollection_Readonly collection, string name = null)
        {
            return Disposable.Create(
                collection.AfterAdd.Order(int.MinValue).EnableDebug(name),
                collection.AfterRemove.Order(int.MinValue).EnableDebug(name)
                );
        }
        public static IDisposable EnableDebug(this IObservableProperty_Readonly property, string name = null)
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
                    if (fieldInfo.GetCustomAttribute<DisableRXsValueDebugAttribute>() != null) continue;
                    var value = fieldInfo.GetValue(obj);
                    if (value is IObservableProperty_Readonly property)
                    {
                        disposables.Add(property.EnableDebug($"<color=white>{obj}</color> <color=green>{fieldInfo.Name()}</color>"));
                        continue;
                    }
                    if (value is IObservableCollection_Readonly collection)
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