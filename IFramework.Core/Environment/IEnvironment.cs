using System;
using UnityEditor;
using Object = UnityEngine.Object;

namespace IFramework.Core
{
    /// <summary>
    /// 用于间接调用未能编译DLL的环境相关方法
    /// </summary>
    public interface IEnvironment
    {
        string RuntimePlatformName { get; }

        string FilePathPrefix { get; }

        bool IsSimulation { get; }
        
    // #if UNITY_EDITOR
    //     public string GetPlatformName(BuildTarget target);
    // #endif 
        public string[] GetAssetPathsFromAssetBundleAndAssetName(string assetName, string assetBundleName);

        public Object LoadAssetAtPath(string assetPath, Type assetType);

        public T LoadAssetAtPath<T>(string assetPath) where T : Object;

        public void InitAssetBundleConfig(IAssetBundleConfig assetBundleConfig, string[] assetBundleNames = null);

    }

    public interface IAssetBundleConfig
    {
        public int AddAssetBundleInfo(string assetBundleName, string[] depends, out IAssetBundleInfo assetBundleInfo);
    }

    public interface IAssetBundleInfo
    {
        
    }
}
