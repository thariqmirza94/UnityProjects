using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour
{
    [SerializeField] private RoomBase HealingRoomPrefab;
    [SerializeField] private RoomBase LibraryRoomPrefab;
    [SerializeField] private RoomBase TradeRoomPrefab;
    [SerializeField] private RoomBase CombatRoomPrefab;
    [SerializeField] private RoomBase TreasureRoomPrefab;
    [SerializeField] private RoomBase StartingRoomPrefab;
    [SerializeField] private RoomBase BossRoomPrefab;
    [SerializeField] private float RoomSize = 30;
    private const int MapSize = 9;
    public Dictionary<Vector2, RoomBase> RoomDic = new(); // Stores room instances with their grid coordinates.

    // Room weights in percentage (normalized to 100%)
    private readonly Dictionary<RoomBase, float> roomWeights = new Dictionary<RoomBase, float>();

    public void SetUp()
    {
        // Assign weights to each room prefab
        roomWeights.Add(HealingRoomPrefab, 15f);
        roomWeights.Add(LibraryRoomPrefab, 10f);
        roomWeights.Add(TradeRoomPrefab, 5f);
        roomWeights.Add(CombatRoomPrefab, 50f);
        roomWeights.Add(TreasureRoomPrefab, 20f);
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
                RoomBase selectedRoomPrefab = GetRandomRoomByWeight();
                RoomBase roomInstance = Instantiate(selectedRoomPrefab, transform);

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

            RoomBase northRoom = FindRoomAtPosition(currentCoords + Vector2.up * RoomSize);
            RoomBase eastRoom = FindRoomAtPosition(currentCoords + Vector2.right * RoomSize);
            RoomBase southRoom = FindRoomAtPosition(currentCoords + Vector2.down * RoomSize);
            RoomBase westRoom = FindRoomAtPosition(currentCoords + Vector2.left * RoomSize);

            _room.Value.SetRooms(northRoom, eastRoom, southRoom, westRoom);
        }
    }

    private RoomBase GetRandomRoomByWeight()
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

    private RoomBase FindRoomAtPosition(Vector2 coordinates)
    {
        if (RoomDic.TryGetValue(coordinates, out RoomBase room))
        {
            return room;
        }
        return null;
    }
}
