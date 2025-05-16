using System.Net.Http.Headers;
using UnityEngine;

public class UPGradeCard : Card
{
    public override int GetDamage()
    {
        return 0;
    }

    public virtual AttackData UpGradeCards(NormalCard nomalCard, PropertyCard propertyCard, AttackData data) { return data; }
   
}


