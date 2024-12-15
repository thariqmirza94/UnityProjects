using UnityEngine;

public class Player : MonoBehaviour 
{
    public int maxHp = 10; // max health points
    private int _curHP = 8; // current health points
    public int curHP
    {
        get => _curHP;
        set => _curHP = Mathf.Clamp(value, 0, maxHp); // Clamp curHP between 0 and maxHp
    }
    public void Initialize()
    {
        maxHp = 10;
        _curHP = 10;
        Debug.Log("Initialize() executed. Current HP set to: " + _curHP);
    }
}