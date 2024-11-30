using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private InGameHud _gameHud;
    [SerializeField] private GameManager _gameManager;
    public void ButtonStartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); // Load the game scene.
        _gameManager.StartGame();                                     // Start the game
        _uiManager.OpenInGameHud();                                      // Opens in-game HUD.
        _gameHud.SetUp();                                                // Set up the in-game HUD.
    }
    public void GuideButton()
    {
        _uiManager.GuideScreen(); // Opens How to Play screen.
    }
    public void ButtonQuitGame()
    {
        Application.Quit(); // Exit application.
        Debug.Log("Application is quitting."); 
    }
}
