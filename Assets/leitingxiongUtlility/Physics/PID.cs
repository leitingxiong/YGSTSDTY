#nullable enable
using System;
using UnityEngine;

namespace leitingxiongUtlility.Physics
{
    [Serializable]
    public class Pid
    {
        public float multiplier = 1f;
        [Range(0, 1)] public float kP;
        [Range(0, 1)] public float kI;
        [Range(0, 1)] public float kD;

        public Pid(float p, float i, float d, float multiplier)
        {
            kP = p;
            kI = i;
            kD = d;
            this.multiplier = multiplier;
        }

    }

    [Serializable]
    public class FloatPid: Pid
    {
        private float? _previousOffset = null;
        private float _integral;

        public float Calculate(float currentValue, float targetValue, float dt)
        {
            var offset = targetValue - currentValue;
            //偏差值offset的绝对值小于1时，不
            if(Mathf.Abs(offset) < 0.1f)
            {
                Clear();
            }
            if (_previousOffset == null)
            {
                _previousOffset = offset;
                return 0;
            }
            else
            {
                var preOffset = _previousOffset.Value;
                _integral += offset * dt;

                var derivative = (offset - preOffset) / dt;

                var output = kP * offset + kI * _integral + kD * derivative;

                _previousOffset = offset;
                return output * multiplier;
            }
        }

        public FloatPid Duplicate()
        {
            return new FloatPid(kD, kI, kP, multiplier);
        }
        public void Clear()
        {
            _integral = 0;
            _previousOffset = null;
        }

        public FloatPid(float p, float i, float d, float multiplier) : base(p, i, d, multiplier)
        {
        }
    }

    [Serializable]
    public class Vector2Pid: Pid
    {
        private Vector2? _previousOffset = null;
        private Vector2 _integral;

        public Vector2 Calculate(Vector2 currentValue, Vector2 targetValue, float dt)
        {
            var offset = targetValue - currentValue;
            if (_previousOffset == null)
            {
                _previousOffset = offset;
                return Vector2.zero;
            }
            else
            {
                var preOffset = _previousOffset.Value;
                _integral += offset * dt;

                var derivative = (offset - preOffset) / dt;

                var output = kP * offset + kI * _integral + kD * derivative;

                _previousOffset = offset;
                return output * multiplier;
            }
        }
        public Vector2Pid Duplicate()
        {
            return new Vector2Pid(kD, kI, kP, multiplier);
        }

        public void Clear()
        {
            _integral = Vector2.zero;
            _previousOffset = null;
        }

        public Vector2Pid(float p, float i, float d, float multiplier) : base(p, i, d, multiplier)
        {
        }
    }
}