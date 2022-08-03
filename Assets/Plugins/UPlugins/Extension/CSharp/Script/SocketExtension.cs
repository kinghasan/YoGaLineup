/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SocketExtension.cs
//  Info     : Socket 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System.Net.Sockets;

namespace Aya.Extension
{
    public static class SocketExtension
    {
        /// <summary>
        /// 是否连接
        /// </summary>
        /// <param name="socket">套接字</param>
        /// <returns>结果</returns>
        public static bool IsConnected(this Socket socket)
        {
            var part1 = socket.Poll(1000, SelectMode.SelectRead);
            var part2 = (socket.Available == 0);
            var result = part1 & part2;
            return result;
        }
    }
}
