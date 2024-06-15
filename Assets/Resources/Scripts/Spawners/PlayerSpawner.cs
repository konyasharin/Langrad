using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject PlayerPrefab { get; set; }
        public static PlayerSpawner Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnPlayer(Vector2 position)
        {
            if (PlayerCharacter.Instance != null)
            {
                Destroy(PlayerCharacter.Instance.gameObject);
            }

            Spawner.Instance.Spawn(PlayerPrefab, position);
        }
    }
}
