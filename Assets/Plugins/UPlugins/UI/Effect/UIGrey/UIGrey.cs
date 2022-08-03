/////////////////////////////////////////////////////////////////////////////
//
//  Script   : UIGrey.cs
//  Info     : UI 灰色效果
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////

namespace UnityEngine.UI
{
    [RequireComponent(typeof(MaskableGraphic))]
    [AddComponentMenu("UI/Effects/UI Gray")]
    public static class UIGrey
    {
        public static Material GrayMaterial;

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            var shader = Shader.Find("UI/Default-Grey");
            if (shader == null)
            {
                Debug.LogError("Can't find UI/Default-Grey!");
            }

            GrayMaterial = new Material(shader) { hideFlags = HideFlags.DontSave };
        }

        /// <summary>
        /// 将UI元素置为置灰状态或者关闭置灰状态
        /// </summary>
        /// <param name="image"></param>
        /// <param name="grey"></param>
        /// <param name="originMaterial"></param>
        public static void SetGreyMaterial(this Graphic image, bool grey, Material originMaterial = null)
        {
            image.material = grey ? GrayMaterial : originMaterial;
        }

        /// <summary>
        /// Image是否处于置灰状态
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool IsGrey(this Graphic image)
        {
            return image.material == GrayMaterial;
        }
    }
}
