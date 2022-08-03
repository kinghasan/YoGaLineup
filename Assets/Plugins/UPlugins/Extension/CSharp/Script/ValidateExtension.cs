/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ValidateExtension.cs
//  Info     : T 有效性验证扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  How to use :	emailSettings
//					.Validate(setting => setting.UserName.Length < 12, "用户名太长" )
//					.Validate(setting => setting.Password.Length > 12, "密码太短")
//					.Validate(setting => setting.Pop3Server != "pop.live.com", "不支持pop.live.com邮件服务器")
//					.ErrorsList
//					.ForEach(Console.WriteLine);
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class ValidateExtension
    {
        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <typeparam name="T">验证类型</typeparam>
        /// <param name="target">验证目标</param>
        /// <param name="predicate">验证表达式</param>
        /// <param name="errorMessage">不满足条件时的错误信息</param>
        /// <returns>验证结果</returns>
        public static ValidateResult<T> Validate<T>(this T target, Predicate<T> predicate, string errorMessage)
        {
            var result = new ValidateResult<T>(target);
            if (!predicate(target))
            {
                result.Errors.Add(errorMessage);
            }

            return result;
        }

        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <typeparam name="T">验证类型</typeparam>
        /// <param name="target">验证结果</param>
        /// <param name="predicate">验证表达式</param>
        /// <param name="errorMessage">不满足条件时的错误信息</param>
        /// <returns>验证结果</returns>
        public static ValidateResult<T> Validate<T>(this ValidateResult<T> target, Predicate<T> predicate, string errorMessage)
        {
            if (!predicate(target.Entity))
            {
                target.Errors.Add(errorMessage);
            }

            return target;
        }

        /// <summary>
        /// 验证结果
        /// </summary>
        /// <typeparam name="T">验证类型</typeparam>
        public class ValidateResult<T>
        {
            internal List<string> Errors { get; set; }
            internal T Entity { get; private set; }

            internal ValidateResult(T entity)
            {
                Errors = new List<string>();
                Entity = entity;
            }

            public string[] ErrorsArray => Errors.ToArray();
            public List<string> ErrorsList => Errors;
        }
    }
}