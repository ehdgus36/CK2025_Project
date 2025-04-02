using UnityEngine;

public class PropertyCard : Card
{
    [SerializeField] int Damage;
    [SerializeField] Buff buff;

    public override int GetDamage() { return Damage; }
    public Buff GetBuff() { return buff; }
}
