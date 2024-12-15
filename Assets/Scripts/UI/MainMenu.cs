using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIManager _uiSystem;
    [SerializeField] private InGameHud _gameHud;
    [SerializeField] private GameController _gameController;

    public void ButtonStartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); // Load the game scene.
        _gameController.StartGame();                                       // Start the game via the GameController.
        _uiSystem.OpenInGameHud();                                         // Transition to the in-game HUD.
        _gameHud.SetUp();                                                 // Set up the in-game HUD.

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor for gameplay.
        Cursor.visible = false;                  // Hide the cursor.
    }

    public void GameGuideButton()
    {
        _uiSystem.OpenGameGuideMenu(); // Opens game guide menu.
    }

    public void ButtonQuitGame()
    {
        Application.Quit(); // Exit the application.
        Debug.Log("Application is quitting.");
    }
}