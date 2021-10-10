namespace IFramework.Core
{
    /// <summary>
    /// IOC单例容器，全局唯一
    /// </summary>
    public class IocContainer : AbstractContainer, ISingleton
    {
        protected IocContainer() { }

        public static IocContainer Instance => SingletonProperty<IocContainer>.Instance;

        public virtual void OnInit() { }
    }
}
