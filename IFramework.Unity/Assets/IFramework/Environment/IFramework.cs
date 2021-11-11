using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 系统初始化类
    /// </summary>
    public class IFramework
    {
        #if UNITY_EDITOR
        [InitializeOnLoadMethod()]
        public static void InitBeforeEditorLoad()
        {
            // "初始化 PlatformEnvironment"
            PlatformEnvironment.Instance.Init(Environment.Instance, new Zip());
        }
        #endif
        
        /// <summary>
        /// 场景开始前初始化
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitBeforeSceneLoad(){
            
            // "初始化 PlatformEnvironment"
            PlatformEnvironment.Instance.Init(Environment.Instance, new Zip());

            // 异步加载初始化 ResourceManager"
            ResourceManager.Instance.InitAsync();
            
            // 初始化Bean
            BeanRegister();
        }

        /// <summary>
        /// 注册Bean到IOC容器
        /// </summary>
        private static void BeanRegister()
        {
            IocContainer.Instance.Register<ITypeEvent, TypeEvent>();
        }
    }
}