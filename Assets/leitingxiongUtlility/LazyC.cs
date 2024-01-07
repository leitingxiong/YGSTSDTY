#nullable enable
using UnityEngine;

namespace leitingxiongUtlility
{
    public struct LazyC<T> where T : Component
    {
        private readonly Component _parent;
        private T _value;

        public LazyC(Component parent)
        {
            this._parent = parent;
            _value = null;
        }

        public T Get()
        {
            if (_value != null)
            {
                return _value;
            }else if (_parent.TryGetComponent<T>(out _value))
            {
                return _value;
            } 
            Debug.LogError(_parent.name + " don't have component: " + typeof(T).Name);
            return null;
        }
    }
}