using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMixtureSystem : MonoBehaviour
{


    [SerializeField] List<Card> CardData;
    [SerializeField] SlotGroup CardDataSlotGroup;
    [SerializeField] ManaGauge ManaGauge;

    [SerializeField] Button SeletButton;


    Player AttackPlayer;
    AttackData MadeAttackData;

    


    public void Initialize()
    {
        SeletButton.onClick.AddListener(SelectionCard);
        AttackPlayer = GameManager.instance.GetPlayer();
    }
    public void Start()
    {
       
    }


    public void AttackButtonEvent()
    {
        SelectionCard();
    }

    private void Update()
    {
        ManaCostCalculate(); //야매
    }

    void SelectionCard()
    {
        CardData = CardDataSlotGroup.ReadData<Card>();
        AttackData attackData = new AttackData();

        int totalMana = 0;

        for (int i = 0; i < CardData.Count; i++)
        {

            object type = CardData[i];

            switch (type)
            {
                case NomalCard N:
                    attackData.Damage += N.GetDamage();
                    totalMana += N.GetManCost();
                    Debug.Log(N.name);
                    break;

                case PropertyCard P:

                    attackData.Damage += P.GetDamage();
                    attackData.Buff = P.GetBuff();

                    totalMana += P.GetManCost();
                    Debug.Log(P.name);
                    break;

                case UPGradeCard U:
                    attackData.Damage += U.GetDamage();
                    totalMana += U.GetManCost();
                    Debug.Log(U.name);
                    break;
            }
        }

        CardDataSlotGroup.RemoveDataAll();
        attackData.FromUnit = AttackPlayer;

        MadeAttackData = attackData;
        Enemy targetEnemy = GameManager.instance.GetEnemysGroup().GetEnemy();
        //공격 이펙트 끝나면 데미지 들어가게 연구 필!!! 현재 즉시 데미지
        GameManager.instance.GetAttackManager().Attack(AttackPlayer, targetEnemy, MadeAttackData);

        
    }

    public void ManaCostCalculate()
    {
        CardData = CardDataSlotGroup.ReadData<Card>();
       

        int totalMana = 0;

        for (int i = 0; i < CardData.Count; i++)
        {

            object type = CardData[i];

            switch (type)
            {
                case NomalCard N:
                   
                    totalMana += N.GetManCost();
                    Debug.Log(N.name);
                    break;

                case PropertyCard P:

           
                    totalMana += P.GetManCost();
                    Debug.Log(P.name);
                    break;

                case UPGradeCard U:
                    totalMana += U.GetManCost();
                    Debug.Log(U.name);
                    break;
            }
        }

        Debug.Log(totalMana);
        ManaGauge.SetManaCost(totalMana);

    }
}
