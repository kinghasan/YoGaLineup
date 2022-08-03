/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Range2.cs
//  Info     : 范围
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Data
{
    [Serializable]
    public struct Range2
    {
        public Range X;
        public Range Y;

        public Range2(Range x, Range y)
        {
            this.X = x;
            this.Y = y;
        }

        public Range2(Vector2 min, Vector2 max)
        {
            X.Min = min.x;
            X.Max = max.x;
            Y.Min = min.y;
            Y.Max = max.y;
        }

        public Vector2 Min
        {
            get => new Vector2(X.Min, Y.Min);
            set
            {
                X.Min = value.x;
                Y.Min = value.y;
            }
        }

        public Vector2 Max
        {
            get => new Vector2(X.Max, Y.Max);
            set
            {
                X.Max = value.x;
                Y.Max = value.y;
            }
        }

        public Vector2 Size
        {
            get => new Vector2(X.Size, Y.Size);
            set
            {
                X.Size = value.x;
                Y.Size = value.y;
            }
        }

        public Vector2 center
        {
            get => new Vector2(X.Center, Y.Center);
            set
            {
                X.Center = value.x;
                Y.Center = value.y;
            }
        }

        public void SortMinMax()
        {
            X.SortMinMax();
            Y.SortMinMax();
        }

        public bool Contains(Vector2 point)
        {
            return X.Contains(point.x) && Y.Contains(point.y);
        }

        public Vector2 Closest(Vector2 point)
        {
            point.x = X.Closest(point.x);
            point.y = Y.Closest(point.y);
            return point;
        }

        #region Intersects
        
        public bool Intersects(Range2 other)
        {
            return X.Intersects(other.X) && Y.Intersects(other.Y);
        }

        public Range2 GetIntersection(Range2 other)
        {
            other.X = X.GetIntersection(other.X);
            other.Y = Y.GetIntersection(other.Y);
            return other;
        }

        #endregion

        #region Distance

        public Vector2 SignedDistance2(Vector2 point)
        {
            point.x = X.SignedDistance(point.x);
            point.y = Y.SignedDistance(point.y);
            return point;
        }

        public Vector2 Distance2(Vector2 point)
        {
            point.x = X.Distance(point.x);
            point.y = Y.Distance(point.y);
            return point;
        }

        public float SqrDistance(Vector2 point)
        {
            return Distance2(point).sqrMagnitude;
        }

        public float Distance(Vector2 point)
        {
            return Distance2(point).magnitude;
        } 

        #endregion

        #region Encapsulate

        public void Encapsulate(Vector2 point)
        {
            X.Encapsulate(point.x);
            Y.Encapsulate(point.y);
        }

        public void Encapsulate(Range2 other)
        {
            X.Encapsulate(other.X);
            Y.Encapsulate(other.Y);
        } 

        #endregion

        public void Expand(Vector2 size)
        {
            X.Expand(size.x);
            Y.Expand(size.y);
        }

        public void Expand(float size)
        {
            X.Expand(size);
            Y.Expand(size);
        }

        public void Offset(Vector2 offset)
        {
            X.Offset(offset.x);
            Y.Offset(offset.y);
        }
    }
}
