using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Reroll_Card : MonoBehaviour
{
    public void Excute(SlotGroup CardSloats, out int discardCount)
    {
        discardCount = CardSloats.ReadData<Card>().Count;

        StartCoroutine(ExcuteReroll(CardSloats));
    }

    IEnumerator ExcuteReroll(SlotGroup CardSloats) // 더 좋은 방법 찾아보기
    {
        yield return new WaitForSeconds(.95f);
        for (int i = 0; i < CardSloats.Getsloat().Length; i++)
        {
            if (CardSloats.Getsloat()[i].ReadData<Card>() != null)
            {
                GameManager.instance.CardCemetery.Insert(CardSloats.Getsloat()[i].ReadData<Card>());
            }
        }

        yield return new WaitForSeconds(.62f);
        GameManager.instance.PlayerCDSlotGroup.PlayerTurnDrow();
    }
}
