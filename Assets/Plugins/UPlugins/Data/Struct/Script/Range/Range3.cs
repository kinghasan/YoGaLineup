/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Range3.cs
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
    public struct Range3
    {
        public Range X;
        public Range Y;
        public Range Z;

        public Range3(Range x, Range y, Range z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Range3(Vector3 min, Vector3 max)
        {
            X.Min = min.x;
            X.Max = max.x;
            Y.Min = min.y;
            Y.Max = max.y;
            Z.Min = min.z;
            Z.Max = max.z;
        }

        public Vector3 Min
        {
            get => new Vector3(X.Min, Y.Min, Z.Min);
            set
            {
                X.Min = value.x;
                Y.Min = value.y;
                Z.Min = value.z;
            }
        }

        public Vector3 Max
        {
            get => new Vector3(X.Max, Y.Max, Z.Max);
            set
            {
                X.Max = value.x;
                Y.Max = value.y;
                Z.Max = value.z;
            }
        }

        public Vector3 Size
        {
            get => new Vector3(X.Size, Y.Size, Z.Size);
            set
            {
                X.Size = value.x;
                Y.Size = value.y;
                Z.Size = value.z;
            }
        }

        public Vector3 Center
        {
            get => new Vector3(X.Center, Y.Center, Z.Center);
            set
            {
                X.Center = value.x;
                Y.Center = value.y;
                Z.Center = value.z;
            }
        }

        public void SortMinMax()
        {
            X.SortMinMax();
            Y.SortMinMax();
            Z.SortMinMax();
        }

        public bool Contains(Vector3 point)
        {
            return X.Contains(point.x) && Y.Contains(point.y) && Z.Contains(point.z);
        }

        public Vector3 Closest(Vector3 point)
        {
            point.x = X.Closest(point.x);
            point.y = Y.Closest(point.y);
            point.z = Z.Closest(point.z);
            return point;
        }

        #region Intersects
        
        public bool Intersects(Range3 other)
        {
            return X.Intersects(other.X) && Y.Intersects(other.Y) && Z.Intersects(other.Z);
        }

        public Range3 GetIntersection(Range3 other)
        {
            other.X = X.GetIntersection(other.X);
            other.Y = Y.GetIntersection(other.Y);
            other.Z = Z.GetIntersection(other.Z);
            return other;
        }
        
        #endregion

        #region Distance

        public Vector3 SignedDistance3(Vector3 point)
        {
            point.x = X.SignedDistance(point.x);
            point.y = Y.SignedDistance(point.y);
            point.z = Z.SignedDistance(point.z);
            return point;
        }

        public Vector3 Distance3(Vector3 point)
        {
            point.x = X.Distance(point.x);
            point.y = Y.Distance(point.y);
            point.z = Z.Distance(point.z);
            return point;
        }

        public float SqrDistance(Vector3 point)
        {
            return Distance3(point).sqrMagnitude;
        }

        public float Distance(Vector3 point)
        {
            return Distance3(point).magnitude;
        } 

        #endregion

        #region Encapsulate

        public void Encapsulate(Vector3 point)
        {
            X.Encapsulate(point.x);
            Y.Encapsulate(point.y);
            Z.Encapsulate(point.z);
        }

        public void Encapsulate(Range3 other)
        {
            X.Encapsulate(other.X);
            Y.Encapsulate(other.Y);
            Z.Encapsulate(other.Z);
        } 

        #endregion

        public void Expand(Vector3 size)
        {
            X.Expand(size.x);
            Y.Expand(size.y);
            Z.Expand(size.z);
        }

        public void Expand(float size)
        {
            X.Expand(size);
            Y.Expand(size);
            Z.Expand(size);
        }

        public void Offset(Vector3 offset)
        {
            X.Offset(offset.x);
            Y.Offset(offset.y);
            Z.Offset(offset.z);
        }
    }
}