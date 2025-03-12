using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipAttackSystem : MonoBehaviour
{
    [SerializeField] List<Card> CardData;
    [SerializeField] Button AttackButton;
    private SlotGroup CardDataSlotGroup;

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

    void SelectionCard()
    {
        List<GameObject> LoadData = CardDataSlotGroup.ReadData();
        int Total_Damage = 0;
        for (int i = 0; i < LoadData.Count; i++)
        {
            if (LoadData[i].GetComponent<Card>())
            {
                Total_Damage += LoadData[i].GetComponent<Card>().GetDamage();
            }
        }
    }
}
