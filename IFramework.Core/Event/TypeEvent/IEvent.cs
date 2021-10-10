using System;

namespace IFramework.Core
{
    public interface IEvent<in T>
    {
        void OnEvent(T param);
    }

    public static class IEventExtension
    {
        public static IDisposable RegisterEvent<T>(this IEvent<T> self) where T : struct
        {
            return TypeEvent.Register<T>(self.OnEvent);
        }

        public static void UnRegisterEvent<T>(this IEvent<T> self) where T : struct
        {
            TypeEvent.UnRegister<T>(self.OnEvent);
        }
    }
}
