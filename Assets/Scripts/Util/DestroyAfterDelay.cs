using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float SecondsBeforeDestruction = 10;

	void Start () {
	    Invoke("DestroyThis", SecondsBeforeDestruction);
	}

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
