using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 系统初始化类
    /// </summary>
    public class IFramework
    {
        /// <summary>
        /// 场景开始前初始化
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitBeforeSceneLoad(){
            
            Log.Info("初始化 PlatformEnvironment");
            PlatformEnvironment.Instance.Init(Environment.Instance);

            Log.Info("初始化 ResourceManager");
            ResourceManager.Init();
            
            // 异步加载
            // ResourceManager.Instance.StartInitAsync();
        }
    }
}