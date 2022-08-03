using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Serializable]
    public class RectTransformLocationData
    {
        public RectTransformLocationType Type;
        public Vector2 AnchorMin;
        public Vector2 AnchorMax;
        public Vector2 Pivot;

        public RectTransformLocationData(RectTransformLocationType type, float anchorMinX, float anchorMinY, float anchorMaxX, float anchorMaxY, float pivotX, float pivotY)
        {
            Type = type;
            AnchorMin = new Vector2(anchorMinX, anchorMinY);
            AnchorMax = new Vector2(anchorMaxX, anchorMaxY);
            Pivot = new Vector2(pivotX, pivotY);
        }
    }
}