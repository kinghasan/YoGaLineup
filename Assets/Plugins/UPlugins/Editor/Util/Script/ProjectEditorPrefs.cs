/////////////////////////////////////////////////////////////////////////////
//
//  Script : ProjectEditorPrefs.cs
//  Info   : 项目专用EditorPrefs，在原有EditorPrefs的基础上，添加项目名作为存储键值
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Aya.EditorScript
{
	public class ProjectEditorPrefs
	{
		#region Key

		/// <summary>
		/// 存储键值前缀
		/// </summary>
		public static string Key
		{
			get
			{
				return Application.productName + "_";
			}
		}
		
		public static void DeleteKey(string key) {
			EditorPrefs.DeleteKey(Key + key);
		}

		public static bool HasKey(string key) {
			return EditorPrefs.HasKey(Key + key);
		} 
		#endregion

		#region Int
		public static int GetInt(string key, int defualtValue = default(int))
		{
			return EditorPrefs.GetInt(Key + key, defualtValue);
		}

		public static void SetInt(string key, int value)
		{
			EditorPrefs.SetInt(Key + key, value);
		}
		#endregion

		#region Bool
		public static bool GetBool(string key, bool defualtValue = default(bool))
		{
			return EditorPrefs.GetBool(Key + key, defualtValue);
		}

		public static void SetBool(string key, bool value)
		{
			EditorPrefs.SetBool(Key + key, value);
		}
		#endregion

		#region Float
		public static float GetFloat(string key, float defualtValue = default(float))
		{
			return EditorPrefs.GetFloat(Key + key, defualtValue);
		}

		public static void SetFloat(string key, float value)
		{
			EditorPrefs.SetFloat(Key + key, value);
		}
		#endregion

		#region String
		public static string GetString(string key, string defualtValue = default(string))
		{
			return EditorPrefs.GetString(Key + key, defualtValue);
		}

		public static void SetString(string key, string value)
		{
			EditorPrefs.SetString(Key + key, value);
		}
		#endregion
	}
}
#endif