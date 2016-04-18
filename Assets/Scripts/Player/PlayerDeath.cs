using System;
using Assets.Scripts.Util;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [Serializable]
    public class HealthProperties
    {
        public bool IsDead;
    }

    public class PlayerDeath : MonoBehaviour
    {
        [UsedImplicitly] public HealthProperties HealthProperties;

        private Rigidbody ourRigidbody;

        [UsedImplicitly]
        void Awake()
        {
            ourRigidbody = GetComponent<Rigidbody>();
            Respawn();
        }

        [UsedImplicitly]
        void OnCollisionEnter(Collision col)
        {
            if (col.collider.gameObject.GetComponent<Killer>())
            {
                KillPlayer();
            }
        }

        [UsedImplicitly]
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.GetComponent<Killer>())
            {
                KillPlayer();
            }
        }

        [UsedImplicitly]
        void FixedUpdate()
        {
            if (HealthProperties.IsDead)
            {
                ourRigidbody.AddForce(new Vector3(0, -60 * ourRigidbody.mass, 0));
            }
        }

        private void KillPlayer()
        {
            ourRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            HealthProperties.IsDead = true;
            Invoke("Respawn", 3.0f);
        }

        [UsedImplicitly]
        private void Respawn()
        {
            HealthProperties.IsDead = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = new Vector3(0, 1, 0);
            ourRigidbody.velocity = Vector3.zero;
            ourRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}