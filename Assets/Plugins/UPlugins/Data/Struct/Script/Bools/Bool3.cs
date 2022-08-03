/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Bool3.cs
//  Info     : bool 组
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
    public struct Bool3
    {
        public bool X;
        public bool Y;
        public bool Z;

        public bool AnyTrue => X || Y || Z;
        public bool AllTrue => X && Y && Z;

        public bool AnyFalse => !X || !Y || !Z;
        public bool AllFalse => !X && !Y && !Z;
    }
}