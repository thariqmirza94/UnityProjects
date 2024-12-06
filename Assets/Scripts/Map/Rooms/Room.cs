using UnityEngine;

public abstract class Room : MonoBehaviour
{
    // Doorways for each direction (north, east, south, west)
    [SerializeField] public GameObject NorthDoorWay, EastDoorWay, SouthDoorWay, WestDoorWay;

    // Stone doors for blocking pathways
    [SerializeField] private GameObject StoneDoorNorth, StoneDoorSouth, StoneDoorWest, StoneDoorEast;

    // Room light object
    [SerializeField] public GameObject RoomLight;

    // References to adjacent rooms
    private Room _north, _south, _east, _west;

    // Public accessors for neighboring rooms
    public Room North => _north;
    public Room South => _south;
    public Room West => _west;
    public Room East => _east;

    // Room's 2D grid position in the map
    private Vector2 _roomPosition;
    public Vector2 RoomPosition => _roomPosition;

    // Reference to the player's inventory
    protected Inventory _playerInventory;

    public void SetPlayerInventory(ref Inventory inventory)
    {
        _playerInventory = inventory;
    }

    public virtual void SetRoomLocation(Vector2 coordinates)
    {
        // Move this room to the specified 2D grid position
        transform.position = new Vector3(coordinates.x, 0, coordinates.y);
        // Store the coordinates as the room's position
        _roomPosition = coordinates;
    }

    public void SetRooms(Room NorthRoom, Room EastRoom, Room SouthRoom, Room WestRoom)
    {
        // Assign the neighboring rooms
        _north = NorthRoom;
        _east = EastRoom;
        _south = SouthRoom;
        _west = WestRoom;

        // Manage doorway and stone door visibility for each direction

        // North direction
        NorthDoorWay.SetActive(_north == null); // Show doorway if there's no room to the north
        StoneDoorNorth.SetActive(_north != null); // Show stone door if there's a room to the north

        // East direction
        EastDoorWay.SetActive(_east == null); // Show doorway if there's no room to the east
        StoneDoorEast.SetActive(_east != null); // Show stone door if there's a room to the east

        // South direction
        SouthDoorWay.SetActive(_south == null); // Show doorway if there's no room to the south
        StoneDoorSouth.SetActive(_south != null); // Show stone door if there's a room to the south

        // West direction
        WestDoorWay.SetActive(_west == null); // Show doorway if there's no room to the west
        StoneDoorWest.SetActive(_west != null); // Show stone door if there's a room to the west
    }
}
