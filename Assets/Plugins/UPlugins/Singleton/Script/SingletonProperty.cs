/////////////////////////////////////////////////////////////////////////////
//
//  Script : SingletonProperty.cs
//  Info   : CSharp 单例属性，用于实现需要继承指定基类类型的单例
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////

namespace Aya.Singleton
{
    #region How to use
    //    public class BaseManager
    //    {
    //    }
    //
    //    public class TestManager : BaseManager, ISingleton
    //    {
    //        public TestManager Ins => SingletonProperty<TestManager>.Ins;
    //    } 
    #endregion

    public static class SingletonProperty<T> where T : class, ISingleton
    {
        private static T Instance;
        private static readonly object InstanceLock = new object();

        public static T Ins
        {
            get
            {
                lock (InstanceLock)
                {
                    if (Instance == null)
                    {
                        Instance = SingletonManager.CreateSingleton<T>();
                    }
                }
                return Instance;
            }
        }

        public static void Dispose()
        {
            Instance = null;
        }
    }
}
