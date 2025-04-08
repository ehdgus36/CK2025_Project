using UnityEngine;

public class PropertyCard : Card
{
    [SerializeField] int Damage;
    [SerializeField] int PlusDamage; //노멀 카드가 있을시 발동하는 추가데미지
    [SerializeField] Buff buff;

    public override int GetDamage() { return Damage; }
    public Buff GetBuff()
    {
        if (buff == null)
        {
            buff = GetComponent<Buff>();
        }

        return buff;
    }

    public virtual int SpecialCardPlusDamag(NomalCard card)
    {
        if (card == null) return 0;

        return PlusDamage;
    }
}
