using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    class CuteMotion : MonoBehaviour
    {
        void Awake()
        {
            transform.DOPunchRotation(new Vector3(1.05f, 1.05f, 1.05f), 2.0f, 0).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}
