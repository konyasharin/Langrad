using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Scripts.LevelGenerate
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private RoomControl[] rooms;
        [SerializeField, Min(5)]
        private int countRooms;

        [Header("Passages")]
        [SerializeField]
        private GameObject bottomTopPassage;
        [SerializeField]
        private GameObject leftRightPassage;

        private int countSpawnedRooms = 0;

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            List<RoomControl> startRooms = new List<RoomControl>();
            foreach (var direction in Enum.GetNames(typeof(Direction)))
            {
                RoomControl newRoom = GetRoomByDirections(new[] { Enum.Parse<Direction>(direction) });
                if (newRoom != null)
                {
                    startRooms.Add(newRoom);
                }
            }

            if (startRooms.Count > 0)
            {
                int randomIndex = Random.Range(0, startRooms.Count);
                RoomControl startRoom = Instantiate(startRooms[randomIndex], new Vector3(70, -20, 0), Quaternion.identity);
                startRoom.SpawnPassages(leftRightPassage, bottomTopPassage);
                countSpawnedRooms += 1;
                RoomControl[] newRooms = startRoom.SpawnRooms(rooms, countRooms, ref countSpawnedRooms);
                foreach (var newRoom in newRooms)
                {
                    newRoom.SpawnPassages(leftRightPassage, bottomTopPassage);
                    newRoom.SpawnRooms(rooms, countRooms, ref countSpawnedRooms);
                }
            }
        }

        [CanBeNull]
        private RoomControl GetRoomByDirections(Direction[] directions)
        {
            foreach (var room in rooms)
            {
                if (room.Directions.SequenceEqual(directions))
                {
                    return room;
                }
            }
            Debug.LogWarning($"Elements of rooms array doesn't have room with this directions");
            return null;
        }
    }
}   
