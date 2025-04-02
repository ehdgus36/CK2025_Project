using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMixtureSystem : MonoBehaviour
{


    [SerializeField] List<Card> CardData;
    [SerializeField] SlotGroup CardDataSlotGroup;
    [SerializeField] ManaGauge ManaGauge;

    [SerializeField] Button SeletButton;


    public void Start()
    {
        SeletButton.onClick.AddListener(SelectionCard);
    }


    public void AttackButtonEvent()
    {
        SelectionCard();
    }

    public void Update()
    {
       
    }

    void SelectionCard()
    {
        CardData = CardDataSlotGroup.ReadData<Card>();
        AttackData attackData = new AttackData();

        for (int i = 0; i < CardData.Count; i++)
        {

            object type = CardData[i];

            switch (type)
            {
                case NomalCard N:
                    attackData.Damage += N.GetDamage();
                    Debug.Log(N.name);
                    break;
                case PropertyCard P:

                    attackData.Damage += P.GetDamage();

                    Debug.Log(P.name);
                    break;
                case UPGradeCard U:
                    attackData.Damage += U.GetDamage();

                    Debug.Log(U.name);
                    break;
            }
        }
    }

    public void ManaCostCalculate()
    {
       
    }
}
