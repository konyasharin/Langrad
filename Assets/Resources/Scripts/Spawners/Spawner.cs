using UnityEngine;

namespace Resources.Scripts.Spawners
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public GameObject Spawn(GameObject prefab, Vector2 position)
        {
            return Instantiate(prefab, position, Quaternion.identity);
        }
    }
}