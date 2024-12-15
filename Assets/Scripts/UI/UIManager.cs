using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _Layouts; // Array of UI layouts, indexed by their corresponding MenuLayout enum.

    private enum MenuLayout
    {
        Main = 0,         // Main Menu layout.
        InGame = 1,       // In-Game HUD layout.
        Pause = 2,        // Pause Menu layout.
        Battle = 3,       // Battle Screen layout.
        GameOver = 4,     // Game Over Screen layout.
        GameGuide = 5,    // Game Guide Screen layout.
        PlayerEscape = 6, // Player Escape Screen layout.
    }

    private void Start()
    {
        OpenMainMenu(); // Open the Main Menu layout by default.
    }

    private void SetLayout(MenuLayout layout)
    {
        for (int i = 0; i < _Layouts.Length; i++)
        {
            _Layouts[i].SetActive((int)layout == i); // Activate the selected layout and deactivate others.
        }
    }

    public void OpenMainMenu()
    {
        SetLayout(MenuLayout.Main);
    }

    public void OpenInGameHud()
    {
        SetLayout(MenuLayout.InGame);
    }

    public void OpenPauseMenu()
    {
        SetLayout(MenuLayout.Pause);
    }

    public void OpenBattleMenu()
    {
        SetLayout(MenuLayout.Battle);
    }

    public void OpenGameOverScreen()
    {
        SetLayout(MenuLayout.GameOver);
    }

    public void OpenGameGuideMenu()
    {
        SetLayout(MenuLayout.GameGuide);
    }

    public void OpenEscapeScreen()
    {
        SetLayout(MenuLayout.PlayerEscape);
    }
}
