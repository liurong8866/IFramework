namespace IFramework.Core
{
    public struct Constant
    {
        // IFramework 目录
        public const string FRAMEWORK_NAME = "IFramework";

        // Environment 目录
        public const string ENVIRONMENT_PATH = "IFramework/Environment";

        // AssetBundles 生成的脚本文件名称
        public const string ASSET_BUNDLE_SCRIPT_FILE = "AssetsName.cs";

        // AssetBundle 生成目录
        public const string ASSET_BUNDLE_PATH = "AssetBundle";

        // Resources/Images
        public const string RESOURCE_IMAGE_PATH = "Resources/Images";

        // Resources/Images/Photo
        public const string RESOURCE_PHOTO_PATH = "Resources/Images/Photo";

        // Resources/Video
        public const string RESOURCE_VIDEO_PATH = "Resources/Video";

        // Resources/Audio
        public const string RESOURCE_AUDIO_PATH = "Resources/Audio";

        // AssetBundle 密钥
        public const string ASSET_BUNDLE_KEY = "iT5jM9h+7zT1rZ6x";

        // AssetBundle 配置文件名称
        public const string ASSET_BUNDLE_CONFIG_FILE = "asset-bundle-config.bin";

        // AssetBundle 配置文件密钥
        public const string ASSET_BUNDLE_CONFIG_FILE_KEY = "AoGI+h3OEA4TcJ1H";

        // Double类型数据比较 == 时 精度保留0.000001 有效，超过则视为可接受误差，判断为：相等
        public const double TOLERANCE = 1E-6;
    }
}
