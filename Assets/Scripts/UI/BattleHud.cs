using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BattleHud : MonoBehaviour
{
    private Player _player;                      // Reference to the Player instance.
    private Interaction _interaction;           // Reference to the Player's interaction component.
    private PlayerController _playerController; // Reference to the Player's movement controller.

    [SerializeField] private UIManager _uiSystem;             // Reference to the UIManager for managing HUD transitions.
    [SerializeField] private InventoryUI _inventoryUI;        // Reference to the inventory UI component.
    [SerializeField] private UseItemButton _useItemButton;    // Button for using items in battle.
    [SerializeField] private GameObject _itemDiscription;     // UI element displaying item descriptions.
    [SerializeField] private BattleManager _battleManager;    // Reference to the BattleManager for battle logic.

    [SerializeField] private Image _currentHealth_P;          // Player's current health display (UI).
    [SerializeField] private Image _currentHealth_M;          // Monster's current health display (UI).
    [SerializeField] private TMP_Text _playerMaxHealth;       // Player's health text display.
    [SerializeField] private TMP_Text _monsterMaxHealth;      // Monster's health text display.

    public Monster _monster;                                 // Reference to the current monster in the battle.
    private Inventory _inventory;                            // Reference to the player's inventory.

    public void SetUp()
    {
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
        _player = Object.FindAnyObjectByType<Player>();
        _interaction = _player.gameObject.GetComponent<Interaction>();
        _playerController = _player.gameObject.GetComponent<PlayerController>();
        _battleManager.SetUp(_player);
    }

    public void ButtonRunAway()
    {
        _uiSystem.OpenInGameHud();

        Debug.Log("Battle ended.");
        _interaction.isPlayerFighting = false;

        Room currentBattleRoom = _interaction.currentBattleRoom;

        if (currentBattleRoom is CombatRoom combatRoom)
        {
            combatRoom.MovePlayerToEndBattlePos();
        }
        else if (currentBattleRoom is BossRoom bossRoom)
        {
            bossRoom.MovePlayerToEndBattlePos();
        }
    }

    public void OnHealthChange(float currentHealth, float maxHealth, bool isPlayer = true)
    {
        if (isPlayer)
        {
            _currentHealth_P.fillAmount = currentHealth / maxHealth;
            _playerMaxHealth.text = $"{currentHealth}/{maxHealth}";
        }
        else
        {
            _currentHealth_M.fillAmount = currentHealth / maxHealth;
            _monsterMaxHealth.text = $"{currentHealth}/{maxHealth}";
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _playerController.canMove = false;

        _monster = _interaction.ReturnDetectedMonster();

        _useItemButton.GetMonster(_monster);
        _battleManager.GetMonster(_monster);

        _inventoryUI.RefreshInventoryUI();

        _battleManager.StartBattle();
    }

    private void OnDisable()
    {
        _playerController.canMove = true;
        _itemDiscription.SetActive(false);
    }
}