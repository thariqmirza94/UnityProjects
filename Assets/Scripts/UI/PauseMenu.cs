using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIManager _uiSystem;      // Reference to the UIManager for screen transitions.
    [SerializeField] private InGameHud _ingameHud;     // Reference to the InGameHud for managing in-game UI state.
    [SerializeField] private GameGuide _gameGuide;     // Reference to the GameGuide menu.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiSystem.OpenInGameHud(); // Switch back to the in-game HUD.
            _ingameHud.gamePaused = false; // Resume the game.
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor for interaction.
        Cursor.visible = true;                   // Make the cursor visible.
        Time.timeScale = 0f;                     // Pause the game time.
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f; // Resume the game time.
    }

    public void ResumeButton()
    {
        _uiSystem.OpenInGameHud(); // Switch back to the in-game HUD.
        _ingameHud.gamePaused = false; // Resume the game.
    }

    public void QuitGameButton()
    {
        Application.Quit(); // Exit the application.
        Debug.Log("Application is quitting.");
    }

    public void GameGuideButton()
    {
        _gameGuide.fromPaused = true;
        _uiSystem.OpenGameGuideMenu(); // Opens game guide menu.
    }
}