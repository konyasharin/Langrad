using System;
using Resources.Scripts.LevelGenerate.RoomScripts;
using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    [Serializable]
    public struct Level
    {
        public Room[] roomsPrefabs;
        public GameObject door;
        public GameObject portal;
        public GameObject[] startItemsPrefabs;
        [Range(3, 20)]
        public int countRooms;
        public GameObject[] enemiesPrefabs;
        [Header("Passages")] 
        public GameObject bottomTopPassage;
        public GameObject leftRightPassage;
        [Header("SpawnPrices")]
        public int minSumSpawnPrices;
        public int maxSumSpawnPrices;
    }
}
