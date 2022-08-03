/////////////////////////////////////////////////////////////////////////////
//
//  Script : AndroidHelper.cs
//  Info   : 跨平台 安卓操作辅助类
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Tip    : 该类可以通过一个静态方法获取一个类实例，然后快速调用该类的方法
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Platform
{
	public class AndroidHelper
	{
		public string PackageName { get; private set; }
		public string StaticMethodName { get; private set; }
#if UNITY_ANDROID
		public AndroidJavaClass JavaClass { get; private set; }
		public AndroidJavaObject JavaInstance { get; private set; }
#endif

		public AndroidHelper(string packageName, string staticMethodName)
		{
			PackageName = packageName;
			StaticMethodName = staticMethodName;
#if UNITY_ANDROID
			JavaClass = new AndroidJavaClass(packageName);
			JavaInstance = JavaClass.CallStatic<AndroidJavaObject>(staticMethodName);
#endif
		}

		#region Invoke

		public void Invoke(string methodName)
		{
#if UNITY_ANDROID
			JavaInstance.Call(methodName);
#endif
		}

		public T Invoke<T>(string methodName)
		{
#if UNITY_ANDROID
			return JavaInstance.Call<T>(methodName);
#else
			return default(T);
#endif
		}

		public void Invoke(string methodName, params object[] args)
		{
#if UNITY_ANDROID
			JavaInstance.Call(methodName, args);
#endif
		}

		public T Invoke<T>(string methodName, params object[] args)
		{
#if UNITY_ANDROID
			return JavaInstance.Call<T>(methodName, args);
#else
			return default(T);
#endif
		}

		public void Invoke(string methodName, string[] values)
		{
#if UNITY_ANDROID
			JavaInstance.Call(methodName, methodName, _javaArrayFromCSharp(values));
#endif
		}

#if UNITY_ANDROID
		private AndroidJavaObject _javaArrayFromCSharp(string[] values)
		{
			var arrayClass = new AndroidJavaClass("java.lang.reflect.Array");
			var arrayObject =
				arrayClass.CallStatic<AndroidJavaObject>("newInstance", new AndroidJavaClass("java.lang.String"), values.Length);
			for (var i = 0; i < values.Length; ++i)
			{
				arrayClass.CallStatic("set", arrayObject, i, new AndroidJavaObject("java.lang.String", values[i]));
			}

			return arrayObject;
		}
#endif

		#endregion

		#region Get / Set

		public T Get<T>(string fieldName)
		{
#if UNITY_ANDROID
			return JavaInstance.Get<T>(fieldName);
#else
			return default(T);
#endif
		}

		public void Set<T>(string fieldName, T value)
		{
#if UNITY_ANDROID
			JavaInstance.Set(fieldName, value);
#endif
		}

		#endregion

		#region Invoke Static

		public void InvokeStatic(string methodName)
		{
#if UNITY_ANDROID
			JavaInstance.CallStatic(methodName);
#endif
		}

		public T InvokeStatic<T>(string methodName)
		{
#if UNITY_ANDROID
			return JavaInstance.CallStatic<T>(methodName);
#else
			return default(T);
#endif
		}

		public void InvokeStatic(string methodName, params object[] args)
		{
#if UNITY_ANDROID
			JavaInstance.CallStatic(methodName, args);
#endif
		}

		public T InvokeStatic<T>(string methodName, params object[] args)
		{
#if UNITY_ANDROID
			return JavaInstance.CallStatic<T>(methodName, args);
#else
			return default(T);
#endif
		}

		#endregion

		#region Get Static / Set Static

		public T GetStatic<T>(string fieldName)
		{
#if UNITY_ANDROID
			return JavaInstance.GetStatic<T>(fieldName);
#else
			return default(T);
#endif
		}

		public void SetStatic<T>(string fieldName, T value)
		{
#if UNITY_ANDROID
			JavaInstance.SetStatic(fieldName, value);
#endif
		}

		#endregion
	}
}