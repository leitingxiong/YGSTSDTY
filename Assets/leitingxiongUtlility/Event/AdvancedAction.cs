#nullable enable
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace leitingxiongUtlility.Event
{
    public class EmptyCtx
    {
    }

    public class AdvancedAction<T>
    {
        private readonly List<(Action<T>, Object[])> _callbacks;

        public AdvancedAction()
        {
            _callbacks = new();
        }

        public void AddListener(Action<T> action, params Object[] relatedObjects)
        {
            _callbacks.Add((action, relatedObjects));
        }

        public void RemoveListener(Action<T> action)
        {
            for (int i = _callbacks.Count - 1; i >= 0; i--)
            {
                var elementAction = _callbacks[i].Item1;
                if (elementAction == action)
                {
                    _callbacks.RemoveAt(i);
                }
            }
        }


        public void Invoke(T obj)
        {
            if (_callbacks.Count > 0)
            {
                for (int i = _callbacks.Count - 1; i >= 0; i--)
                {
                    bool isValid = true;
                    foreach (Object relatedObject in _callbacks[i].Item2)
                    {
                        isValid &= (relatedObject != null && !relatedObject.IsDestroyed());
                    }

                    if (!isValid) _callbacks.RemoveAt(i);
                    else
                    {
                        _callbacks[i].Item1.Invoke(obj);
                    }
                }
            }
        }
    }
}