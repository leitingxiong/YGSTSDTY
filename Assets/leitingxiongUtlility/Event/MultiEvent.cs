#nullable enable

using System;
using Object = UnityEngine.Object;

namespace KoiroPkg_Universal.Event
{
    public class MultiEvent<T>
    {
        public readonly AdvancedAction<T> before = new AdvancedAction<T>();
        public readonly AdvancedAction<T> execute = new AdvancedAction<T>();
        public readonly AdvancedAction<T> after = new AdvancedAction<T>();


        public void AddListenerBefore(Action<T> action, params Object[] objects)
        {
            before.AddListener(action, objects);
        }
        public void AddListener(Action<T> action, params Object[] objects)
        {
            execute.AddListener(action, objects);
        }

        public void AddListenerAfter(Action<T> action, params Object[] objects)
        {
            after.AddListener(action, objects);
        }

        public void RemoveListener(Action<T> action)
        {
            execute.RemoveListener(action);
        }

        public void RemoveListenerAfter(Action<T> action)
        {
            after.RemoveListener(action);
        }

        public void RemoveListenerBefore(Action<T> action)
        {
            after.RemoveListener(action);
        }

        public void InvokeBefore(T context)
        {
            before.Invoke(context);
        }
        public void InvokeExecuteAndAfter(T context)
        {
            execute.Invoke(context);
            after.Invoke(context);
        }

        public void InvokeAll(T context)
        {
            before.Invoke(context);
            execute.Invoke(context);
            after.Invoke(context);
        }
    }
}