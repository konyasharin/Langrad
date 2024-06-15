using System;
using System.Collections.Generic;
using Resources.Scripts.Actors.Enemies;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.LevelGenerate.RoomScripts;
using Resources.Scripts.Spawners;
using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    public class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField]
        public Level Level { get; private set; }
        public int SumSpawnPrices { get; private set; }
        public Enemy[] Enemies { get; private set; }
        public List<Room> SpawnedRooms { get; } = new();
        /// <summary>
        /// Список комнат, от которых на текущей итерации генерации будут генерироваться следующие комнаты
        /// </summary>
        public List<Room> WaitingRooms { get; private set; } = new();
        /// <summary>
        /// Количество еще пустых переходов между комнатами
        /// (в этом проходе должна заспавниться хотя бы одна комната)
        /// </summary>
        [HideInInspector]
        public int countEmptyPassages;

        private void Awake()
        {
            Enemies = new Enemy[Level.enemiesPrefabs.Length];
            for (int i = 0; i < Level.enemiesPrefabs.Length; i++)
            {
                Enemies[i] = Level.enemiesPrefabs[i].GetComponent<Enemy>();
            }
        }

        public void Generate()
        {
            RoomsManager.Instance.levelGenerator = this;
            SpawnRooms();
            SumSpawnPrices = UnityEngine.Random.Range(Level.minSumSpawnPrices, Level.maxSumSpawnPrices + 1);
            foreach (var room in SpawnedRooms)
            {
                if (room.Type == RoomType.Common)
                {
                    room.SpawnEnemies();   
                }
            }

            Room startRoom = RoomsManager.Instance.GetRoomsByType(RoomType.Start)[0];
            startRoom.SpawnItems(Level.startItemsPrefabs);
            PlayerSpawner.Instance.SpawnPlayer(startRoom.transform.position);
            Spawner.Instance.Spawn(Level.portal, SpawnedRooms[^1].transform.position);
        }

        private void SpawnRooms()
        {
            List<Room> startRooms = new List<Room>();
            foreach (var direction in Enum.GetNames(typeof(Direction)))
            {
                Room newRoom = RoomsManager.Instance.GetRoomByDirections(new[] { Enum.Parse<Direction>(direction) });
                if (newRoom != null)
                {
                    startRooms.Add(newRoom);
                }
            }

            if (startRooms.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, startRooms.Count);
                Room startRoom = Instantiate(startRooms[randomIndex], new Vector3(70, -20, 0), Quaternion.identity);
                startRoom.Type = RoomType.Start;
                SpawnedRooms.Add(startRoom);
                countEmptyPassages = 1;
                startRoom.levelGenerator = this;
                startRoom.SpawnRooms();
                WaitingRooms.Add(SpawnedRooms[^1]);
                
                int i = 0;
                while (SpawnedRooms.Count < Level.countRooms && i <= 1000)
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
                                    RoomsManager.Instance.IsBusyOtherRoomPoint(roomSpawnPoint, replacedRoom ? replacedRoom : newWaitingRoom))
                                {
                                    Room newReplacedRoom = RoomsManager.Instance.DeleteDirection(roomSpawnPoint.Direction, replacedRoom == null ? newWaitingRoom : replacedRoom);
                                    
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
                    if (WaitingRooms.Count == 0 && SpawnedRooms.Count < Level.countRooms)
                    {
                        for (int j = 0; j < SpawnedRooms.Count; j++)
                        {
                            if (RoomsManager.Instance.IsExpandableRoom(SpawnedRooms[i]))
                            {
                                (Room newRoom, bool isReplaced) = RoomsManager.Instance.AddRandomDirection(SpawnedRooms[i]);
                                if (isReplaced)
                                {
                                    WaitingRooms.Add(newRoom);
                                    SpawnedRooms[i] = newRoom;   
                                    break;
                                }
                            }
                        }
                    }
                    
                    Debug.Log(SpawnedRooms.Count);
                    i++;
                }

                SpawnedRooms[^1].Type = RoomType.Exit;
            }
        }
    }
}   
