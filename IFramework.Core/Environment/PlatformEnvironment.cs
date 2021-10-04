namespace IFramework.Core
{
    /// <summary>
    /// 中转类，不要调用
    /// </summary>
    public sealed class PlatformEnvironment : Singleton<PlatformEnvironment>
    {
        private IEnvironment environment;

        private PlatformEnvironment() { }

        public void Init(IEnvironment environment)
        {
            this.environment = environment;
        }

        /// <summary>
        /// 获取当前平台名称
        /// </summary>
        public string RuntimePlatformName => environment.RuntimePlatformName;

        public bool IsSimulation => environment.IsSimulation;

        public string FilePathPrefix => environment.FilePathPrefix;
    }
}
