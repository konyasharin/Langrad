using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Scripts.LevelGenerate
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] roomsPrefabs;
        [SerializeField]
        private int countSpawnRooms;
        
        [Header("Passages")]
        [SerializeField]
        private GameObject bottomTopPassage;
        [SerializeField] private GameObject leftRightPassage;

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            List<GameObject> startRooms = new List<GameObject>();
            foreach (var direction in Enum.GetNames(typeof(Direction)))
            {
                GameObject newRoom = GetRoomByDirections(new[] { Enum.Parse<Direction>(direction) });
                if (newRoom != null)
                {
                    startRooms.Add(newRoom);
                }
            }

            if (startRooms.Count > 0)
            {
                int randomIndex = Random.Range(0, startRooms.Count);
                Instantiate(startRooms[randomIndex], new Vector3(70, -20, 0), Quaternion.identity);
            }
        }

        [CanBeNull]
        private GameObject GetRoomByDirections(Direction[] directions)
        {
            foreach (var room in roomsPrefabs)
            {
                string[] roomNameParts = room.name.Split("_");
                if (roomNameParts.Length == 2 && CreateDirectionsString(directions) == roomNameParts[1])
                {
                    return room;
                }
            }
            Debug.LogWarning($"elements of rooms array doesn't have next part in name: '{CreateDirectionsString(directions)}'");
            return null;
        }

        private string CreateDirectionsString(Direction[] directions)
        {
            string directionsString = "";
            Dictionary<string, int> directionsCount = new Dictionary<string, int>();

            foreach (var direction in Enum.GetNames(typeof(Direction)))
            {
                directionsCount.Add(direction, 0);
            }
            
            foreach (var direction in directions)
            {
                string directionString = Enum.GetName(typeof(Direction), direction);
                if (directionString != null)
                {
                    directionsCount[directionString] += 1;
                }
            }

            foreach (var key in directionsCount.Keys)
            {
                if (directionsCount[key] != 0)
                {
                    directionsString += key;
                }
            }
            
            return directionsString;
        }
    }
}   
