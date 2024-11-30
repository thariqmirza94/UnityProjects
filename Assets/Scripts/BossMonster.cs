using Unity.VisualScripting;
using UnityEngine;

public class BossMonster : Monster
{
    [SerializeField] private GameObject _HammerPrefab;

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
            attackResult = "The boss monster attacked, but you dodged!";
        }
        return attackResult; // Return the result to be used elsewhere
    }

    public void BossMonsterDead()
    {
        Instantiate(_HammerPrefab, this.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}