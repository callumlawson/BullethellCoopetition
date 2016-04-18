using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class Warp : NetworkBehaviour
    {
        [UsedImplicitly] public GameObject WarpIndicator;
        [UsedImplicitly] public float MaxWarpDistance;
        [UsedImplicitly] public AudioClip WarpSound;

        private AudioSource soundPlayer;

        [UsedImplicitly]
        void Awake()
        {
            soundPlayer = GetComponent<AudioSource>();
        }

        [UsedImplicitly]
        void Start () {
            WarpIndicator.SetActive(false);
        }

        [UsedImplicitly]
        void Update ()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            var horizontal = Input.GetAxis("RightHorizontal");
            var vertical = Input.GetAxis("RightVertical");

            if (Mathf.Abs(horizontal) > 0.15f || Mathf.Abs(vertical) > 0.15f)
            {
                WarpIndicator.SetActive(true);
                WarpIndicator.transform.DOMove(transform.position +
                                               new Vector3(horizontal * MaxWarpDistance, 0, vertical * MaxWarpDistance), 0.1f);
            }
            else
            {
                WarpIndicator.SetActive(false);
            }

            if (Input.GetButtonDown("RightShoulder"))
            {
                soundPlayer.PlayOneShot(WarpSound);
                transform.DOMove(WarpIndicator.transform.position, 0.1f);
            }
        }
    }
}
