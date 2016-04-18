using UnityEngine;
using DG.Tweening;

public class FaceVelocity : MonoBehaviour {

    public Rigidbody cachedRigidbody;

    private Vector3 lastDirection;
    private Tweener tween;

    void Start()
    {
        tween = transform.DOBlendableRotateBy(GetVelocityAngle(), 0.5f).SetAutoKill(false).OnComplete(() => tween.ChangeEndValue(lastDirection, true).Restart());
        lastDirection = cachedRigidbody.velocity;
    }

    void Update()
    {
        if (cachedRigidbody.velocity.magnitude > 0.1f && GetVelocityAngle() != lastDirection)
        {
            tween.ChangeEndValue(GetVelocityAngle(), true).Restart();
            lastDirection = GetVelocityAngle();
        }
        else
        {
//            tween.ChangeEndValue(lastDirection, true).Restart();
        }
    }

    private Vector3 GetVelocityAngle()
    {
        return Quaternion.LookRotation(cachedRigidbody.velocity).eulerAngles;
    }
}
