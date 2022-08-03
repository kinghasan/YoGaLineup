/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Bool4.cs
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
    public struct Bool4
    {
        public bool X;
        public bool Y;
        public bool Z;
        public bool W;

        public bool AnyTrue => X || Y || Z || W;
        public bool AllTrue => X && Y && Z && W;

        public bool AnyFalse => !X || !Y || !Z || !W;
        public bool AllFalse => !X && !Y && !Z && !W;
    }
}