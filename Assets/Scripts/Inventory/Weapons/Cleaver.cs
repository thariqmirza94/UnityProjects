using UnityEngine;

public class Cleaver : Weapon
{
    [SerializeField] private Monster _monster;

    private static readonly int _id = 2;  // Unique ID for the Cleaver class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is a Cleaver. This weapon deals 1-12 HP random damage.";
    public override string description => _description;

    public override int maxDamage
    {
        get => _maxDamage;
        set
        {
            Debug.Log($"maxDamage is being set to {value}");
            _maxDamage = value;
        }
    }
    private int _maxDamage = 12;
    protected override string _dynamicText { get; set; }

    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    public override void Attack() // attack monster for random damage 
    {
        int result = randomNumber(maxDamage);
        _monster.cur_hp -= result;
        _dynamicText = $"Dice roll was {result}!\nYou attacked the monster with Cleaver for {result} damage.";
    }

    public override string ItemActionText()
    {
        return _dynamicText;
    }
}