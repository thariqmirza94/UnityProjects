using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHp = 10; // max health
    private int _currentHP = 8; // current health
    public int curHP
    {
        get => _currentHP;
        set => _currentHP = Mathf.Clamp(value, 0, maxHp); // Clamp curHP between 0 and maxHp
    }
    public void Initialize()
    {
        maxHp = 10;
        _currentHP = 10;
        Debug.Log("Initialize() executed. Current HP set to: " + _currentHP);
    }
}

