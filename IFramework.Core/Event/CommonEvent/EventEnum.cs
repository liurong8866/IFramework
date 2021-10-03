namespace IFramework.Core
{
    /// <summary>
    /// 编辑器事件枚举
    /// </summary>
    public enum EditorEventEnum
    {
        Start = 0,
        End = 2000
    }

    /// <summary>
    /// 引擎事件枚举
    /// </summary>
    public enum EngineEventEnum
    {
        Start = EditorEventEnum.End
    }
}
