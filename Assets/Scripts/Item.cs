using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract int ID { get; } // Item ID
    public Sprite Icon; // Sprite for using in Inventory Display
    public abstract string description { get; }
    protected abstract string _dynamicText { get; set; }
    protected int randomNumber(int maxIndex) // random number generator
    {
        int RanNum = Random.Range(1, maxIndex + 1);
        return RanNum;
    }

    public abstract string ItemActionText();

}