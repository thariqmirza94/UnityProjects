using UnityEngine;

public class BossMonster : Monster
{
    [SerializeField] private GameObject _MacePrefab;

    private Player _player;
    public override int max_hp { get; set; }
    public override int cur_hp { get; set; }
    public override int damage { get; set; }

    public void SetUp()
    {
        _player = Object.FindAnyObjectByType<Player>();
        max_hp = 30;
        cur_hp = 30;
        damage = 9;
    }

    public override string Attack()
    {
        string attackResult = "";
        damage = Random.Range(0, damage);

        if (damage == 0)
        {
            attackResult = "The monster attacked you, but it missed!";
        }
        else
        {
            _player.curHP -= damage;
            attackResult = $"The dice roll was {damage}!\nMonster attacked you. You lost {damage} HP points.";
        }

        return attackResult; // Return the result to be used elsewhere
    }

    public void BossMonsterDead()
    {
        Instantiate(_MacePrefab, this.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}