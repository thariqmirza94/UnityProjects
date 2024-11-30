using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;     
    [SerializeField] private InGameHud _gameHud;    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _uiManager.OpenInGameHud(); // Toggle in-game HUD.
            _gameHud.gamePaused = false; // Resume game.
        }
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlock cursor.
        Cursor.visible = true;                   // Make cursor visible.
        Time.timeScale = 0f;                     // Pause timer.
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f; // Resume timer.
    }
    public void GuideButton()
    {
        _uiManager.GuideScreen(); // Opens How to Play screen.
    }
    public void ResumeButton()
    {
        _uiManager.OpenInGameHud(); // Toggle in-game HUD.
        _gameHud.gamePaused = false; // Resume the game.
    }
    public void ButtonQuitGame()
    {
        Application.Quit(); // Exit application.
        Debug.Log("Application is quitting."); 
    }
}
