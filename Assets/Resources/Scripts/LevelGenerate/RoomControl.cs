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

        public RoomControl[] SpawnRooms(RoomControl[] rooms, int countRooms, ref int countSpawnedRooms)
        {
            List<RoomControl> newRooms = new List<RoomControl>();
            foreach (var roomSpawnPoint in _roomSpawnPoints)
            {
                List<RoomControl> accessRooms = new List<RoomControl>();
                foreach (var room in rooms)
                {
                    foreach (var directionsCombination in DirectionsOperations.GenerateDirectionsCombinations(countRooms - countSpawnedRooms))
                    {
                        if (room.Directions.SequenceEqual(directionsCombination))
                        {
                            accessRooms.Add(room);
                        }
                    }
                }

                if (accessRooms.Count > 0)
                {
                    newRooms.Add(Instantiate(accessRooms[Random.Range(0, accessRooms.Count - 1)], roomSpawnPoint.transform.position, Quaternion.identity));
                    countSpawnedRooms += newRooms[^1].Directions.Length - 1;
                }
            }

            return newRooms.ToArray();
        }
    }
}
