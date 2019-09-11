namespace Primitives
{
    public class Initializer<T> : Disposable
    {
        public Initializer(T value) => Value = value;
        public T Value { get; }
        public static implicit operator T(Initializer<T> initializer) => initializer.Value;
    }
}
