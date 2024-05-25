using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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
                if (room.Directions.SequenceEqual(directions))
                {
                    return room;
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
