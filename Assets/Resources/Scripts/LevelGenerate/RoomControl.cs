using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    public class RoomControl : MonoBehaviour
    {
        private readonly List<SpawnPoint> _passageSpawnPoints = new();
        private readonly List<SpawnPoint> _roomSpawnPoints = new();
        [field: SerializeField]
        public Direction[] Directions { get; private set; }

        private Direction? _alreadySpawnDirection;

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
                    _roomSpawnPoints.Add(spawnPoint);
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

            if (_passageSpawnPoints.Count != Directions.Length || _roomSpawnPoints.Count != Directions.Length)
            {
                Debug.LogWarning("Counts of passage or room spawn points and directions doesn't match");
            }
        }

        public void SpawnPassages(GameObject leftRightPassage, GameObject bottomTopPassage)
        {
            foreach (var passageSpawnPoint in _passageSpawnPoints)
            {
                if (passageSpawnPoint.Direction == _alreadySpawnDirection)
                {
                    continue;
                }

                if (passageSpawnPoint.Direction == Direction.Left || passageSpawnPoint.Direction == Direction.Right)
                {
                    Instantiate(leftRightPassage, passageSpawnPoint.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(bottomTopPassage, passageSpawnPoint.transform.position,
                        Quaternion.identity);
                }   
            }
        }

        public RoomControl[] SpawnRooms(RoomControl[] rooms, int countRooms, ref int countSpawnedRooms, ref int countEmptyPassages)
        {
            List<RoomControl> newRooms = new List<RoomControl>();
            foreach (var roomSpawnPoint in _roomSpawnPoints)
            {
                List<RoomControl> accessRooms = new List<RoomControl>();
                
                if (_alreadySpawnDirection == roomSpawnPoint.Direction)
                { 
                    continue;
                }
                
                Direction requiredDirection;
                switch (roomSpawnPoint.Direction)
                {
                    case Direction.Top:
                        requiredDirection = Direction.Bottom;
                        break;
                    case Direction.Right:
                        requiredDirection = Direction.Left;
                        break;
                    case Direction.Bottom:
                        requiredDirection = Direction.Top;
                        break;
                    case Direction.Left:
                        requiredDirection = Direction.Right;
                        break;
                    default:
                        Debug.LogWarning("Room spawn point doesn't have direction");
                        continue;
                }
                
                foreach (var room in rooms)
                {
                    for (int i = 0; i <= Mathf.Clamp(countRooms - countSpawnedRooms, 1, 4); i++)
                    {
                        foreach (var directionsCombination in DirectionsOperations.GenerateDirectionsCombinations(i))
                        {
                            if (room.Directions.SequenceEqual(directionsCombination) && room.Directions.Contains(requiredDirection) &&
                                !(room.Directions.Length == 1 && countEmptyPassages == 1 && countRooms - countSpawnedRooms > 1))
                            {
                                accessRooms.Add(room);
                            }
                        }   
                    }
                }
                if (accessRooms.Count > 0)
                {
                    newRooms.Add(Instantiate(accessRooms[Random.Range(0, accessRooms.Count)], roomSpawnPoint.transform.position, Quaternion.identity));
                    newRooms[^1]._alreadySpawnDirection = requiredDirection;
                    countSpawnedRooms += newRooms[^1].Directions.Length - 1;
                    Debug.Log(countSpawnedRooms);
                    countEmptyPassages -= 1;
                }
            }
            
            return newRooms.ToArray();
        }
    }
}
