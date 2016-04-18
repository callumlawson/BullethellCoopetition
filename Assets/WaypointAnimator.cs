using UnityEngine;
using DG.Tweening;

public class WaypointAnimator : MonoBehaviour
{
    public Transform WayPoint;
    public float DurationInSeconds;

	// Use this for initialization
	void Start ()
	{
	    transform.DOMove(WayPoint.position, DurationInSeconds).SetLoops(-1, LoopType.Yoyo);
	}
}
