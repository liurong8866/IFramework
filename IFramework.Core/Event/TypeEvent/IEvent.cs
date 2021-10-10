using System;

namespace IFramework.Core
{
    public interface IEvent<T>
    {
        void OnEvent(T action);
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
