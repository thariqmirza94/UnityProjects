using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour
{
    [SerializeField] private UIManager _uiSystem;
    [SerializeField] private BattleHud _battleHud;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _playerHealth;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private UseItemButton _useItemButton;
    [SerializeField] private GameObject _inventoryScreen;
    private Player _player;

    public bool gamePaused = true;
    private float _timerTime = 0;
    private Inventory _inventory;
    private PlayerController _playerController;

    private void Start()
    {
        // Pause timer
        _timer.text = "Timer Paused";
        _timer.color = Color.yellow;
    }

    public void SetUp()
    {
        gamePaused = false;
        _player = Object.FindAnyObjectByType<Player>();
        OnHealthChange(_player.curHP, _player.maxHp);
        _battleHud.SetUp();

        // FInd Inventory in game scene.
        _inventory = Object.FindAnyObjectByType<Inventory>();
        if (_inventory != null)
        {
            _inventoryUI.SetUp(_inventory);
            _useItemButton.SetUp(_inventory);
        }
        else
        {
            Debug.LogWarning("Inventory is not set correctly.");
        }

        // Find player controller in game scene
        _playerController = Object.FindAnyObjectByType<PlayerController>(); // find player controller
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor
        Cursor.visible = false; //hide cursor
    }

    private void Update()
    {
        if (gamePaused) //
            return;
        _timerTime += Time.deltaTime;
        _timer.text = $"{_timerTime,0:0.000}";

        if (Input.GetKeyDown(KeyCode.Escape)) //open pause menu
        {
            gamePaused = true;
            _uiSystem.OpenPauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab)) // Toggle inventory screen in game hud
        {
            // lock player camera and show cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //refresh inventory ui
            _inventoryUI.RefreshInventoryUI();
            _inventoryUI.CloseItemDetail(); //close item detail screen
            _inventoryScreen.SetActive(!_inventoryScreen.activeSelf); // toggle active state of inventory screen

            // if inventory screen is not active, cursor lock and invisible
            if (!_inventoryScreen.activeSelf) 
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            //toggle player controller can move boolean
            if (_playerController.canMove == true)
            {
                _playerController.canMove = false;
            }
            else if (_playerController.canMove == false)
            {
                _playerController.canMove = true;
            }
        }

        OnHealthChange(_player.curHP, _player.maxHp); // update player health
    }

    public void OnHealthChange(float currenthealth, float maxHealth) 
    {
        _healthBar.fillAmount = currenthealth / maxHealth;
        _playerHealth.text = $"{currenthealth} / {maxHealth}";
    }
}