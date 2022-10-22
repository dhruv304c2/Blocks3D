using UnityEngine;

namespace Game.Core.Interface
{
    public interface IPoolable<T>
    {
        Transform ObjectPool { get; }
        void Dispose();
        T Spawn();
        void InitialisePool(int size);
    }
}