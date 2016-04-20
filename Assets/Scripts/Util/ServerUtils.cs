using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Util
{
    public static class ServerUtils
    {
        public static GameObject ServerSpawn(GameObject gameObject, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
        {
            var thing = Object.Instantiate(gameObject, position, rotation) as GameObject;
            //TODO: Optimise this.
            var parent = GameObject.Find("[DynamicObjects]");
            if (parent == null)
            {
                Debug.LogError("Expected to find DynamicObjects game object in scene. Please create one at root");
                return null;
            }
            if (thing == null)
            {
                Debug.LogError("Tried to spawn gameobject on server but it was null :(");
                return null;
            }
            thing.transform.parent = parent.transform;
            NetworkServer.Spawn(thing);
            return thing;
        }
    }
}
