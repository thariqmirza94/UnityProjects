using UnityEngine;

public class HealingPill : Consumable
{
    private static readonly int _id = 3;  // Unique ID for the Healing Pill class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is a Healing pill. Use this item to heal 1-6 random amount of HP.";
    public override string description => _description;

    public override int effect { get; set; } = 6;

    protected override string _dynamicText { get; set; }

    private Player _player;

    public void GetPlayer()
    {
        _player = Object.FindAnyObjectByType<Player>();
    }

    public void Heal()
    {
        int result = randomNumber(effect);
        int healAmount = Mathf.Min(result, _player.maxHp - _player.curHP); // Calculate the actual heal amount.

        if (_player.curHP == _player.maxHp) // Player is already fully healed.
        {
            _dynamicText = $"Dice roll was {result}! You are at full health. You wasted heal.";
        }
        else
        {
            _player.curHP += healAmount; // Add healAmount to the player's current HP.

            if (_player.curHP == _player.maxHp) // Player's HP is now full.
            {
                _dynamicText = $"Dice roll was {result}! You got healed {healAmount}. You at full health {_player.maxHp}.";
            }
            else // Player's HP increased but is not yet full.
            {
                _dynamicText = $"Dice roll was {result}! You got healed {healAmount}. Your current health is {_player.curHP}.";
            }
        }
    }

    public override string ItemActionText()
    {
        return _dynamicText;    
    }
}