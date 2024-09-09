using System;
using UnityEngine;

namespace Main
{
    partial class ValueExtension
    {
        public const int LimitOrder = -1000;
        //Value
        public static IDisposable LimitMax(this IProperty<int> property, int max, int order = LimitOrder)
            => property.BeforeSet.Order(order).Subscribe(e => e.Modified = Mathf.Min(e.Previous, max));
        public static IDisposable LimitMax(this IProperty<float> property, float max, int order = LimitOrder)
            => property.BeforeSet.Order(order).Subscribe(e => e.Modified = Mathf.Min(e.Previous, max));
        public static IDisposable LimitMin(this IProperty<int> property, int min, int order = LimitOrder)
            => property.BeforeSet.Order(order).Subscribe(e => e.Modified = Mathf.Max(e.Previous, min));
        public static IDisposable LimitMin(this IProperty<float> property, float min, int order = LimitOrder)
            => property.BeforeSet.Order(order).Subscribe(e => e.Modified = Mathf.Max(e.Previous, min));
        //Propery
        public static IDisposable LimitMax(this IProperty<int> property, IPropertyReadonly<int> max, int order = LimitOrder)
            => Disposable.Create(
            property.BeforeSet.Order(order).Subscribe(e => e.Modified = Mathf.Min(e.Previous, max.Value)),
            max.AfterSet.Order(order).Subscribe(e => property.Value = Mathf.Min(property.Value, e.Current)));
        public static IDisposable LimitMax(this IProperty<float> property, IPropertyReadonly<float> max, int order = LimitOrder)
            => Disposable.Create(
            property.BeforeSet.Order(order).Subscribe(e => e.Modified = Mathf.Min(e.Previous, max.Value)),
            max.AfterSet.Order(order).Subscribe(e => property.Value = Mathf.Min(property.Value, e.Current)));
        public static IDisposable LimitMin(this IProperty<int> property, IPropertyReadonly<int> min, int order = LimitOrder)
            => Disposable.Create(
            property.BeforeSet.Order(order).Subscribe(e => e.Modified = Mathf.Max(e.Previous, min.Value)),
            min.AfterSet.Order(order).Subscribe(e => property.Value = Mathf.Max(property.Value, e.Current)));
        public static IDisposable LimitMin(this IProperty<float> property, IPropertyReadonly<float> min, int order = LimitOrder)
            => Disposable.Create(
            property.BeforeSet.Order(order).Subscribe(e => e.Modified = Mathf.Max(e.Previous, min.Value)),
            min.AfterSet.Order(order).Subscribe(e => property.Value = Mathf.Max(property.Value, e.Current)));
    }
}