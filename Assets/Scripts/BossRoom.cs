using UnityEngine;

/// <summary>
/// The BossRoom class represents a room where the player fights a boss monster.
/// It handles spawning the boss, managing player interactions, initiating battles, and handling player, monster position during and after the battle.
/// </summary>
public class BossRoom : RoomBase
{
    [SerializeField] private 
        BossMonster _bossMonsterPrefab;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private UIManager _uiSystem;

    [SerializeField] private Transform _playerBattlePosition;
    [SerializeField] private Transform _monsterBattlePosition;
    [SerializeField] private Transform _battleEndPlayerPosition;

    private Interaction _interaction;                   //Player interaction script
    private GameObject _playerInThisRoom;               //Player gameobj in this room
    private GameObject _monsterInThisRoom;              //Monster gameobj in this room
    private bool _isPlayerInThisRoom;                   //Check If player is in this toom
    private PlayerController _playerController;         //Player controller script

    private void Start()
    {
        Monster bossMonsterInstance = Instantiate(_bossMonsterPrefab, transform);
        bossMonsterInstance.transform.position = _spawnPoint.position;
        bossMonsterInstance.transform.Rotate(0, 270f, 0);
        if (bossMonsterInstance.TryGetComponent<BossMonster>(out BossMonster bossMonster))
        {
            bossMonster.SetUp();
        }
        _uiSystem = GameObject.Find("UI_Manager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Boss Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");

            _isPlayerInThisRoom = true;
            _playerInThisRoom = other.gameObject;
            _playerController = other.GetComponent<PlayerController>();
            _interaction = other.GetComponent<Interaction>();
            _monsterInThisRoom = FindMonsterInThisRoom().gameObject;
            _interaction.currentBattleRoom = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Boss Room.");
            _isPlayerInThisRoom = false;
        }
    }

    private Monster FindMonsterInThisRoom()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Monster>(out Monster monster))
            {
                return monster;
            }
        }

        Debug.LogWarning("Monster is not found in this room.");
        return null;
    }

    public void ReadyToBattle()
    {
        if (_isPlayerInThisRoom)
        {
            MoveToBattlePos(_playerInThisRoom.transform, _playerBattlePosition.position);
            MoveToBattlePos(_monsterInThisRoom.transform, _monsterBattlePosition.position);
            _playerController.CameraLookAtObject(_monsterBattlePosition);
        }
    }

    private void MoveToBattlePos(Transform obj, Vector3 targetPosition)
    {
        if (Vector3.Distance(obj.position, targetPosition) > 0.01f)
        {
            obj.position = targetPosition;
        }
    }

    public void MovePlayerToEndBattlePos()
    {
        if (_isPlayerInThisRoom)
        {
            Debug.Log("Battle is ended, moving player to EndBattlePosition");
            Rigidbody rb = _playerInThisRoom.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true; // Temporarily disable physics

                //Move player to battle end position
                _playerInThisRoom.transform.position = _battleEndPlayerPosition.position;

                rb.isKinematic = false; // Re-enable physics
            }
        }
    }
}