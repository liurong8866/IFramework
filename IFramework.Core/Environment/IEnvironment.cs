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
    }
}
