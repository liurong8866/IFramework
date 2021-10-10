namespace IFramework.Core
{
    public delegate void OnAction(int key, params object[] args);

    public class NumberEvent : CommonEvent<OnAction> { }
}
