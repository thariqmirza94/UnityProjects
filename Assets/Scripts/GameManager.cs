using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Map gameMap;
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("GameManager Start");
        gameMap = new Map();
        Debug.Log("GameManager Map Created");
        StartGame();
    }

    // Update is called once per frame
    public void StartGame()
    {
        Debug.Log("Hello, World");
    }
}
