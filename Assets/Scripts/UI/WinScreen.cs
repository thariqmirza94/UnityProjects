using System.Collections;
using UnityEngine;

public class PlayerEscape : MonoBehaviour
{
    [SerializeField] private GameObject _EscapeImage; // UI image displayed during the escape sequence.
    [SerializeField] private GameObject _EndScreen;   // UI screen displayed after the escape sequence ends. 
    [SerializeField] private UIManager _uiSystem;     // Reference to the UIManager for managing UI transitions.

    public void MainMenu()
    {
        _uiSystem.OpenMainMenu(); // Open the main menu via UIManager.
    }

    public void QuitGame()
    {
        Application.Quit(); // Exit the application.
        Debug.Log("Application is quitting."); // Debug message for testing.
    }

    private void OnEnable()
    {
        StartCoroutine(EscapeSequence()); // Begin the escape sequence coroutine.
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor for interaction.
        Cursor.visible = true; // Make the cursor visible.
    }

    IEnumerator EscapeSequence()
    {
        yield return new WaitForSeconds(22);

        _EscapeImage.SetActive(true); // Show the escape image.

        yield return new WaitForSeconds(5); // Wait for 5 seconds.

        _EscapeImage.SetActive(false); // Hide the escape image.
        _EndScreen.SetActive(true); // Show the end screen.
    }
}