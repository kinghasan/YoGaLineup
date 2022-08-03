/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ObjectUtil.cs
//  Info     : 对象操作辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aya.Util
{
	public static class ObjectUtil
	{
		/// <summary>
		/// 深度拷贝
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="target">对象</param>
		/// <returns>结果</returns>
		public static T DeepCopy<T>(T target)
		{
			T local;
			var formatter = new BinaryFormatter();
			var serializationStream = new MemoryStream();
			try
			{
				formatter.Serialize(serializationStream, target);
				serializationStream.Position = 0L;
				local = (T) formatter.Deserialize(serializationStream);
			}
			finally
			{
				serializationStream.Close();
			}
			return local;
		}
	}
}