using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    [SerializeField] private GameObject NorthDoorway, EastDoorway, SouthDoorway, WestDoor;          // Doorways for each direction (north, east, south, west)
    [SerializeField] public GameObject RoomLight;                                                   // Room light object

    private RoomBase _north, _east, _south, _west;

    public RoomBase North => _north;
    public RoomBase East => _east;
    public RoomBase South => _south;
    public RoomBase West => _west;

    private Vector2 _roomPosition;
    public Vector2 RoomPosition => _roomPosition;
    private Inventory _playerInventory;
    public void SetPlayerInventory(ref Inventory inventory)
    {
        _playerInventory = inventory;
    }
    public virtual void SetRoomLocation(Vector2 coordinates)
    {
        // X, Z plane
        transform.position = new Vector3(coordinates.x, 0, coordinates.y);
        _roomPosition = coordinates;
        Console.WriteLine("Room " + _roomPosition + " Created!");
    }
    public void SetRooms(RoomBase roomNorth, RoomBase roomEast, RoomBase roomSouth, RoomBase roomWest)
    {
        _north = roomNorth;
        NorthDoorway.SetActive(_north == null);
        _east = roomEast;
        EastDoorway.SetActive(_east == null);
        _south = roomSouth;
        SouthDoorway.SetActive(_south == null);
        _west = roomWest;
        WestDoor.SetActive(_west == null);
    }
    public virtual void OnRoomEntered()
    {
        Debug.Log("Empty Room Entered");
    }

    public virtual void OnRoomSearched()
    {
        Debug.Log("Empty Room Searched");
    }

    public virtual void OnRoomExited()
    {
        Debug.Log("Empty Room Exited");
    }
}