/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Range.cs
//  Info     : 范围
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Data
{
    [Serializable]
    public struct Range
    {
        public float Min;
        public float Max;

        public Range(float min, float max)
        {
            this.Min = min;
            this.Max = max;
        }

        public float Size
        {
            get => Max - Min;
            set
            {
                float center2 = Min + Max;
                Min = (center2 - value) * 0.5f;
                Max = (center2 + value) * 0.5f;
            }
        }

        public float Center
        {
            get => (Min + Max) * 0.5f;
            set
            {
                var half = (Max - Min) * 0.5f;
                Min = value - half;
                Max = value + half;
            }
        }

        public void SortMinMax()
        {
            if (!(Min > Max)) return;
            var tmp = Max;
            Max = Min;
            Min = tmp;
        }

        public bool Contains(float value)
        {
            return value >= Min && value <= Max;
        }

        public float Closest(float value)
        {
            if (value <= Min) return Min;
            if (value >= Max) return Max;
            return value;
        } 

        #region Intersects

        public bool Intersects(Range other)
        {
            return Min <= other.Max && Max >= other.Min;
        }

        public Range GetIntersection(Range other)
        {
            if (Min > other.Min) other.Min = Min;
            if (Max < other.Max) other.Max = Max;
            return other;
        } 

        #endregion

        #region Distance

        public float SignedDistance(float value)
        {
            if (value < Min) return value - Min;
            if (value > Max) return value - Max;
            return 0f;
        }

        public float Distance(float value)
        {
            if (value < Min) return Min - value;
            if (value > Max) return value - Max;
            return 0f;
        }

        #endregion

        #region Encapsulate
        
        public void Encapsulate(float value)
        {
            if (value < Min) Min = value;
            else if (value > Max) Max = value;
        }

        public void Encapsulate(Range other)
        {
            if (other.Min < Min) Min = other.Min;
            if (other.Max > Max) Max = other.Max;
        } 

        #endregion

        public void Expand(float size)
        {
            Min -= size;
            Max += size;
        }

        public void Offset(float offset)
        {
            Min += offset;
            Max += offset;
        }
    }
}
