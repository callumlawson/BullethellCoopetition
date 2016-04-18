using Assets.Scripts.Player;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]
public class CustomCharacterController : NetworkBehaviour
{
    [UsedImplicitly] public AnimationCurve AccelerationVelocityCurve;
    [UsedImplicitly] public float MaxAcceleration = 60.0f;
    [UsedImplicitly] public float MaxSpeed = 18.0f;

    private PlayerDeath playerDeath;

    [UsedImplicitly]
    void Awake()
    {
        playerDeath = GetComponent<PlayerDeath>();
    }

    [UsedImplicitly]
    void FixedUpdate()
    {
        if (!isLocalPlayer || playerDeath.HealthProperties.IsDead)
        {
            return;
        }

        var targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * MaxSpeed;

        var velocity = GetComponent<Rigidbody>().velocity;

        var velocityChange = (targetVelocity - velocity);

        velocityChange = Vector3.ClampMagnitude(velocityChange, GetMaxAcceleration(velocity.magnitude) * Time.deltaTime);

        GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private float GetMaxAcceleration(float currentSpeed)
    {
        return AccelerationVelocityCurve.Evaluate(currentSpeed/MaxSpeed) * MaxAcceleration;
    }
}