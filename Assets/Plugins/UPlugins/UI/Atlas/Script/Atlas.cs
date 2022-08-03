/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Atlas.cs
//  Info     : UI 图集
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System.IO;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.UI.Atlas
{
	[CreateAssetMenu(menuName = "UI/Atlas")]
	[ExecuteInEditMode]
	public class Atlas : ScriptableObject, ISerializationCallbackReceiver {

#if UNITY_EDITOR
		public DefaultAsset Folder;
#endif

		public List<Sprite> Sprites;
		public List<string> SpriteNames;
		public Dictionary<string, Sprite> Dics;

#if UNITY_EDITOR
		[ContextMenu("Pack")]
		public void Pack()
		{
			if (Folder == null)
			{
				return;
			}
			var spts = new List<Sprite>();
			var sptnames = new List<string>();

			var folderPath = AssetDatabase.GetAssetPath(Folder);
			var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
			foreach (var f in files)
			{
				var s = AssetDatabase.LoadAssetAtPath<Sprite>(f);
				if (s != null)
				{
					spts.Add(s);
					sptnames.Add(s.name);
				}
			}
			SpriteNames = sptnames;
			Sprites = spts;
		}
#endif

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			if (Sprites == null)
			{
				return;
			}
			Dics = new Dictionary<string, Sprite>();
			for (var i = 0; i < Sprites.Count; i++)
			{
				var sprite = Sprites[i];
				var name = SpriteNames[i];

				if (sprite == null)
				{
					Debug.LogError("null sprite");
					continue;
				}
				Dics.Add(name, sprite);
			}
		}

		public void Awake()
		{

		}
	}

}
