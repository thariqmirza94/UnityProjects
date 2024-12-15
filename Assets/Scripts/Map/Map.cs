using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour
{
    [SerializeField] private Room CombatRoomPrefab;
    [SerializeField] private Room TreasureRoomPrefab;
    [SerializeField] private Room StartingRoomPrefab;
    [SerializeField] private Room BossRoomPrefab;
    [SerializeField] private float RoomSize = 30;
    private const int MapSize = 9;
    public Dictionary<Vector2, Room> RoomDic = new(); // Stores room instances with their grid coordinates.

    // Room weights in percentage (normalized to 100%)
    private readonly Dictionary<Room, float> roomWeights = new Dictionary<Room, float>();

    public void SetUp()
    {
        // Assign weights to each room prefab
        roomWeights.Add(CombatRoomPrefab, 50f);
        roomWeights.Add(TreasureRoomPrefab, 50f);
    }

    public void CreateMap(ref Inventory inventory)
    {
        // Instantiate starting room at proper coords
        Vector2 startCoords = new Vector2(0, 0);
        var startingRoomInstance = Instantiate(StartingRoomPrefab, transform);
        startingRoomInstance.SetRoomLocation(startCoords);
        startingRoomInstance.SetPlayerInventory(ref inventory);
        RoomDic.Add(startCoords, startingRoomInstance);

        // Instantiate boss room at proper coords
        Vector2 bossCoords = new Vector2((MapSize - 1) * RoomSize, (MapSize - 1) * RoomSize);
        var bossRoomInstance = Instantiate(BossRoomPrefab, transform);
        bossRoomInstance.SetRoomLocation(bossCoords);
        bossRoomInstance.SetPlayerInventory(ref inventory);
        RoomDic.Add(bossCoords, bossRoomInstance);

        for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                // Skip the starting room position (0, 0) and boss room position (last cell)
                if ((x == 0 && z == 0) || (x == MapSize - 1 && z == MapSize - 1))
                    continue;

                // Coordinates for room instance
                Vector2 coords = new Vector2(x * RoomSize, z * RoomSize);

                // Create a room instance based on weighted selection
                Room selectedRoomPrefab = GetRandomRoomByWeight();
                Room roomInstance = Instantiate(selectedRoomPrefab, transform);

                // Store that coordinates in room instance and move it to the coordinate location
                roomInstance.SetRoomLocation(coords);
                roomInstance.SetPlayerInventory(ref inventory);
                RoomDic.Add(coords, roomInstance);
            }
        }

        // Loop each room in room dictionary and set connections
        foreach (var _room in RoomDic)
        {
            Vector2 currentCoords = _room.Key;

            Room northRoom = FindRoomAtPosition(currentCoords + Vector2.up * RoomSize);
            Room eastRoom = FindRoomAtPosition(currentCoords + Vector2.right * RoomSize);
            Room southRoom = FindRoomAtPosition(currentCoords + Vector2.down * RoomSize);
            Room westRoom = FindRoomAtPosition(currentCoords + Vector2.left * RoomSize);

            _room.Value.SetRooms(northRoom, eastRoom, southRoom, westRoom);
        }
    }

    private Room GetRandomRoomByWeight()
    {
        float totalWeight = roomWeights.Values.Sum();
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var room in roomWeights)
        {
            cumulativeWeight += room.Value;
            if (randomValue <= cumulativeWeight)
            {
                return room.Key;
            }
        }

        // Fallback (should never happen)
        return roomWeights.Keys.First();
    }

    private Room FindRoomAtPosition(Vector2 coordinates)
    {
        if (RoomDic.TryGetValue(coordinates, out Room room))
        {
            return room;
        }
        return null;
    }
}