#nullable enable
using UnityEngine;

namespace KoiroPkg_Universal.UI
{
    public class UIBehaviour: MonoBehaviour
    {
        private RectTransform _rectTransform;
        public RectTransform rectTransform => this.LazyGetComponent(ref _rectTransform);
    }
}