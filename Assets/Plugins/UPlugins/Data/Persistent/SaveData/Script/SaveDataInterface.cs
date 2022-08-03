/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveDataInterface.cs
//  Info     : 存档外部设置接口
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Security;

namespace Aya.Data.Persistent
{
    public static class SaveDataInterface
    {
        public static Func<string, string> Encrypt { get; set; } = RC4Util.Encrypt;
        public static Func<string, string> Decrypt { get; set; } = RC4Util.Decrypt;
        public static Func<string, string> GetMd5 { get; set; } = MD5Util.GetMd5;
    }
}
