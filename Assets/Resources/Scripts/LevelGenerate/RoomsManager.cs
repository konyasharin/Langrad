using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Resources.Scripts.LevelGenerate.RoomScripts;
using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    public class RoomsManager: MonoBehaviour
    {
        public static RoomsManager Instance { get; private set; }
        [HideInInspector]
        public LevelGenerator levelGenerator;

        private void Awake()
        {
            Instance = this;
        }
    
        [CanBeNull]
        public Room GetRoomByDirections(Direction[] directions)
        {
            foreach (var room in levelGenerator.Level.roomsPrefabs)
            {
                if (room.Directions.Length == directions.Length)
                {
                    bool isSuitable = true;
                    foreach (var direction in directions)
                    {
                        if (!room.Directions.Contains(direction))
                        {
                            isSuitable = false;
                            break;
                        }
                    }

                    if (isSuitable)
                    {
                        return room;
                    }   
                }
            }
            Debug.LogWarning($"Elements of rooms array doesn't have room with this directions");
            return null;
        }
    
        public bool IsBusyOtherRoomPoint(SpawnPoint spawnPoint, Room excludedRoom)
        {
            foreach (var room in levelGenerator.SpawnedRooms)
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
    
        public Room DeleteDirection(Direction deleteDirection, Room room)
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
            newRoom.Type = room.Type;
            newRoom.levelGenerator = room.levelGenerator;
            Destroy(room.gameObject);
            levelGenerator.countEmptyPassages -= 1;
            return newRoom;
        }
        
        public bool IsExpandableRoom(Room room)
        {
            if (room.Type == RoomType.Common && room.Directions.Length != 4)
            {
                return true;
            }

            return false;
        }
        
        public (Room, bool) AddRandomDirection(Room room)
        {
            List<Direction> newDirections = new List<Direction>();
            foreach (var direction in room.Directions)
            {
                newDirections.Add(direction);
            }

            Direction newDirection = DirectionsOperations.GetRandomDirection(room.Directions);
            newDirections.Add(newDirection);

            Direction[] cachedOldDirections = room.Directions; 
            Room newRoom = Instantiate(GetRoomByDirections(newDirections.ToArray()), room.transform.position, Quaternion.identity);
            Destroy(room.gameObject);

            if (IsBusyOtherRoomPoint(newRoom.RoomSpawnPoints.Find(point => point.Direction == newDirection), room))
            {
                Destroy(newRoom.gameObject);
                newRoom = Instantiate(GetRoomByDirections(cachedOldDirections), newRoom.transform.position, Quaternion.identity);
                return (newRoom, false);
            }
            levelGenerator.countEmptyPassages += 1;
            newRoom.Initialize(room.RequiredDirection, room.Type, room.levelGenerator);
            
            return (newRoom, true);
        }

        public Room[] GetRoomsByType(RoomType roomType)
        {
            List<Room> rooms = new List<Room>();
            foreach (var room in levelGenerator.SpawnedRooms)
            {
                if (room.Type == roomType)
                {
                    rooms.Add(room);
                }
            }

            return rooms.ToArray();
        }

        [CanBeNull]
        public Room GetActiveRoom()
        {
            foreach (var room in levelGenerator.SpawnedRooms)
            {
                if (room.Status == RoomStatus.Active)
                {
                    return room;
                }
            }

            return null;
        }
    }
}
