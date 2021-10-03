namespace IFramework.Core
{
    public interface ICountor
    {
        // 数量
        int Counter { get; }

        // 记录
        bool Hold(object owner = null);

        // 释放
        bool UnHold(object owner = null);
    }
}
