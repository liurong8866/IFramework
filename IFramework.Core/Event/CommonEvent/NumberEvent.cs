namespace IFramework.Core
{
    public delegate void OnEvent(int key, params object[] args);

    public class NumberEvent : CommonEvent<OnEvent> { }
}
