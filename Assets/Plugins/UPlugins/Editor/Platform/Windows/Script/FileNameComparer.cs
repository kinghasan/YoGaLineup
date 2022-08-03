/////////////////////////////////////////////////////////////////////////////
//
//  Script : FileNameComparer.cs
//  Info   : Windows 系统自带的文件名排序比较器
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR && UNITY_STANDALONE_WIN
using System.Collections;
using System.Runtime.InteropServices;

namespace Aya.EditorScript
{
    public class FileNameComparer : IComparer
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string param1, string param2);

        public int Compare(object name1, object name2)
        {
            switch (name1)
            {
                case null when null == name2:
                    return 0;
                case null:
                    return -1;
            }

            return null == name2 ? 1 : StrCmpLogicalW(name1.ToString(), name2.ToString());
        }
    }
}
#endif