using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera; // Reference to the player's camera for raycasting
    [SerializeField] private float _interactionRange; // Maximum range for interaction

    private bool _isLookingAtSomething = false; // Tracks if the player is looking at an interactable object
    private TextMeshProUGUI _interactionIndicator; // UI element for interaction hints
    private Inventory _inventory; // Reference to the player's inventory

    private UIManager _uiManager; // Reference to the UI manager
    public Monster detectedmonster; // Stores the currently detected monster
    public bool isPlayerFighting = false; // Tracks if the player is currently in a fight
    public Room currentBattleRoom; // Reference to the current battle room

    private RaycastHit _raycastHit; // Cached result of the raycast

    public void SetUp(ref Inventory inventory)
    {
        // Find and assign the interaction indicator UI and UI manager
        GameObject indicatorObject = GameObject.Find("UI_Manager/Indicator");
        GameObject uiManagerObj = GameObject.Find("UI_Manager");

        _uiManager = uiManagerObj.GetComponent<UIManager>();

        if (indicatorObject != null)
        {
            _interactionIndicator = indicatorObject.GetComponent<TextMeshProUGUI>();
            _interactionIndicator.gameObject.SetActive(false); // Hide the indicator by default
        }
        else
        {
            Debug.LogWarning("Interaction indicator UI not found.");
        }

        _inventory = inventory;
    }

    private void Update()
    {
        // Only allow interactions if the player is not in a fight
        if (!isPlayerFighting)
        {
            CheckObjectOnRay(); // Perform a raycast to detect interactable objects
        }

        // Handle interactions when the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && _isLookingAtSomething)
        {
            HandleInteraction(); // Perform the appropriate interaction
        }
    }

    private void CheckObjectOnRay()
    {
        // Reset interaction state and hide the indicator
        _isLookingAtSomething = false;
        _interactionIndicator.gameObject.SetActive(false);

        // Create a ray starting from the player's camera
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);

        // Perform the raycast and check for hits
        if (Physics.Raycast(ray, out _raycastHit, _interactionRange))
        {
            // Check for specific interactable components on the hit object
            if (_raycastHit.collider.TryGetComponent<Item>(out Item item))
            {
                _interactionIndicator.text = "Get Item (E)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<Chest>(out Chest chest))
            {
                _interactionIndicator.text = "Open Chest (E)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<Monster>(out Monster monster))
            {
                _interactionIndicator.text = "Fight (E)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<BreakableWall>(out BreakableWall breakableWall))
            {
                _interactionIndicator.text = "Break the wall (E)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
        }
    }

    private void HandleInteraction()
    {
        // Use the cached RaycastHit to determine the type of interaction
        if (_raycastHit.collider.TryGetComponent<Item>(out Item item))
        {
            PickUpItem(item);
        }
        else if (_raycastHit.collider.TryGetComponent<Chest>(out Chest chest))
        {
            OpenChest(chest);
        }
        else if (_raycastHit.collider.TryGetComponent<Monster>(out Monster monster))
        {
            BattleStart(monster);
        }
        else if (_raycastHit.collider.TryGetComponent<BreakableWall>(out BreakableWall breakableWall))
        {
            BreakWall();
        }
    }

    private void PickUpItem(Item item)
    {
        _inventory.AddItem(item.ID); // Add the item to the inventory
        Destroy(item.gameObject); // Remove the item from the scene
        Debug.Log($"Picked up item: {item.name}");
    }

    private void OpenChest(Chest chest)
    {
        chest.OpenChest(); // Open the chest
        Debug.Log($"Opened chest: {chest.name}");
    }

    private void BattleStart(Monster monster)
    {
        isPlayerFighting = true; // Set the player to fighting mode
        detectedmonster = monster; // Set the detected monster
        Debug.Log($"Started battle with: {monster.name}");
        _interactionIndicator.gameObject.SetActive(false);
        _uiManager.OpenBattleMenu(); // Open the battle menu UI
    }

    private void BreakWall()
    {
        if (_inventory.DoesPlayerHave(7))
        {
            _uiManager.OpenEscapeScreen();
        }
    }

    public Monster ReturnDetectedMonster()
    {
        return detectedmonster;
    }
}