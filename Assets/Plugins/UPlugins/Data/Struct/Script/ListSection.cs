/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ListSection.cs
//  Info     : 列表切片
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;

namespace Aya.Data
{
    public struct ListSection<T>
    {
        public List<T> List { get; private set; }
        public int StartIndex;
        public int Length;

        public int Count => Length;

        public T this[int index]
        {
            get => List[StartIndex + index];
            set => List[StartIndex + index] = value;
        }

        public ListSection(List<T> list, int startIndex, int length)
        {
            List = list;
            StartIndex = startIndex;
            Length = length;
        }

        public static ListSection<T> GetSection(List<T> list, int startIndex, int length)
        {
            var result = new ListSection<T>(list, startIndex, length);
            return result;
        }
    }

    public static class ListSectionExtension
    {
        public static ListSection<T> GetSection<T>(this List<T> list, int startIndex, int length)
        {
            var result = ListSection<T>.GetSection(list, startIndex, length);
            return result;
        }
    }
}