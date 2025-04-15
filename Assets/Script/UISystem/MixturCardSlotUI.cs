using UnityEngine;
using UnityEngine.EventSystems;

enum CardType
{ 
NOMAL ,SPECIAL , UP_GRADE
}
public class MixtureCardSlotUI : SlotUI
{
    [SerializeField]CardType CardType;

    public override void InsertData(GameObject data)
    {

        
        switch (CardType)// data에 타입에 맞는 클래스있는지 확인 없으며 Return;
        {
            case CardType.NOMAL:
                if (data.GetComponent<NomalCard>() == null)
                    return;
                break;
            case CardType.SPECIAL:
                if (data.GetComponent<PropertyCard>() == null)
                    return;
                break;
            case CardType.UP_GRADE:
                if (data.GetComponent<UPGradeCard>() == null)
                    return;
                break;
            default:
                return;
                
                
        }

        
        //현재 타입에 맞는 클래스가 있으면 값넣기
        base.InsertData(data);
    }
}
