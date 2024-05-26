using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Resources.Scripts.LevelGenerate.Room;
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
        public Room.Room GetRoomByDirections(Direction[] directions)
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
    
        public bool IsBusyOtherRoomPoint(SpawnPoint spawnPoint, Room.Room excludedRoom)
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
    
        public Room.Room DeleteDirection(Direction deleteDirection, Room.Room room)
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
            
            Room.Room newRoom =  Instantiate(GetRoomByDirections(newDirections.ToArray()), room.transform.position, Quaternion.identity);
            newRoom.RequiredDirection = room.RequiredDirection;
            newRoom.Type = room.Type;
            newRoom.levelGenerator = room.levelGenerator;
            Destroy(room.gameObject);
            levelGenerator.countEmptyPassages -= 1;
            return newRoom;
        }

        public Room.Room[] GetRoomsByType(RoomType roomType)
        {
            List<Room.Room> rooms = new List<Room.Room>();
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
        public Room.Room GetActiveRoom()
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
