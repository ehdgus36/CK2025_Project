using UnityEngine;

public class CriticalCard : UPGradeCard
{
    [SerializeField] int CriticalDrainage = 1;


    public override AttackData UpGradeCards(NomalCard nomalCard, PropertyCard propertyCard, AttackData data)
    {
        //if (nomalCard != null && propertyCard != null)
        //{
        //    if (nomalCard.GetID() == "1111" && propertyCard.GetID() == "2222")
        //    {
        //        data.Buff.SetBuffDuationTurn(2); // 임시값 하드코딩

        //        data.Type = AttackType.All;
        //        Debug.Log("특수 조건발동");
        //    }

        //    data.Damage = data.Damage * CriticalDrainage;
        //    return data;
        //}

        //if (nomalCard != null || propertyCard != null)
        //{
        //    data.Buff.SetBuffDuationTurn(2); // 임시값 하드코딩

        //    data.Damage = data.Damage * CriticalDrainage;
        //    return data;
        //}

        //data.Damage = data.Damage * CriticalDrainage;
        return data;
    }
}
