using System;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    public interface IReadOnlyVariable<out T>
    {
        T Value { get; }

        IDisposable Subscribe(Action<T, T> action);
    }
}