namespace IFramework.Engine
{
    public interface IActionExecutor
    {
        void ExecuteAction(IAction action);
    }

    public class MonoExecutor : IActionExecutor
    {
        public void ExecuteAction(IAction action) { }
    }
}
