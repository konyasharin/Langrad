using System;
using UnityEngine;

namespace Resources.Scripts.Player
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
            if (Player.Instance == null)
            {
                Instantiate(PlayerPrefab, position, Quaternion.identity);
            }
        }
    }
}
