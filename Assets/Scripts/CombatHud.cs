using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Threading;

public class CombatHud : MonoBehaviour
{
    private Player _player;                      // Reference to the Player instance.
    private Interaction _interaction;           // Reference to the Player's interaction component.
    private PlayerController _playerController; // Reference to the Player's movement controller.

    [SerializeField] private UIManager _uiManager;             // Reference to the UIManager for managing HUD transitions.
    [SerializeField] private InventoryUI _inventoryUI;        // Reference to the inventory UI component.
    [SerializeField] private UseItemButton _useItemButton;    // Button for using items in battle.
    [SerializeField] private GameObject _itemDiscription;     // UI element displaying item descriptions.
    [SerializeField] private CombatManager _combatManager;    // Reference to the BattleManager for battle logic.

    [SerializeField] private Image _currentHealth_P;          // Player's current health.
    [SerializeField] private Image _currentHealth_E;          // Enemy's current health.
    [SerializeField] private TMP_Text _playerMaxHealth;       // Player max health.
    [SerializeField] private TMP_Text _enemyMaxHealth;      // Enemy max health.

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
        _combatManager.SetUp(_player);
    }

    public void ButtonRunAway()
    {
        _uiManager.OpenInGameHud();

        Debug.Log("Battle ended.");
        _interaction.isPlayerFighting = false;

        RoomBase currentBattleRoom = _interaction.currentBattleRoom;

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
            _currentHealth_E.fillAmount = currentHealth / maxHealth;
            _enemyMaxHealth.text = $"{currentHealth}/{maxHealth}";
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _playerController.canMove = false;

        _monster = _interaction.ReturnDetectedMonster();

        _useItemButton.GetMonster(_monster);
        _combatManager.GetMonster(_monster);

        _inventoryUI.RefreshInventoryUI();

        _combatManager.StartBattle();
    }

    private void OnDisable()
    {
        _playerController.canMove = true;
        _itemDiscription.SetActive(false);
    }

}
