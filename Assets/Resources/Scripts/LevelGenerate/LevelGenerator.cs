using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Resources.Scripts.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Scripts.LevelGenerate
{
    public class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField]
        public Room[] RoomsPrefabs { get; private set; }
        [field: SerializeField, Min(5)]
        public int CountRooms { get; private set; }
        [field: SerializeField]
        public GameObject[] EnemiesPrefabs { get; private set; }
        
        [field: SerializeField, Header("Passages")]
        public GameObject BottomTopPassage { get; private set; }
        [field: SerializeField]
        public GameObject LeftRightPassage { get; private set; }

        [SerializeField, Space(5)] 
        private int minSumSpawnPrices;
        [SerializeField] 
        private int maxSumSpawnPrices;
        
        public int SumSpawnPrices { get; private set; }
        public Enemy[] Enemies { get; set; }
        public List<Room> SpawnedRooms { get; private set; } = new();
        /// <summary>
        /// Список комнат, от которых на текущей итерации генерации будут генерироваться следующие комнаты
        /// </summary>
        public List<Room> WaitingRooms { get; private set; } = new();
        /// <summary>
        /// Количество еще пустых переходов между комнатами
        /// (в этом проходе должна заспавниться хотя бы одна комната)
        /// </summary>
        [HideInInspector]
        public int countEmptyPassages = 0;

        private void Awake()
        {
            Enemies = new Enemy[EnemiesPrefabs.Length];
            for (int i = 0; i < EnemiesPrefabs.Length; i++)
            {
                Enemies[i] = EnemiesPrefabs[i].GetComponent<Enemy>();
            }
        }

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            SpawnRooms();
            SumSpawnPrices = UnityEngine.Random.Range(minSumSpawnPrices, maxSumSpawnPrices + 1);
            foreach (var room in SpawnedRooms)
            {
                room.SpawnEnemies();
            }
        }

        private void SpawnRooms()
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
                int randomIndex = UnityEngine.Random.Range(0, startRooms.Count);
                Room startRoom = Instantiate(startRooms[randomIndex], new Vector3(70, -20, 0), Quaternion.identity);
                SpawnedRooms.Add(startRoom);
                countEmptyPassages = 1;
                startRoom.levelGenerator = this;
                startRoom.SpawnRooms();
                WaitingRooms.Add(SpawnedRooms[^1]);
                int i = 0;
                while (SpawnedRooms.Count != CountRooms && i <= 1000)
                {
                    List<Room> newWaitingRooms = new List<Room>();
                    foreach (var waitingRoom in WaitingRooms)
                    {
                        foreach (var newWaitingRoom in waitingRoom.SpawnRooms())
                        {
                            Room replacedRoom = null;
                            foreach (var roomSpawnPoint in newWaitingRoom.RoomSpawnPoints)
                            {
                                // Удаляем проход если в месте, где стоит roomSpawnPoint уже стоит другая точка
                                if (roomSpawnPoint.Direction != (replacedRoom ? replacedRoom.RequiredDirection : newWaitingRoom.RequiredDirection) &&
                                    IsBusyOtherRoomPoint(roomSpawnPoint, replacedRoom ? replacedRoom : newWaitingRoom))
                                {
                                    Room newReplacedRoom = DeleteDirection(roomSpawnPoint.Direction, replacedRoom == null ? newWaitingRoom : replacedRoom);
                                    
                                    for (int j = 0; j < SpawnedRooms.Count; j++)
                                    {
                                        if (SpawnedRooms[j] == (replacedRoom ? replacedRoom : newWaitingRoom))
                                        {
                                            replacedRoom = newReplacedRoom;
                                            SpawnedRooms[j] = replacedRoom;
                                            break;
                                        }
                                    }
                                }
                            }

                            newWaitingRooms.Add(replacedRoom != null ? replacedRoom : newWaitingRoom);
                        }
                    }
                    WaitingRooms = newWaitingRooms;
                    Debug.Log(SpawnedRooms.Count);
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
            countEmptyPassages -= 1;
            return newRoom;
        }
    }
}   
