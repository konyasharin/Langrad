using System;
using System.Collections.Generic;
using System.Linq;
using Resources.Scripts.Actors.Enemies;
using Resources.Scripts.Actors.Player;
using UnityEngine;
using Random = Resources.Scripts.Utils.Random;

namespace Resources.Scripts.LevelGenerate
{
    [RequireComponent(typeof(Collider2D))]
    public class Room : MonoBehaviour
    {
        private readonly List<SpawnPoint> _passageSpawnPoints = new();
        private readonly List<SpawnPoint> _doorSpawnPoints = new();
        private SpawnArea _spawnArea;
        private Collider2D _collider;
        public List<Enemy> Enemies { get; private set; } = new();
        [HideInInspector]
        public LevelGenerator levelGenerator;
        public List<SpawnPoint> RoomSpawnPoints { get; private set; } = new();
        private List<GameObject> _doors = new();
        [field: SerializeField]
        public Direction[] Directions { get; private set; }
        public Direction? RequiredDirection;
        public RoomType? Type;
        public RoomStatus? Status;
        
        private void Awake()
        {
            _spawnArea = GetComponentInChildren<SpawnArea>();
            _collider = GetComponent<Collider2D>();
            foreach (var spawnPoint in GetComponentsInChildren<SpawnPoint>())
            {
                switch (spawnPoint.SpawnPointType)
                {
                    case SpawnPointType.Door: 
                        _doorSpawnPoints.Add(spawnPoint);
                        break;
                    case SpawnPointType.Passage:
                        _passageSpawnPoints.Add(spawnPoint);
                        break;
                    case SpawnPointType.Room:
                        RoomSpawnPoints.Add(spawnPoint);
                        break;
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
            
            if (_passageSpawnPoints.Count != Directions.Length || 
                RoomSpawnPoints.Count != Directions.Length || 
                _doorSpawnPoints.Count != Directions.Length)
            {
                Debug.LogWarning("Counts of passage, room or door spawn points and directions doesn't match");
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
                    Instantiate(levelGenerator.Level.leftRightPassage, passageSpawnPoint.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(levelGenerator.Level.bottomTopPassage, passageSpawnPoint.transform.position,
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
                    newRooms.Add(Instantiate(accessRooms[UnityEngine.Random.Range(0, accessRooms.Count)],
                        roomSpawnPoint.transform.position, Quaternion.identity));
                    newRooms[^1].RequiredDirection = DirectionsOperations.GetOppositeDirection(roomSpawnPoint.Direction);
                    if (newRooms[^1].Type == null || newRooms[^1].Status == null)
                    {
                        newRooms[^1].Type = RoomType.Common;
                        newRooms[^1].Status = RoomStatus.Waiting;
                    }
                    newRooms[^1].levelGenerator = levelGenerator;
                    levelGenerator.SpawnedRooms.Add(newRooms[^1]);
                    levelGenerator.countEmptyPassages += newRooms[^1].Directions.Length - 2;
                }
            }
            
            return newRooms.ToArray();
        }

        private List<Room> GetAccessRooms(Direction requiredDirection)
        {
            List<Room> accessRooms = new List<Room>();
            
            foreach (var room in levelGenerator.Level.roomsPrefabs)
            {
                for (int i = 0; i <= Mathf.Clamp(levelGenerator.Level.countRooms - levelGenerator.SpawnedRooms.Count, 1, 4); i++)
                {
                    foreach (var directionsCombination in DirectionsOperations.GenerateDirectionsCombinations(i))
                    {
                        /*
                         * Условия, которым должна удовлетворять комната (префаб комнаты),
                         * чтобы быть доступной для спавна:
                         * 1) Префаб содержит направление, с которого мы пришли в данную комнату (requiredDirection)
                         * 2) Если нужно еще сгенерировать больше 1 комнаты, и количество пустых
                         * переходов при этом равно 1 (то есть уже мы нигде не сможем сгенерировать комнату), то
                         * мы не можем сгенерировать комнату-тупик (с 1 проходом из которого мы пришли)
                         * 3) Количество комнат которые нужно заспавнить минус количество комнат в префабе должно быть больше,
                         * чем количество еще пустых переходов минус 1 (минус один потому что у нас удалится предыдущий пустой проход)
                         */
                        if (room.Directions.SequenceEqual(directionsCombination) && 
                            room.Directions.Contains(requiredDirection) &&
                            !(room.Directions.Length == 1 && levelGenerator.countEmptyPassages == 1 &&
                              levelGenerator.Level.countRooms - levelGenerator.SpawnedRooms.Count > 1) &&
                            !(levelGenerator.Level.countRooms - levelGenerator.SpawnedRooms.Count - room.Directions.Length < levelGenerator.countEmptyPassages - 1))
                        {
                            accessRooms.Add(room);
                        }
                    }   
                }
            }

            return accessRooms;
        }

        private void CloseRoom()
        {
            foreach (var spawnPoint in _doorSpawnPoints)
            {
                _doors.Add(Instantiate(levelGenerator.Level.door, spawnPoint.transform.position,
                    DirectionsOperations.GetRotation(spawnPoint.Direction)));
            }
        }

        private void OpenRoom()
        {
            foreach (var door in _doors)
            {
                Destroy(door);
            }

            Status = RoomStatus.Completed;
        }

        private void ChangeEnemiesList(Enemy removeEnemy)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i] == removeEnemy)
                {
                    Enemies.RemoveAt(i);
                }
            }

            if (Enemies.Count == 0)
            {
                OpenRoom();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (Status != RoomStatus.Completed && other.CompareTag("Player") && Type == RoomType.Common)
            {
                foreach (var corner in PlayerUtils.GetCorners())
                {
                    if (!_collider.bounds.Contains(corner))
                    {
                        return;
                    }
                }
                CloseRoom();
                Status = RoomStatus.Active;
                foreach (var enemy in Enemies)
                {
                    enemy.moveIsBlock = false;
                    enemy.OnDeath.AddListener(ChangeEnemiesList);
                }
            }
        }

        public void SpawnEnemies()
        {
            int currentSumPrices = 0;
            while (levelGenerator.Enemies.Any(enemy => enemy.SpawnPrice <= levelGenerator.SumSpawnPrices - currentSumPrices))
            {
                List<Enemy> accessEnemies = new List<Enemy>();
                Dictionary<int, int> weights = new Dictionary<int, int>();
                
                foreach (var enemy in levelGenerator.Enemies)
                {
                    if (enemy.SpawnPrice <= levelGenerator.SumSpawnPrices - currentSumPrices)
                    {
                        accessEnemies.Add(enemy);
                    }
                }
                
                for (int i = 0; i < accessEnemies.Count; i++)
                {
                    weights.Add(i, accessEnemies[i].SpawnPrice);
                }
                
                Enemy randomEnemy = Random.GetByWeights(accessEnemies, weights);
                for (int i = 0; i < levelGenerator.Enemies.Length; i++)
                {
                    if (levelGenerator.Enemies[i] == randomEnemy)
                    {
                        Enemies.Add(Instantiate(levelGenerator.Level.enemiesPrefabs[i], _spawnArea.GetRandomPosition(),
                            Quaternion.identity).GetComponent<Enemy>());
                        break;
                    }
                }

                currentSumPrices += randomEnemy.SpawnPrice;
            }
        }
    }
}
