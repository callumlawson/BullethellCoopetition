using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Player
{
    public class CameraEnabler : NetworkBehaviour
    {
        public Camera ourCamera;

        // Use this for initialization
        void Start ()
        {
            ourCamera.enabled = isLocalPlayer;
        }
    }
}
