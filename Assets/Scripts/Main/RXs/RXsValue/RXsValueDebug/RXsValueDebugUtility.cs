using System;
using UnityEngine;
using System.Reflection;

namespace Main.RXs
{
    public class DisableRXsValueDebugAttribute : Attribute { }
    public static class RXsValueDebugUtility
    {
        public static IDisposable EnableDebug(this IRXsCollection_Readonly collection, string name = null)
        {
            return new DisposableList(
                collection.AfterAdd.Order(int.MinValue).Subscribe(e => Debug.Log($"{name} : AddItem <color=green>{e.Item ?? "Null"}</color> at <color=green>{e.Index}</color>")),
                collection.AfterRemove.Order(int.MinValue).Subscribe(e => Debug.Log($"{name} : RemoveItem <color=green>{e.Item ?? "Null"}</color> at <color=green>{e.Index}</color>"))
                );
        }
        public static IDisposable EnableDebug(this IRXsProperty_Readonly property, string name = null)
        {
            return property.AfterSet.Order(int.MinValue).Subscribe(e => Debug.Log($"{name} : Set from <color=green>{e.Previous ?? "Null"}</color> to <color=green>{e.Current ?? "Null"}</color>"));
        }
        public static IDisposable EnableDebug(object obj, DisposableList disposableList = null)
        {
            disposableList ??= new();
            disposableList.Dispose();
            var type = obj.GetType();
            while (type != null)
            {
                foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (fieldInfo.GetCustomAttribute<DisableRXsValueDebugAttribute>() != null) continue;
                    var value = fieldInfo.GetValue(obj);
                    if (value is IRXsProperty_Readonly property)
                    {
                        disposableList.Add(property.EnableDebug($"<color=white>{obj}</color> <color=yellow>{fieldInfo.Name()}</color>"));
                        continue;
                    }
                    if (value is IRXsCollection_Readonly collection)
                    {
                        disposableList.Add(collection.EnableDebug($"<color=white>{obj}</color> <color=yellow>{fieldInfo.Name()}</color>"));
                        continue;
                    }
                }
                type = type.BaseType;
            }
            return disposableList;
        }
    }
}