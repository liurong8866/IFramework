using System.IO;
using UnityEditor;
using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 平台配置相关方法
    /// </summary>
    public static class Platform
    {
        /*----------------------------- 平台相关 -----------------------------*/

        /// <summary>
        /// 运行时平台名称
        /// </summary>
        public static string RuntimePlatformName => PlatformEnvironment.Instance.RuntimePlatformName;

        /// <summary>
        /// 是否为模拟模式
        /// </summary>
        public static bool IsSimulation => PlatformEnvironment.Instance.IsSimulation;

        /// <summary>
        /// 运行时平台名称
        /// </summary>
        public static string GetPlatformName(RuntimePlatform platform)
        {
            switch (platform) {
                case RuntimePlatform.WindowsPlayer: return "Windows";
                case RuntimePlatform.OSXPlayer: return "MacOS";
                case RuntimePlatform.LinuxPlayer: return "Linux";
                case RuntimePlatform.IPhonePlayer: return "iOS";
                case RuntimePlatform.Android: return "Android";
                case RuntimePlatform.WebGLPlayer: return "WebGL";
                case RuntimePlatform.PS4: return "PS4";
                case RuntimePlatform.PS5: return "PS5";
                case RuntimePlatform.XboxOne: return "XboxOne";
                case RuntimePlatform.WSAPlayerARM:
                case RuntimePlatform.WSAPlayerX64:
                case RuntimePlatform.WSAPlayerX86: return "WSAPlayer";
                default: return null;
            }
        }

        /*------------ 路径属性 Runtime开头的方法为携带 PlatformName ------------*/

        /// <summary>
        /// 运行时平台AssetBundle包路径，优先获取Persistent，其次获取Stream
        /// </summary>
        public static string RuntimeAssetBundlePath => DirectoryUtils.CombinePath(GetPersistentOrStreamPath(Constant.ASSET_BUNDLE_PATH), RuntimePlatformName);

        /// <summary>
        /// 运行时平台StreamAsset/AssetBundle/Platform包路径
        /// </summary>
        public static string RuntimeStreamAssetBundlePath => DirectoryUtils.CombinePath(StreamingAssets.AssetBundlePath, RuntimePlatformName);

        /// <summary>
        /// StreamingAssets目录类
        /// </summary>
        public static class StreamingAssets
        {
            /// <summary>
            /// 内部目录 StreamingAssets文件夹路径
            /// </summary>
            public static string Root => Application.streamingAssetsPath;

            /// <summary>
            /// StreamingAssets文件夹下到AssetBundle包
            /// </summary>
            public static string AssetBundlePath => DirectoryUtils.CombinePath(Root, Constant.ASSET_BUNDLE_PATH);
        }

        /// <summary>
        /// PersistentData目录类
        /// </summary>
        public static class PersistentData
        {
            private static string assetBundlePath;
            private static string imagePath;
            private static string photoPath;
            private static string videoPath;
            private static string audioPath;

            /// <summary>
            /// 外部目录 PersistentDataPath文件夹路径
            /// </summary>
            public static string Root => Application.persistentDataPath;

            /// <summary>
            /// 外部路径 PersistentDataPath/AssetBundle
            /// </summary>
            public static string AssetBundlePath => assetBundlePath = ResourcePath(assetBundlePath, Root, Constant.ASSET_BUNDLE_PATH);

            /// <summary>
            /// 外部路径 PersistentDataPath/Resources/Images
            /// </summary>
            public static string ImagePath => imagePath = ResourcePath(imagePath, Root, Constant.RESOURCE_IMAGE_PATH);

            /// <summary>
            /// 外部路径 PersistentDataPath/Resources/Images/Photo
            /// </summary>
            public static string PhotoPath => photoPath = ResourcePath(photoPath, Root, Constant.RESOURCE_PHOTO_PATH);

            /// <summary>
            /// 外部路径 PersistentDataPath/Resources/Video
            /// </summary>
            public static string VideoPath => videoPath = ResourcePath(videoPath, Root, Constant.RESOURCE_VIDEO_PATH);

            /// <summary>
            /// 外部路径 PersistentDataPath/Resources/Audio
            /// </summary>
            public static string AudioPath => audioPath = ResourcePath(audioPath, Root, Constant.RESOURCE_AUDIO_PATH);
        }

        /// <summary>
        /// TemporaryCache目录类
        /// </summary>
        public static class TemporaryCache
        {
            private static string assetBundlePath;
            private static string imagePath;
            private static string photoPath;
            private static string videoPath;
            private static string audioPath;

            /// <summary>
            /// 外部目录 temporaryCachePath文件夹路径
            /// </summary>
            public static string Root => Application.temporaryCachePath;

            /// <summary>
            /// 外部路径 temporaryCachePath/AssetBundle
            /// </summary>
            public static string AssetBundlePath => assetBundlePath = ResourcePath(assetBundlePath, Root, Constant.ASSET_BUNDLE_PATH);

            /// <summary>
            /// 外部路径 temporaryCachePath/Resources/Images
            /// </summary>
            public static string ImagePath => imagePath = ResourcePath(imagePath, Root, Constant.RESOURCE_IMAGE_PATH);

            /// <summary>
            /// 外部路径 temporaryCachePath/Resources/Images/Photo
            /// </summary>
            public static string PhotoPath => photoPath = ResourcePath(photoPath, Root, Constant.RESOURCE_PHOTO_PATH);

            /// <summary>
            /// 外部路径 temporaryCachePath/Resources/Video
            /// </summary>
            public static string VideoPath => videoPath = ResourcePath(videoPath, Root, Constant.RESOURCE_VIDEO_PATH);

            /// <summary>
            /// 外部路径 temporaryCachePath/Resources/Audio
            /// </summary>
            public static string AudioPath => audioPath = ResourcePath(audioPath, Root, Constant.RESOURCE_AUDIO_PATH);
        }

        /// <summary>
        /// 文件路径前缀 file://
        /// </summary>
        public static string FilePathPrefix => PlatformEnvironment.Instance.FilePathPrefix;

        /*--------------------------- 资源名称相关 ---------------------------*/

        /// <summary>
        /// 根据路径获得资源名
        /// </summary>
        public static string GetAssetBundleNameByUrl(string url)
        {
            return url.Replace(@"\", "/").Replace(RuntimeStreamAssetBundlePath + "/", "").Replace(PersistentData.Root + "/", "");
        }

        /// <summary>
        /// 根据资源名获得资源路径
        /// </summary>
        public static string GetUrlByAssetBundleName(string name)
        {
            // 优先返回PersistentAsset路径
            string url = DirectoryUtils.CombinePath(PersistentData.Root, name);
            return File.Exists(url) ? url : DirectoryUtils.CombinePath(RuntimeStreamAssetBundlePath, name);
        }

        /*--------------------------- 私有方法、变量 ---------------------------*/

        /// <summary>
        /// 先从外部资源获取，如果没有则返回内部资源路径
        /// </summary>
        private static string GetPersistentOrStreamPath(string relativePath)
        {
            string path = DirectoryUtils.CombinePath(PersistentData.Root, relativePath);
            if (File.Exists(path)) {
                return path;
            }
            return DirectoryUtils.CombinePath(StreamingAssets.Root, relativePath);
        }

        /// <summary>
        /// 查找资源路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="root"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        private static string ResourcePath(string path, string root, string folder)
        {
            if (path == null) {
                path = DirectoryUtils.CombinePath(root, folder);
                DirectoryUtils.Create(path);
            }
            return path;
        }
    }
}
