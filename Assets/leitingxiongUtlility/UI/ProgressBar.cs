#nullable enable
using UnityEngine;

namespace leitingxiongUtlility.UI
{
    public class ProgressBar: UIBehaviour
    {
        public RectTransform top;
        public RectTransform down;

        private float _progress;

        public float progress
        {
            get => _progress;
            set
            {
                _progress = value;
                float width = value * down.sizeDelta.x;
                top.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            }
        }

        public void Init(float progress)
        {
            this.progress = progress;
        }
    }
}