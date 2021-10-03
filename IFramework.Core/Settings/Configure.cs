namespace IFramework.Core
{
    public class Configure
    {
        // 当前平台
        public static readonly ConfigInt CurrentPlatform = new ConfigInt("CurrentPlatform");

        // 自动生成名称
        public static readonly ConfigBool AutoGenerateName = new ConfigBool("AutoGenerateName", true);

        // 是否模拟模式
        public static readonly ConfigBool IsSimulation = new ConfigBool("IsSimulation", true);

        // 是否从StreamingAssets加载资源
        public static readonly ConfigBool LoadAssetFromStream = new ConfigBool("LoadAssetFromStream", true);
    }
}
