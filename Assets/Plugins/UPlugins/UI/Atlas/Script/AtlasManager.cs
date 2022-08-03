/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AtlasManager.cs
//  Info     : UI 图集管理器，用于快速获取Sprite对象
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;

namespace Aya.UI.Atlas
{
	public static class AtlasManager
	{
		private static readonly Dictionary<string, Atlas> AtlasList = new Dictionary<string, Atlas>();

		public static Sprite GetSprite(string atlasName, string spriteName) 
		{
			Atlas atlas = null;
			if (!AtlasList.ContainsKey(atlasName))
			{
				atlas = Resources.Load<Atlas>("Atlas/" + atlasName);
				AtlasList.Add(atlasName, atlas);
			}
			if (!AtlasList.TryGetValue(atlasName, out atlas))
			{
				Debug.LogError("Don;t find Atlas：" + atlasName);
				return null;
			}
			if (atlas == null)
			{
				Debug.LogError("Find Atlas：" + atlasName + "! But it is null!");
				return null;
			}
			if (atlas.Dics == null)
			{
				Debug.LogError("Atlas:" + atlasName + " dic is null!");
				return null;
			}

			Sprite ret;
			atlas.Dics.TryGetValue(spriteName, out ret);
			if (ret == null)
			{
				Debug.LogError("Sprite is not found：" + atlasName + ", " + spriteName);
			}
			return ret;
		}
	}
}

