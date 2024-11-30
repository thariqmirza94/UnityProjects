using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _Layouts; // Array of UI layouts.

    private enum MenuLayout
    {
        Main = 0,         // Main Menu.
        GameHud = 1,       // In-Game HUD.
        Pause = 2,        // Pause Menu.
        Combat = 3,       // Combat Menu.
        GameOver = 4,     // Game Over Screen.
        Guide = 5,    // Guide Menu.
        WinScreen = 6, // Win Screen.
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
        SetLayout(MenuLayout.GameHud);
    }

    public void OpenPauseMenu()
    {
        SetLayout(MenuLayout.Pause);
    }

    public void OpenBattleMenu()
    {
        SetLayout(MenuLayout.Combat);
    }

    public void OpenGameOverScreen()
    {
        SetLayout(MenuLayout.GameOver);
    }

    public void GuideScreen()
    {
        SetLayout(MenuLayout.Guide);
    }

    public void OpenEscapeScreen()
    {
        SetLayout(MenuLayout.WinScreen);
    }
}
