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
        [Min(3)]
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
