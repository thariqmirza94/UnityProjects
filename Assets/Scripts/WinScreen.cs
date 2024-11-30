using System.Collections;
using UnityEngine;

public class PlayerEscape : MonoBehaviour
{
    [SerializeField] private GameObject _WinScreen; // Win Screen.
    [SerializeField] private UIManager _uiManager;     // Reference to the UIManager.

    public void MainMenu()
    {
        _uiManager.OpenMainMenu(); // Go back to main menu.
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application is quitting.");
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor.
        Cursor.visible = true; // Toggle cursor visible.
    }
}
