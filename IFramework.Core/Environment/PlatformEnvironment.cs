using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace IFramework.Core
{
    /// <summary>
    /// 中转类，不要调用
    /// </summary>
    public sealed class PlatformEnvironment : Singleton<PlatformEnvironment>
    {
        private IZip zip;
        private IEnvironment environment;
        
        private PlatformEnvironment() { }

        public void Init(IEnvironment environment, IZip zip)
        {
            this.environment = environment;
            this.zip = zip;
        }

        /// <summary>
        /// 获取当前平台名称
        /// </summary>
        public string RuntimePlatformName => environment.RuntimePlatformName;

        public bool IsSimulation => environment.IsSimulation;

        public string FilePathPrefix => environment.FilePathPrefix;

        public string GetPlatformName(int target)
        {
            return environment.GetPlatformName(target);
        }

        public string[] GetAssetPathsFromAssetBundleAndAssetName(string assetName, string assetBundleName)
        {
            return environment.GetAssetPathsFromAssetBundleAndAssetName(assetName, assetBundleName);
        }

        public Object LoadAssetAtPath(string assetPath, Type assetType)
        {
            return environment.LoadAssetAtPath(assetPath, assetType);
        }

        public T LoadAssetAtPath<T>(string assetPath) where T : Object
        {
            return environment.LoadAssetAtPath<T>(assetPath);
        }

        public void InitAssetBundleConfig(IAssetBundleConfig assetBundleConfig, string[] assetBundleNames = null)
        {
            environment.InitAssetBundleConfig(assetBundleConfig, assetBundleNames);
        }

        public List<string> GetFileInInner(string fileName)
        {
            return zip.GetFileInInner(fileName);
        }
    }
}
