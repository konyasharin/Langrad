using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.Spawners
{
    public class Spawner : MonoBehaviour, IService
    {
        public GameObject Spawn(GameObject prefab, Vector2 position)
        {
            return Instantiate(prefab, position, Quaternion.identity);
        }
    }
}