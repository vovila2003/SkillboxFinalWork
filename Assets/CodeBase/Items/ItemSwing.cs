using DG.Tweening;
using UnityEngine;

namespace CodeBase.Items
{
    public class ItemSwing : MonoBehaviour
    {
        private void Start() =>
            transform.DOMoveY(0.1f, 0.5f)
                     .SetRelative(true)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetLink(gameObject);
    }
}