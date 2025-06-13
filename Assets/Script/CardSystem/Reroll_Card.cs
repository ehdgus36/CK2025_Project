using System.Collections.Generic;
using UnityEngine;

public class Reroll_Card : Card
{
    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
        
        for (int i = 0; i < CardSloats.Getsloat().Length; i++)
        {
            if (CardSloats.Getsloat()[i].ReadData<Card>() != null)
            {
                GameManager.instance.CardCemetery.Insert(CardSloats.Getsloat()[i].ReadData<Card>());
            }
        }

        GameManager.instance.PlayerCDSlotGroup.PlayerTurnDrow();

        base.TargetExcute(Target, nextCard);
    }
}
