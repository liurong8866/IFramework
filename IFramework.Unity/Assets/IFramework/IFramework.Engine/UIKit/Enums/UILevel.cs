namespace IFramework.Engine
{
    /// <summary>
    /// 面板显示层级
    /// </summary>
    public enum UILevel
    {
        Background = 0, //背景层UI
        Common = 1,     //普通层UI
        Popup = 2,      //弹出层UI
        Canvas = 100    // 一个 Panel 就是一个 Canvas 的 Panel
    }
}
