using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    int mapSize = 9;
    public int MapSize => mapSize;
    RoomBase[] Rooms;

    // Update is called once per frame
    public Map()
    {
        CreateMap();
        VisualizeMap();
    }
    private void CreateMap()
    {
        
    }
    private void VisualizeMap()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                var mapRoomRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
                mapRoomRepresentation.transform.position = new Vector3(x , 0, z );
            }
        }
    }
}
