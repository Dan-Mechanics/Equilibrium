using UnityEngine;

namespace OuterWilds
{
    public interface IDataGettable<T> 
    {
        public T Data { get; }
    }
}