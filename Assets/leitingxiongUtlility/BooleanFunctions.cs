#nullable enable
using System;
using System.Collections.Generic;

namespace leitingxiongUtlility
{
    public class BooleanFunctions
    {
        private readonly HashSet<Func<bool>> _hashSet = new HashSet<Func<bool>>();

        public bool GetOrValue()
        {
            foreach (var func in _hashSet)
            {
                if (func.Invoke())
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetAndValue()
        {
            bool flag = true;
            foreach (var func in _hashSet)
            {
                flag &= func.Invoke();
            }

            return flag;
        }

        public void Add(Func<bool> func)
        {
            _hashSet.Add(func);
        }

        public void Remove(Func<bool> func)
        {
            _hashSet.Remove(func);
        }
    }
}