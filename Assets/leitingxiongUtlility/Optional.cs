#nullable enable
using System;

namespace leitingxiongUtlility
{
    public class Optional<T>
    {
        private T _instance;
        public bool isExist => _instance != null;
        public Optional(T instance)
        {
            _instance = instance;
        }

        public Optional()
        {
        
        }

        public void IfExist(Action<T> action)
        {
            action.Invoke(_instance);
        }
        
        
    }
}