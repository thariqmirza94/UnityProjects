using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _firstPage;
    [SerializeField] private GameObject _BackToPauseButton;
    [SerializeField] private GameObject _BackToMainMenuButton;
    public bool fromPaused = false;
    public void ButtonBackToMain()
    {
        _uiManager.OpenMainMenu();
    }
    public void ButtonBackToPause()
    {
        _uiManager.OpenPauseMenu();
    }

    public void ButtonNextPage()
    {
        _firstPage.SetActive(false);
    }
    public void ButtonPreviousPage()
    {
        _firstPage.SetActive(true);
    }

    private void OnEnable()
    {
        _firstPage.SetActive(true);

        if (fromPaused) // Opened from pause menu
        {
            _BackToPauseButton.SetActive(true);
            _BackToMainMenuButton.SetActive(false);
        }
        else // Opened from main menu
        {
            _BackToPauseButton.SetActive(false);
            _BackToMainMenuButton.SetActive(true);
        }
    }

}
