using UnityEngine;

public class StartingRoom : Room
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Starting Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player left Starting Room.");
        }
    }
}