namespace IFramework.Core
{
    public interface IPersistable<T>
    {
        T Get();

        void Save(T value);

        void Clear();
    }
}
