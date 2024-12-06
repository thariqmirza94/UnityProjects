using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : Weapon
{
    [SerializeField] private Monster _monster;

    private static readonly int _id = 4;  // Unique ID for the Mace class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is a Mace. It deals 100 damage to monsters and can also break through soft walls.";
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
    private int _maxDamage = 100;

    protected override string _dynamicText { get; set; }

    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    public override void Attack() // attack monster for random damage 
    {
        int result = maxDamage;
        _monster.cur_hp -= result;
        _dynamicText = $"You attacked the monster with Mace for {result} damage.";
    }

    public override string ItemActionText()
    {
        return _dynamicText;
    }
}