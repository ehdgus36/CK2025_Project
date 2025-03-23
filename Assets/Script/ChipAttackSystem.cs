using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipAttackSystem : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] List<Card> CardData;
    [SerializeField] Button AttackButton;
    [SerializeField] SlotGroup CardDataSlotGroup;
    [SerializeField] ManaGauge ManaGauge;

    bool isCard = false;


    public bool GetIsCard() { return isCard; }

    private void OnEnable()
    {
        if (CardDataSlotGroup == null) return;
       
        CardDataSlotGroup.RemoveDataAll();
        ManaGauge.Initialize();
    }
    // 실시간 마나 계산 기능 필요함

    private void Start()
    {
        if (AttackButton == null) return;
        AttackButton.onClick.AddListener(AttackButtonEvent);
    }

    public void AttackButtonEvent()
    {
        SelectionCard();
    }

    public void Update()
    {
        if (CardDataSlotGroup == null) return;


        if (CardDataSlotGroup.ReadData().Count != 0)
        {
            ManaCostCalculate();
            isCard = true;
        }
        else
        {
            ManaGauge.SetManaCost(0);
            isCard = false;
        }
    }

    void SelectionCard()
    {
        List<GameObject> LoadData = CardDataSlotGroup.ReadData();
        List<Card> cardsData = new List<Card>();

        int Total_Damage = 0;
        for (int i = 0; i < LoadData.Count; i++)
        {
            if (LoadData[i].GetComponent<Card>())
            {
                Total_Damage += LoadData[i].GetComponent<Card>().GetDamage();
                cardsData.Add(LoadData[i].GetComponent<Card>());
            }
        }
        GameManager.instance.GetAttackManager().Attack(player,
                                                       GameManager.instance.GetEnemysGroup().GetEnemy(),
                                                       cardsData);

        ManaGauge.UseMana();
        // GameManager.instance.AttackDamage(Total_Damage);
        CardDataSlotGroup.RemoveDataAll();
        
    }

    public void ManaCostCalculate()
    {
        if (ManaGauge == null) return;

        List<GameObject> LoadData = CardDataSlotGroup.ReadData();
        int Total_ManaCost = 0;
        for (int i = 0; i < LoadData.Count; i++)
        {
            if (LoadData[i].GetComponent<Card>())
            {
                Total_ManaCost += LoadData[i].GetComponent<Card>().GetManCost();
            }
        }

        ManaGauge.SetManaCost(Total_ManaCost);
    }
}
