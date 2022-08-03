/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Tuple.cs
//  Info     : 元组
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////

namespace Aya.Data
{
    public class Tuple<T1, T2>
    {
        public readonly T1 Item1;
        public readonly T2 Item2;

        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Tuple<T1, T2> tuple)) return false;
            if (Item1 == null)
            {
                if (tuple.Item1 != null) return false;
            }
            else
            {
                if (tuple.Item1 == null || !Item1.Equals(tuple.Item1)) return false;
            }

            if (Item2 == null)
            {
                if (tuple.Item2 != null) return false;
            }
            else
            {
                if (tuple.Item2 == null || !Item2.Equals(tuple.Item2)) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = 0;
            if (Item1 != null)
            {
                hash ^= Item1.GetHashCode();
            }

            if (Item2 != null)
            {
                hash ^= Item2.GetHashCode();
            }

            return hash;
        }
    }

    public static class Tuple
    {
        public static Tuple<T1, T2> Create<T1, T2>(T1 t1, T2 t2)
        {
            return new Tuple<T1, T2>(t1, t2);
        }
    }
}
