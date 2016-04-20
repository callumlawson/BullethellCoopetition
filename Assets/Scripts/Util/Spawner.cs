using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Util
{
    public class Spawner : NetworkBehaviour
    {
        [Range(0.2f, 5f)] public float PeriodOfSpawningSeconds = 1;
        [Range(0.2f, 5f)] public float StartDelayInSeconds;

        public GameObject ThingToSpawn;
        public float InitialVelocity;

        public override void OnStartServer()
        {
            InvokeRepeating("SpawnThing", StartDelayInSeconds, PeriodOfSpawningSeconds);
        }

        private void SpawnThing()
        {
            var thing = ServerUtils.ServerSpawn(ThingToSpawn, gameObject.transform.position, gameObject.transform.rotation);
            if (thing != null)
            {
                thing.GetComponent<Rigidbody>().velocity = (transform.rotation*Vector3.forward).normalized*InitialVelocity;
            }
        }
    }
}