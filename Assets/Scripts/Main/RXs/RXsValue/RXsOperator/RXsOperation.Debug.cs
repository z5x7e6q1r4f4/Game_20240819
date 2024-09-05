using System;
using UnityEngine;
using System.Reflection;
using NUnit.Framework;
using System.Collections.Generic;

namespace Main.RXs
{
    public class DisableRXsValueDebugAttribute : Attribute { }
    partial class Operation
    {
        public static IRXsDisposable EnableDebug(this IRXsCollection_Readonly collection, string name = null)
        {
            return RXsSubscription.FromList(
                collection.AfterAdd.Order(int.MinValue).EnableDebug(name),
                collection.AfterRemove.Order(int.MinValue).EnableDebug(name)
                );
        }
        public static IRXsDisposable EnableDebug(this IRXsProperty_Readonly property, string name = null)
        {
            return property.AfterSet.Order(int.MinValue).EnableDebug(name);
        }
        public static IRXsDisposable EnableDebug(object obj)
        {
            List<IRXsDisposable> disposableList = new();
            var type = obj.GetType();
            while (type != null)
            {
                foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (fieldInfo.GetCustomAttribute<DisableRXsValueDebugAttribute>() != null) continue;
                    var value = fieldInfo.GetValue(obj);
                    if (value is IRXsProperty_Readonly property)
                    {
                        disposableList.Add(property.EnableDebug($"<color=white>{obj}</color> <color=green>{fieldInfo.Name()}</color>"));
                        continue;
                    }
                    if (value is IRXsCollection_Readonly collection)
                    {
                        disposableList.Add(collection.EnableDebug($"<color=white>{obj}</color> <color=green>{fieldInfo.Name()}</color>"));
                        continue;
                    }
                }
                type = type.BaseType;
            }
            return RXsSubscription.FromList(disposableList);
        }
    }
}