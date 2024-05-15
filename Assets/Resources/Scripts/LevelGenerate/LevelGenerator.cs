using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Resources.Scripts.LevelGenerate
{
    public class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField]
        public Room[] RoomsPrefabs { get; private set; }
        [field: SerializeField, Min(5)]
        public int CountRooms { get; private set; }

        [field: Header("Passages")]
        [field: SerializeField]
        public GameObject BottomTopPassage { get; private set; }
        [field: SerializeField]
        public GameObject LeftRightPassage { get; private set; }
        public List<Room> SpawnedRooms { get; private set; } = new();
        public List<Room> WaitingRooms { get; private set; } = new();
        [HideInInspector]
        public int countEmptyPassages = 0;

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            List<Room> startRooms = new List<Room>();
            foreach (var direction in Enum.GetNames(typeof(Direction)))
            {
                Room newRoom = GetRoomByDirections(new[] { Enum.Parse<Direction>(direction) });
                if (newRoom != null)
                {
                    startRooms.Add(newRoom);
                }
            }

            if (startRooms.Count > 0)
            {
                int randomIndex = Random.Range(0, startRooms.Count);
                Room startRoom = Instantiate(startRooms[randomIndex], new Vector3(70, -20, 0), Quaternion.identity);
                SpawnedRooms.Add(startRoom);
                countEmptyPassages = 1;
                startRoom.levelGenerator = this;
                startRoom.SpawnRooms();
                WaitingRooms.Add(SpawnedRooms[^1]);
                int i = 0;
                while (SpawnedRooms.Count + WaitingRooms.Count != CountRooms && i <= 10)
                {
                    countEmptyPassages = 0;
                    List<Room> newWaitingRooms = new List<Room>();
                    
                    // Количество еще не заспавненных комнат/переходов в ожидающих комнатах;
                    
                    foreach (var waitingRoom in WaitingRooms)
                    {
                        countEmptyPassages += waitingRoom.Directions.Length - 1;
                    }
                    
                    foreach (var waitingRoom in WaitingRooms)
                    {
                        foreach (var newWaitingRoom in waitingRoom.SpawnRooms())
                        {
                            Room replacedRoom = null;
                            foreach (var roomSpawnPoint in newWaitingRoom.RoomSpawnPoints)
                            {
                                if (roomSpawnPoint.Direction != (replacedRoom ? replacedRoom.RequiredDirection : newWaitingRoom.RequiredDirection) &&
                                    IsBusyOtherRoomPoint(roomSpawnPoint, replacedRoom ? replacedRoom : newWaitingRoom))
                                {
                                    replacedRoom = DeleteDirection(roomSpawnPoint.Direction, replacedRoom == null ? newWaitingRoom : replacedRoom);
                                    
                                    for (int j = 0; j < SpawnedRooms.Count; j++)
                                    {
                                        if (SpawnedRooms[j] == (replacedRoom ? replacedRoom : newWaitingRoom))
                                        {
                                            SpawnedRooms[i] = replacedRoom;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (replacedRoom != null)
                            {
                                newWaitingRooms.Add(replacedRoom);   
                            }
                            else
                            {
                                newWaitingRooms.Add(newWaitingRoom);
                            }
                        }
                    }
                    WaitingRooms = newWaitingRooms;
                    i++;
                }
            }
        }

        [CanBeNull]
        private Room GetRoomByDirections(Direction[] directions)
        {
            foreach (var room in RoomsPrefabs)
            {
                if (room.Directions.SequenceEqual(directions))
                {
                    return room;
                }
            }
            Debug.LogWarning($"Elements of rooms array doesn't have room with this directions");
            return null;
        }

        private bool IsBusyOtherRoomPoint(SpawnPoint spawnPoint, Room excludedRoom)
        {
            foreach (var room in SpawnedRooms)
            {
                if (room == excludedRoom)
                {
                    continue;
                }
                
                if (room.RoomSpawnPoints.Any(point => point.transform.position == spawnPoint.transform.position))
                {
                    return true;
                }
            }
            return false;
        }
        
        private Room DeleteDirection(Direction deleteDirection, Room room)
        {
            List<Direction> newDirections = new List<Direction>();
            foreach (var direction in room.Directions)
            {
                if (direction == deleteDirection)
                {
                    continue;
                }
                newDirections.Add(direction);
            }
            Room newRoom =  Instantiate(GetRoomByDirections(newDirections.ToArray()), room.transform.position, Quaternion.identity);
            newRoom.RequiredDirection = room.RequiredDirection;
            newRoom.levelGenerator = room.levelGenerator;
            Destroy(room.gameObject);
            Debug.Log(room.name);
            Debug.Log(newRoom.name);
            Debug.Log(room.transform.position.x);
            Debug.Log(room.transform.position.y);
            return newRoom;
        }
    }
}   
