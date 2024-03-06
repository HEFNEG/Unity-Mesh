using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooper.UI {
    public class ScrollItem : UIElement {
        public virtual Vector2 SizeDelta => RectTransform.sizeDelta;

        public virtual void Show<T>(T data) {

        }
    }
}
