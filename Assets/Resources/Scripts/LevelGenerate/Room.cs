using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Scripts.LevelGenerate
{
    public class Room : MonoBehaviour
    {
        private readonly List<SpawnPoint> _passageSpawnPoints = new();

        [HideInInspector]
        public LevelGenerator levelGenerator;
        public List<SpawnPoint> RoomSpawnPoints { get; private set; } = new();
        [field: SerializeField]
        public Direction[] Directions { get; private set; }

        public Direction? RequiredDirection;

        private void Awake()
        {
            foreach (var spawnPoint in GetComponentsInChildren<SpawnPoint>())
            {
                if (spawnPoint.SpawnPointType == SpawnPointType.Passage)
                {
                    _passageSpawnPoints.Add(spawnPoint);
                }
                else
                {
                    RoomSpawnPoints.Add(spawnPoint);
                }
            }
            
            if (Directions.Length > 4)
            {
                Directions = Directions.Distinct().ToArray();
            }
            else if (Directions.Length == 0)
            {
                Debug.LogWarning("Directions didn't choose");   
            }

            if (_passageSpawnPoints.Count != Directions.Length || RoomSpawnPoints.Count != Directions.Length)
            {
                Debug.LogWarning("Counts of passage or room spawn points and directions doesn't match");
            }
        }
        
        private void SpawnPassages()
        {
            foreach (var passageSpawnPoint in _passageSpawnPoints)
            {
                if (passageSpawnPoint.Direction == RequiredDirection)
                {
                    continue;
                }

                if (passageSpawnPoint.Direction == Direction.Left || passageSpawnPoint.Direction == Direction.Right)
                {
                    Instantiate(levelGenerator.LeftRightPassage, passageSpawnPoint.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(levelGenerator.BottomTopPassage, passageSpawnPoint.transform.position,
                        Quaternion.identity);
                }   
            }
        }

        public Room[] SpawnRooms()
        {
            SpawnPassages();
            List<Room> newRooms = new List<Room>();
            foreach (var roomSpawnPoint in RoomSpawnPoints)
            {
                
                if (RequiredDirection == roomSpawnPoint.Direction)
                { 
                    continue;
                }
                
                List<Room> accessRooms = GetAccessRooms(DirectionsOperations.GetOppositeDirection(roomSpawnPoint.Direction));
                if (accessRooms.Count > 0)
                {
                    newRooms.Add(Instantiate(accessRooms[Random.Range(0, accessRooms.Count)], roomSpawnPoint.transform.position, Quaternion.identity));
                    newRooms[^1].RequiredDirection = DirectionsOperations.GetOppositeDirection(roomSpawnPoint.Direction);
                    newRooms[^1].levelGenerator = levelGenerator;
                    levelGenerator.SpawnedRooms.Add(newRooms[^1]);
                    levelGenerator.countEmptyPassages += newRooms[^1].Directions.Length - 2;
                    Debug.Log(newRooms[^1].name);
                    Debug.Log(levelGenerator.countEmptyPassages);
                }
            }
            
            return newRooms.ToArray();
        }

        private List<Room> GetAccessRooms(Direction requiredDirection)
        {
            List<Room> accessRooms = new List<Room>();
            
            foreach (var room in levelGenerator.RoomsPrefabs)
            {
                for (int i = 0; i <= Mathf.Clamp(levelGenerator.CountRooms - levelGenerator.SpawnedRooms.Count, 1, 4); i++)
                {
                    foreach (var directionsCombination in DirectionsOperations.GenerateDirectionsCombinations(i))
                    {
                        if (room.Directions.SequenceEqual(directionsCombination) && 
                            room.Directions.Contains(requiredDirection) &&
                            !(room.Directions.Length == 1 && levelGenerator.countEmptyPassages == 1 &&
                              levelGenerator.CountRooms - levelGenerator.SpawnedRooms.Count > 1) &&
                            !(levelGenerator.CountRooms - levelGenerator.SpawnedRooms.Count - room.Directions.Length < levelGenerator.countEmptyPassages - 1))
                            // !(levelGenerator.countEmptyPassages + room.Directions.Length - 2 > levelGenerator.CountRooms - levelGenerator.SpawnedRooms.Count))
                        {
                            accessRooms.Add(room);
                        }
                    }   
                }
            }

            return accessRooms;
        }
    }
}
