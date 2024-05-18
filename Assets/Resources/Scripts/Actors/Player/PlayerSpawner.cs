using UnityEngine;

namespace Resources.Scripts.Actors.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        [field: SerializeField]
        public GameObject PlayerPrefab { get; set; }

        public void SpawnPlayer(Vector3 position)
        {
            if (PlayerCharacter.Instance != null)
            {
                Destroy(PlayerCharacter.Instance.gameObject);
            }
            Instantiate(PlayerPrefab, position, Quaternion.identity);
        }
    }
}
