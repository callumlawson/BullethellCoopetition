using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(0.2f, 5f)]
    public float PeriodOfSpawningSeconds = 1;

    [Range(0.2f, 5f)]
    public float StartDelayInSeconds;

    public GameObject ThingToSpawn;
    public float InitialVelocity;
   

	void Start () {
	    InvokeRepeating("SpawnThing", StartDelayInSeconds, PeriodOfSpawningSeconds);
	}

    private void SpawnThing()
    {
        var thing = Instantiate(ThingToSpawn, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        if (thing != null)
        {
            thing.GetComponent<Rigidbody>().velocity = (transform.rotation * Vector3.forward).normalized * InitialVelocity;
        }
    }
}
