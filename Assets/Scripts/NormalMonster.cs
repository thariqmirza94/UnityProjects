using Unity.VisualScripting;
using UnityEngine;

public class NormalMonster : Monster
{
    private Player _player;

    public override int max_hp { get; set; }
    public override int cur_hp { get; set; }

    public override int damage { get; set; }

    public void SetUp()
    {
        _player = Object.FindAnyObjectByType<Player>();

        if (_player == null)
        {
            Debug.LogWarning("Monster script can't find player instance");
        }
        max_hp = Random.Range(8, 13);
        cur_hp = max_hp;
        damage = 5;
    }

    public override string Attack()
    {
        string attackResult = "";
        damage = Random.Range(0, damage);

        if (damage == 0)
        {
            attackResult = "The boss monster attacked, but you dodged!";
        }
        return attackResult; // Return the result to be used elsewhere
    }

    public void MonsterDead()
    {
        this.gameObject.SetActive(false);
    }
}