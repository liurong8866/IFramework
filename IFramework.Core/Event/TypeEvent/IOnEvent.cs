using System;

namespace IFramework.Core
{
    public interface IOnEvent<T>
    {
        void OnEvent(T t);
    }

    public static class OnEventExtension
    {
        public static IDisposable RegisterEvent<T>(this IOnEvent<T> self) where T : struct
        {
            return TypeEvent.Register<T>(self.OnEvent);
        }

        public static void UnRegisterEvent<T>(this IOnEvent<T> self) where T : struct
        {
            TypeEvent.UnRegister<T>(self.OnEvent);
        }
    }
}
