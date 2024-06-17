using Resources.Scripts.Actors.Player;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.Spawners
{
    public class PlayerSpawner : MonoBehaviour, IService
    {
        [field: SerializeField] public GameObject PlayerPrefab { get; set; }
        private PlayerCharacter _player;
        private Spawner _spawner;

        public void Initialize()
        {
            _spawner = ServiceLocator.Instance.Get<Spawner>();
        }

        public PlayerCharacter SpawnPlayer(Vector2 position)
        {
            if (_player != null)
            {
                Destroy(_player.gameObject);
            }

            _player = _spawner.Spawn(PlayerPrefab, position).GetComponent<PlayerCharacter>();

            return _player;
        }
    }
}
