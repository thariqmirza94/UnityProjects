using System.Collections;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private UseItemButton _useItemButton;
    [SerializeField] private TMP_Text _battleProgress;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _RunAwayButton;
    [SerializeField] private BattleHud _battleHud;

    private Player _player;
    private Interaction _interaction;
    private Monster _monster;
    private string _itemActionText;

    public void SetUp(Player player)
    {
        _player = player;
        _interaction = _player.gameObject.GetComponent<Interaction>();
    }

    private void OnEnable()
    {
        _useItemButton.SetUp();
    }

    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    public void GetItemAction(string itemActionText)
    {
        _itemActionText = itemActionText;
    }

    public void StartBattle()
    {
        //Update player health and monster health UI
        _battleHud.OnHealthChange(_player.curHP, _player.maxHp, true);
        _battleHud.OnHealthChange(_monster.cur_hp, _monster.max_hp, false);

        _useItemButton.ItemActionCompleted = false;
        _battleProgress.color = Color.black;
        _battleProgress.text = "Choose one item to use.";
        _interaction.isPlayerFighting = true;

        Room battleRoom = _interaction.currentBattleRoom;
        if (battleRoom != null && battleRoom is CombatRoom combatRoom)
        {
            combatRoom.ReadyToBattle();
        }
        else if (battleRoom != null && battleRoom is BossRoom bossRoom)
        {
            bossRoom.ReadyToBattle();
        }
        else
        {
            Debug.LogWarning("Battle room is null in battle manager.");
        }
        //Start player turn
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        _RunAwayButton.SetActive(true);
        Debug.Log("Player's turn started.");

        _battleProgress.color = Color.black;
        _battleProgress.text = "Choose one item to use.";

        yield return new WaitUntil(() => _useItemButton.ItemActionCompleted == true);

        _RunAwayButton.SetActive(false);
        _battleProgress.color = Color.blue;
        _battleProgress.text = "Rolling dice...";

        yield return new WaitForSeconds(2f);

        _battleProgress.text = _itemActionText;
        _useItemButton.button.interactable = false;

        //Update player health and monster health UI
        _battleHud.OnHealthChange(_player.curHP, _player.maxHp, true);
        _battleHud.OnHealthChange(_monster.cur_hp, _monster.max_hp, false);

        yield return new WaitForSeconds(3f);

        if (_monster.cur_hp <= 0)
        {
            // Disable monster
            if (_monster is BossMonster bossMonster)
            {
                bossMonster.BossMonsterDead();
            }
            else if (_monster is NormalMonster normalMonster)
            {
                normalMonster.MonsterDead();
            }
            yield return new WaitForSeconds(1f);

            EndBattle();

            _useItemButton.ItemActionCompleted = false;
            _useItemButton.button.interactable = true;
            
            _uiManager.OpenInGameHud();

            yield break; // Stop further actions
        }

        StartCoroutine(MonsterTurn());
    }

    IEnumerator MonsterTurn()
    {
        Debug.Log("Monster's turn started.");

        yield return new WaitForSeconds(1f);

        _battleProgress.color = Color.red;
        _battleProgress.text = "Rolling dice...";

        yield return new WaitForSeconds(2f);

        // Attack player and return proper text
        _battleProgress.text = _monster.Attack();

        //Update player health and monster health UI
        //_battleHud.OnHealthChange(_player.curHP, _player.maxHp, true);
        _battleHud.OnHealthChange(_monster.cur_hp, _monster.max_hp, false);

        yield return new WaitForSeconds(3f);

        //Use item button interactable for next player turn
        _useItemButton.ItemActionCompleted = false;
        _useItemButton.button.interactable = true;

        if (_player.curHP <= 0)
        {
            Debug.Log("Player defeated. Game Over.");
            EndBattle();
            _uiManager.OpenGameOverScreen();
            yield break; // Stop further actions
        }
        StartCoroutine(PlayerTurn());
    }

    public void EndBattle()
    {
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
}