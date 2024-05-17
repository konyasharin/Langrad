using System;
using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    [Serializable]
    public struct Level
    {
        public Room[] roomsPrefabs;
        [Min(5)]
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
