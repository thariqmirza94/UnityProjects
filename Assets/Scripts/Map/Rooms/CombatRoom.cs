using UnityEngine;

public class CombatRoom : Room
{
    // Array for random monster spawn
    [SerializeField] private Monster[] _normalMonsterPrefab;

    // Game object for spawning position information
    [SerializeField] private Transform _spawnPoint;
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
        // Instantiate random monster and monster set up
        SpawnAndInitializeMonster();
        _interaction = Object.FindAnyObjectByType<Interaction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Combat Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");

            _isPlayerInThisRoom = true;
            _playerInThisRoom = other.gameObject;
            _playerController = other.GetComponent<PlayerController>();
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
            Debug.Log("Player left Combat Room.");
            _isPlayerInThisRoom = false;
        }
    }

    private void SpawnAndInitializeMonster()
    {
        Monster monsterInstance = Instantiate(
            _normalMonsterPrefab[Random.Range(0, _normalMonsterPrefab.Length)],
            transform
        );
        monsterInstance.transform.position = _spawnPoint.position;

        if (monsterInstance.TryGetComponent<NormalMonster>(out NormalMonster normalMonster))
        {
            normalMonster.SetUp();
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
            _playerInThisRoom.transform.LookAt(_monsterBattlePosition.position);
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
                //Set player rotation to normal
                _playerInThisRoom.transform.rotation = Quaternion.identity;

                rb.isKinematic = false; // Re-enable physics
            }
        }
    }
}