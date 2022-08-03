/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RegexUtil.cs
//  Info     : 正则表达式辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System.Text.RegularExpressions;

namespace Aya.Util
{
	/// <summary>
	/// 正则表达式辅助
	/// </summary>
	public static class RegexUtil
	{
		/// <summary>
		/// 正则 - 数字
		/// </summary>
		public static Regex RegexNumber = new Regex(@"\d+");

		#region Text

		/// <summary>
		/// 正则 - XML
		/// </summary>
		public static Regex RegexXml = new Regex(@"^([a-zA-Z]+-?)+[a-zA-Z0-9]+\\.[x|X][m|M][l|L]$");

		/// <summary>
		/// 正则 - Html标签<para/>
		/// 注意：无法处理复杂嵌套
		/// </summary>
		public static Regex RegexHtml = new Regex(@"<(\S*?)[^>]*>.*?</\1>|<.*? /> ");

        #endregion

        #region Number

	    /// <summary>
	    /// 正则 - 电话号码<para/>
	    /// </summary>
	    public static Regex RegexPhoneNumber = new Regex(@"^[1][3,4,5,7,8][0-9]{9}$");

        #endregion

        #region String

        /// <summary>
        /// 正则 - 双字节<para/>
        /// 可用于统计字符串长度
        /// </summary>
        public static Regex RegexDoubleByte = new Regex(@"[^\x00-\xff]");

		/// <summary>
		/// 正则 - 空行<para/>
		/// 可用于统计、删除空行
		/// </summary>
		public static Regex RegexSpaceLine = new Regex(@"\n\s*\r");

		/// <summary>
		/// 正则 - 首尾空白字符<para/>
		/// 可以用来删除行首行尾的空白字符(包括空格、制表符、换页符等等
		/// </summary>
		public static Regex RegexSpaceAtStartOrEnd = new Regex(@"^\s*|\s*$");

		#endregion

		#region Date

		/// <summary>
		/// 正则 - 日期<para/>
		/// xxxx-xx-xx
		/// </summary>
		public static Regex RegexDate = new Regex(@"^\d{4}-\d{1,2}-\d{1,2}");

		/// <summary>
		/// 正则 - 日期中的月份<para/>
		/// 一年的12个月(01～09和1～12)
		/// </summary>
		public static Regex RegexMonth = new Regex(@"^(0?[1-9]|1[0-2])$");

		/// <summary>
		/// 正则 - 日期中的天<para/>
		/// 一个月的31天(01～09和1～31)
		/// </summary>
		public static Regex RegexDay = new Regex(@"^((0?[1-9])|((1|2)[0-9])|30|31)$");

		#endregion

		#region ID / Pwd

		/// <summary>
		/// 正则 - 帐号<para/>
		/// 帐号是否合法(字母开头，允许5-16字节，允许字母数字下划线)
		/// </summary>
		public static Regex RegexID = new Regex(@"^[a-zA-Z][a-zA-Z0-9_]{4,15}$");

		/// <summary>
		/// 正则 - 密码<para/>
		/// 密码(以字母开头，长度在6~18之间，只能包含字母、数字和下划线)
		/// </summary>
		public static Regex RegexPwd = new Regex(@"^[a-zA-Z]\w{5,17}$");

		/// <summary>
		/// 正则 - 强密码<para/>
		/// 强密码(必须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间)
		/// </summary>
		public static Regex RegexPwdStrong = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,18}$");

		#endregion

		#region Internet

		/// <summary>
		/// 正则 - 域名
		/// </summary>
		public static Regex RegexDomainName = new Regex(@"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?");

		/// <summary>
		/// 正则 - E-mail
		/// </summary>
		public static Regex RegexEmail = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

		/// <summary>
		/// 正则 - URL
		/// </summary>
		public static Regex RegexUrl = new Regex(@"[a-zA-z]+://[^\s]*");

		#endregion

		#region IP v4/v6

		/// <summary>
		/// 正则 - IPv4
		/// 0.0.0.0 - 255.255.255.255
		/// </summary>
		public static Regex RegexIpV4 = new Regex(@"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$");

		/// <summary>
		/// 正则 - IPv6
		/// 5e:0:0:0:0:0:5668:eeee
		/// </summary>
		public static Regex RegexIpV6 =
			new Regex(@"^([\da-fA-F]{1,4}:){6}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)");

		/// <summary>
		/// 正则 - IPv4 or v6
		/// </summary>
		public static Regex RegexIpV4OrV6 =
			new Regex(@"^([\da-fA-F]{1,4}:){6}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)");

		#endregion

		#region Chinese Only

		/// <summary>
		/// 正则 - QQ<para/>
		/// 腾讯QQ号从10000开始
		/// </summary>
		public static Regex RegexQQ = new Regex(@"[1-9][0-9]{4,}");

		/// <summary>
		/// 正则 - 汉字
		/// </summary>
		public static Regex RegexChinese = new Regex(@"^[\u4e00-\u9fa5]$");

		/// <summary>
		/// 正则 - 手机号码
		/// </summary>
		public static Regex RegexTelephone = new Regex(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$");

		/// <summary>
		/// 正则 - 座机号码<para/>
		/// 匹配形式如 0511-4405222 或 021-87888822
		/// </summary>
		public static Regex RegexPhone = new Regex(@"\d{3}-\d{8}|\d{4}-\d{7}");

		/// <summary>
		/// 正则 - 中国邮编<para/>
		/// 中国邮政编码为6位数字
		/// </summary>
		public static Regex RegexPostCode = new Regex(@"[1-9]\d{5}(?!\d)");

		/// <summary>
		/// 正则 - 身份证<para/>
		/// 中国的身份证为15位或18位
		/// </summary>
		public static Regex RegexChineseID = new Regex(@"\d{15}|\d{18}");

		/// <summary>
		/// 正则 - 短身份证号<para/>
		/// 短身份证号码(数字、字母x结尾)
		/// </summary>
		public static Regex RegexChineseIDShort = new Regex(@"^([0-9]){7,18}(x|X)?$");

		#endregion
	}
}