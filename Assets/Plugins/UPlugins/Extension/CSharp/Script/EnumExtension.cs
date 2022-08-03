/////////////////////////////////////////////////////////////////////////////
//
//  Script   : EnumExtension.cs
//  Info     : Enum 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;

namespace Aya.Extension
{
    public static class EnumExtension
    {
        #region Flag

        /// <summary>
        /// 清除位运算标志
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="flag">标志</param>
        /// <returns>结果</returns>
        public static T ClearFlag<T>(this T enumValue, T flag) where T : Enum
        {
            var result = ClearFlags(enumValue, flag);
            return result;
        }

        /// <summary>
        /// 清除位运算标志
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="flags">标志</param>
        /// <returns>结果</returns>
        public static T ClearFlags<T>(this T enumValue, params T[] flags) where T : Enum
        {
            var value = Convert.ToInt32(enumValue);
            foreach (var flag in flags)
            {
                value &= ~Convert.ToInt32(flag);
            }

            var result = (T) Enum.Parse(enumValue.GetType(), value.ToString());
            return result;
        }

        /// <summary>
        /// 设置位运算标志
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="flag">标志</param>
        /// <returns>结果</returns>
        public static T SetFlag<T>(this T enumValue, T flag) where T : Enum
        {
            var result = SetFlags(enumValue, flag);
            return result;
        }

        /// <summary>
        /// 设置位运算标志
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="flags">标志</param>
        /// <returns>结果</returns>
        public static T SetFlags<T>(this T enumValue, params T[] flags) where T : Enum
        {
            var value = Convert.ToInt32(enumValue);
            foreach (var flag in flags)
            {
                value |= Convert.ToInt32(flag);
            }
            var result = (T)Enum.Parse(enumValue.GetType(), value.ToString());
            return result;
        }

        /// <summary>
        /// 是否包含保值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="flag">标志</param>
        /// <returns>结果</returns>
        public static bool ContainsFlag<T>(this T enumValue, T flag) where T : Enum
        {
            var value = Convert.ToInt32(enumValue);       
            var index = 1 << Convert.ToInt32(flag);
            var result = (value & index) == index;
            return result;
        }

        /// <summary>
        /// 是否包含值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="flag">标志</param>
        /// <returns>结果</returns>
        public static bool ContainsFlagUnsafe<T>(this T enumValue, T flag) where T :
#if CSHARP_7_3_OR_NEWER
            unmanaged, Enum
#else
            struct
#endif
        {
            unsafe
            {
#if CSHARP_7_3_OR_NEWER
                switch (sizeof(T))
                {
                    case 1:
                        return (*(byte*)&enumValue & *(byte*)&flag) > 0;
                    case 2:
                        return (*(ushort*)&enumValue & *(ushort*)&flag) > 0;
                    case 4:
                        return (*(uint*)&enumValue & *(uint*)&flag) > 0;
                    case 8:
                        return (*(ulong*)&enumValue & *(ulong*)&flag) > 0;
                    default:
                        throw new Exception("Size does not match a known Enum backing type.");
                }
#else
                switch (UnsafeUtility.SizeOf<TEnum>())
                {
                    case 1:
                        {
                            byte valLhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref lhs, &valLhs);
                            byte valRhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref rhs, &valRhs);
                            return (valLhs & valRhs) > 0;
                        }
                    case 2:
                        {
                            ushort valLhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref lhs, &valLhs);
                            ushort valRhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref rhs, &valRhs);
                            return (valLhs & valRhs) > 0;
                        }
                    case 4:
                        {
                            uint valLhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref lhs, &valLhs);
                            uint valRhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref rhs, &valRhs);
                            return (valLhs & valRhs) > 0;
                        }
                    case 8:
                        {
                            ulong valLhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref lhs, &valLhs);
                            ulong valRhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref rhs, &valRhs);
                            return (valLhs & valRhs) > 0;
                        }
                    default:
                        throw new Exception("Size does not match a known Enum backing type.");
                }
#endif
            }
        }

        #endregion

        #region String

        /// <summary>
        /// 获取字符串对应的枚举类型(如获取失败则返回枚举的默认值，如需确保成功请使用 TryParse 接口)
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举</param>
        /// <param name="strValue">字符串</param>
        /// <returns>枚举</returns>
        public static T FromString<T>(this T enumValue, string strValue) where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), strValue))
            {
                return default(T);
            }
            var result = (T)Enum.Parse(typeof(T), strValue);
            return result;
        }

        /// <summary>
        /// 尝试转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="enumValue">枚举</param>
        /// <param name="strValue">字符串</param>
        /// <param name="returnValue">结果</param>
        /// <returns>是否成功</returns>
        public static bool TryParse<T>(this T enumValue, string strValue, out T returnValue) where T : Enum
        {
            returnValue = default(T);
            if (!Enum.IsDefined(typeof(T), strValue)) return false;
            var converter = TypeDescriptor.GetConverter(typeof(T));
            returnValue = (T)converter.ConvertFromString(strValue);
            return true;
        } 
        
        #endregion

        #region Index

        /// <summary>
        /// 获取枚举数值对应的索引
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举</param>
        /// <param name="intValue">数值</param>
        /// <returns>索引</returns>
        public static int EnumIndex<T>(this T enumValue, int intValue) where T : Enum
        {
            var index = 0;
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if ((int)value == intValue)
                {
                    return index;
                }
                ++index;
            }
            return -1;
        }

        /// <summary>
        /// 获取枚举数值对应的索引
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举</param>
        /// <param name="byteValue">字节值</param>
        /// <returns>索引</returns>
        public static int EnumIndex<T>(this T enumValue, byte byteValue) where T : Enum
        {
            var index = 0;
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if ((byte)value == byteValue)
                {
                    return index;
                }
                ++index;
            }
            return -1;
        }

        #endregion
    }
}