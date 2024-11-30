public abstract class Weapon : Item
{
    // Create an instance of DieRoller in the Weapon class
    public abstract void Attack();
    public abstract int maxDamage { get; set; }
}