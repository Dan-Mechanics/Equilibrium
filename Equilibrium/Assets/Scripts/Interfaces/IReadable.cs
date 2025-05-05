using UnityEngine;

namespace OuterWilds
{
    public interface IReadable<T> 
    {
        public T Data { get; }
    }
}