/////////////////////////////////////////////////////////////////////////////
//
//  Script : ShellUtil.cs
//  Info   : Shell 接口封装
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using System.Diagnostics;
using UnityEngine;

namespace Aya.EditorScript
{
    public static class ShellUtil
    {
        /// <summary>
        /// 创建一次脚本执行
        /// </summary>
        /// <param name="filePath">脚本文件路径</param>
        /// <param name="args">参数</param>
        /// <param name="workingDir">工作目录</param>
        /// <returns></returns>
        private static Process CreateShellProcess(string filePath, string args, string workingDir = "")
        {
            var en = System.Text.Encoding.UTF8;
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                en = System.Text.Encoding.GetEncoding("gb2312");
            }
            var pStartInfo = new ProcessStartInfo(filePath)
            {
                Arguments = args,
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                StandardErrorEncoding = en,
                StandardOutputEncoding = en
            };
            if (!string.IsNullOrEmpty(workingDir))
            {
                pStartInfo.WorkingDirectory = workingDir;
            }
            return Process.Start(pStartInfo);
        }

        /// <summary>
        /// 运行脚本,不返回stderr版本
        /// </summary>
        /// <param name="filePath">命令</param>
        /// <param name="args">命令的参数</param>
        /// <param name="workingDri">工作目录</param>
        /// <returns>脚本的stdout输出</returns>
        public static string RunShellNoErr(string filePath, string args, string workingDri = "")
        {
            var p = CreateShellProcess(filePath, args, workingDri);
            var res = p.StandardOutput.ReadToEnd();
            p.Close();
            return res;
        }

        /// <summary>
        /// 运行脚本,不返回stderr版本
        /// </summary>
        /// <param name="filePath">脚本文件路径</param>
        /// <param name="args">命令的参数</param>
        /// <param name="input">StandardInput</param>
        /// <param name="workingDri">工作目录</param>
        /// <returns>脚本的stdout输出</returns>
        public static string RunShellNoErr(string filePath, string args, string[] input, string workingDri = "")
        {
            var p = CreateShellProcess(filePath, args, workingDri);
            if (input != null && input.Length > 0)
            {
                for (var i = 0; i < input.Length; i++)
                {
                    p.StandardInput.WriteLine(input[i]);
                }
            }
            var res = p.StandardOutput.ReadToEnd();
            p.Close();
            return res;
        }

        /// <summary>
        /// 运行脚本
        /// </summary>
        /// <param name="filePath">脚本文件路径</param>
        /// <param name="args">命令的参数</param>
        /// <param name="workingDir">工作目录</param>
        /// <returns>string[] res[0]命令的stdout输出, res[1]命令的stderr输出</returns>
        public static string[] RunShell(string filePath, string args, string workingDir = "")
        {
#if !UNITY_IOS
            var res = new string[3];
#else
            var res = new string[2];
#endif
            var p = CreateShellProcess(filePath, args, workingDir);
            res[0] = p.StandardOutput.ReadToEnd();
            res[1] = p.StandardError.ReadToEnd();
#if !UNITY_IOS
            res[2] = p.ExitCode.ToString();
#endif
            p.Close();
            return res;
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="absPath">文件夹的绝对路径</param>
        public static void OpenFolderInExplorer(string absPath)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                RunShellNoErr("explorer.exe", absPath);
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                RunShellNoErr("open", absPath.Replace("\\", "/"));
            }
        }
    }
}
#endif