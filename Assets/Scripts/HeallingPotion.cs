using UnityEngine;

public class HealingPotion : Consumable
{
    private static readonly int _id = 4;  // Unique ID for the Dagger class
    public override int ID => _id;        // Implements the ID property in Item
    private static readonly string _description = "This is Healing potion. This item can cause 1-6 random heal to player.";
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
            _dynamicText = $"Dice number was {result}! You are already fully healed. You don't get healed.";
        }
        else
        {
            _player.curHP += healAmount; // Add healAmount to the player's current HP.

            if (_player.curHP == _player.maxHp) // Player's HP is now full.
            {
                _dynamicText = $"Dice number was {result}! You got healed for {healAmount}. Your curHP is now full at {_player.maxHp}.";
            }
            else // Player's HP increased but is not yet full.
            {
                _dynamicText = $"Dice number was {result}! You got healed for {healAmount}. Your current curHP is {_player.curHP}.";
            }
        }
    }

    public override string ItemActionText()
    {
        return _dynamicText;
    }
}