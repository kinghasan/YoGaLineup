/////////////////////////////////////////////////////////////////////////////
//
//  Script : ScriptingDefineSymbolsUtil.cs
//  Info   : 宏定义操作辅助类
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using UnityEditor;

namespace Aya.EditorScript
{
    public class ScriptingDefineSymbolsUtil
    {
        #region Current

        /// <summary>
        /// 当前目标平台的所有宏定义符号
        /// </summary>
        public static string[] Symbols
        {
            get
            {
                var symbols = GetSymbols();
                return symbols.Split(';');
            }
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取当前目标平台的所有宏定义符号
        /// </summary>
        /// <returns>结果</returns>
        public static string GetSymbols()
        {
            return GetSymbols(EditorUserBuildSettings.selectedBuildTargetGroup);
        }

        /// <summary>
        /// 获取指定目标平台的所有宏定义符号
        /// </summary>
        /// <param name="buildTargetGroup">目标平台</param>
        /// <returns>结果</returns>
        public static string GetSymbols(BuildTargetGroup buildTargetGroup)
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            return symbols;
        }

        #endregion

        #region Override Set

        /// <summary>
        /// 覆盖设置当前目标平台的宏定义符号
        /// </summary>
        /// <param name="symbols">宏定义符号</param>
        public static void SetSymbols(string symbols)
        {
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            SetSymbols(buildTargetGroup, symbols);
        }

        /// <summary>
        /// 覆盖设置指定目标平台的宏定义符号
        /// </summary>
        /// <param name="buildTargetGroup">目标平台</param>
        /// <param name="symbols">宏定义符号</param>
        public static void SetSymbols(BuildTargetGroup buildTargetGroup, string symbols)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, symbols);
        }

        #endregion

        #region Add

        /// <summary>
        /// 添加符号到当前目标平台
        /// </summary>
        /// <param name="symbol">宏定义符号</param>
        public static void Add(string symbol)
        {
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            Add(buildTargetGroup, symbol);
        }

        /// <summary>
        /// 添加符号到当前目标平台
        /// </summary>
        /// <param name="buildTargetGroup">目标平台</param>
        /// <param name="symbol">宏定义符号</param>
        public static void Add(BuildTargetGroup buildTargetGroup, string symbol)
        {
            var symbols = GetSymbols(buildTargetGroup);
            if (string.IsNullOrEmpty(symbol))
            {
                symbols = symbol;
            }
            else
            {
                symbols += $";{symbol}";
            }

            SetSymbols(buildTargetGroup, symbols);
        }

        #endregion

        #region Contains

        /// <summary>
        /// 当前目标平台是否包含宏定义符号
        /// </summary>
        /// <param name="symbol">宏定义符号</param>
        /// <returns>结果</returns>
        public static bool Contains(string symbol)
        {
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            return Contains(buildTargetGroup, symbol);
        }

        /// <summary>
        /// 指定目标平台是否包含宏定义符号
        /// </summary>
        /// <param name="buildTargetGroup">目标平台</param>
        /// <param name="symbol">宏定义符号</param>
        /// <returns>结果</returns>
        public static bool Contains(BuildTargetGroup buildTargetGroup, string symbol)
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            return symbols.Contains(symbol);
        }

        #endregion

        #region Remove

        /// <summary>
        /// 移除当前平台的宏定义符号
        /// </summary>
        /// <param name="symbol">宏定义符号</param>
        public static void Remove(string symbol)
        {
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            Remove(buildTargetGroup, symbol);
        }

        /// <summary>
        /// 移除指定平台的宏定义符号
        /// </summary>
        /// <param name="buildTargetGroup">目标平台</param>
        /// <param name="symbol">宏定义符号</param>
        public static void Remove(BuildTargetGroup buildTargetGroup, string symbol)
        {
            var symbols = GetSymbols(buildTargetGroup);

            if (symbols.Contains($"{symbol};"))
            {
                symbols = symbols.Replace($"{symbol};", string.Empty);
            }
            else if (symbols.Contains(symbol))
            {
                symbols = symbols.Replace(symbol, string.Empty);
            }
            else
            {
                return;
            }

            SetSymbols(buildTargetGroup, symbols);
        }

        #endregion

        #region Clear

        /// <summary>
        /// 移除当前平台的所有宏定义符号
        /// </summary>
        public static void Clear()
        {
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            Clear(buildTargetGroup);
        }

        /// <summary>
        /// 移除指定平台的所有宏定义符号
        /// </summary>
        /// <param name="buildTargetGroup">目标平台</param>
        public static void Clear(BuildTargetGroup buildTargetGroup)
        {
            SetSymbols(buildTargetGroup, string.Empty);
        }

        #endregion
    }
}

#endif