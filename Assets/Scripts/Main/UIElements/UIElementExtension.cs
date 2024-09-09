using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Main
{
    public static class UIElementExtension
    {
        public static IStyle Padding(this IStyle style, StyleLength? top = null, StyleLength? bottom = null, StyleLength? left = null, StyleLength? right = null)
        {
            if (top != null) style.paddingTop = top.Value;
            if (bottom != null) style.paddingBottom = bottom.Value;
            if (left != null) style.paddingLeft = left.Value;
            if (right != null) style.paddingRight = right.Value;
            return style;
        }
        public static IStyle Padding(this IStyle style, StyleLength padding)
        {
            return style.Padding(padding, padding, padding, padding);
        }
        public static IStyle Margin(this IStyle style, StyleLength? top = null, StyleLength? bottom = null, StyleLength? left = null, StyleLength? right = null)
        {
            if (top != null) style.marginTop = top.Value;
            if (bottom != null) style.marginBottom = bottom.Value;
            if (left != null) style.marginLeft = left.Value;
            if (right != null) style.marginRight = right.Value;
            return style;
        }
        public static IStyle Margin(this IStyle style, StyleLength padding)
        {
            return style.Margin(padding, padding, padding, padding);
        }
        public static IStyle Size(this IStyle style, StyleLength? width = null, StyleLength? height = null)
        {
            if (width != null) style.width = width.Value;
            if (height != null) style.height = height.Value;
            return style;
        }
        public static IStyle MinSize(this IStyle style, StyleLength? width = null, StyleLength? height = null)
        {
            if (width != null) style.minWidth = width.Value;
            if (height != null) style.minHeight = height.Value;
            return style;
        }
        public static IStyle MaxSize(this IStyle style, StyleLength? width = null, StyleLength? height = null)
        {
            if (width != null) style.maxWidth = width.Value;
            if (height != null) style.maxHeight = height.Value;
            return style;
        }
        public static IEnumerable<VisualElement> Parents(this VisualElement visualElement, bool includeSelf = false)
        {
            if (!includeSelf) visualElement = visualElement.parent;
            while (visualElement != null)
            {
                yield return visualElement;
                visualElement = visualElement.parent;
            }
        }
        public static IEnumerable<T> QuerryParent<T>(this VisualElement visualElement, bool includeSelf = false)
        {
            foreach (var v in visualElement.Parents(includeSelf))
                if (v is T typed) yield return typed;
        }
        public static T QParent<T>(this VisualElement visualElement, bool includeSelf = false)
            => visualElement.QuerryParent<T>(includeSelf).FirstOrDefault();
    }
}