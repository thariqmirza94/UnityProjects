using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private RoomBase[] RoomPrefabs;
    [SerializeField] private float RoomSize = 1;
    public const int MapSize = 9;
    readonly Dictionary<Vector2, RoomBase> _rooms = new();
    public Dictionary<Vector2, RoomBase> Rooms => _rooms;


    public void CreateMap()
    {
        for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                Vector2 coords = new Vector2(x * RoomSize, z * RoomSize);

                var roomInstance = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)], transform);

                roomInstance.SetRoomLocation(coords);

                _rooms.Add(coords, roomInstance);
            }
        }

        foreach (var roomByCoordinate in _rooms)
        {
            RoomBase northRoom = FindRoom(roomByCoordinate.Key, Direction.North);
            RoomBase eastRoom = FindRoom(roomByCoordinate.Key, Direction.East);
            RoomBase southRoom = FindRoom(roomByCoordinate.Key, Direction.South);
            RoomBase westRoom = FindRoom(roomByCoordinate.Key, Direction.West);

            roomByCoordinate.Value.SetRooms(northRoom, eastRoom, southRoom, westRoom);
        }
    }
    private RoomBase FindRoom(Vector2 currentRoom, Direction direction)
    {
        RoomBase room = null;
        Vector2 nextRoomCoordinates = new Vector2(-1, -1);
        switch (direction)
        {
            case Direction.North:
                // Determine North Room
                nextRoomCoordinates = currentRoom + (Vector2.up * RoomSize);
                break;
            case Direction.East:
                // east
                nextRoomCoordinates = currentRoom + (Vector2.right * RoomSize);
                break;
            case Direction.South:
                // south
                nextRoomCoordinates = currentRoom + (Vector2.down * RoomSize);
                break;
            case Direction.West:
                // west
                nextRoomCoordinates = currentRoom + (Vector2.left * RoomSize);
                break;
        }

        if (_rooms.TryGetValue(nextRoomCoordinates, out var nextRoom))
        {
            room = nextRoom;
        }

        return room;
    }
}
