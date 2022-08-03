﻿/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Bool2.cs
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
    public struct Bool2
    {
        public bool X;
        public bool Y;

        public bool AnyTrue => X || Y;
        public bool AllTrue => X && Y;

        public bool AnyFalse => !X || !Y;
        public bool AllFalse => !X && !Y;
    }
}
