using System.Collections.Generic;
using Spine;
using UnityEngine;

public class Random_Card : Card
{
    CardData StartCardData;

    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < CardSloats.Getsloat().Length; i++)
        {
            if (CardSloats.Getsloat()[i].ReadData<Card>() != null)
            {
                cards.Add(CardSloats.Getsloat()[i].ReadData<Card>());
            }
        }
        StartCardData = cardData;

        cardData = cards[Random.Range(0, cards.Count)].cardData;
        

        base.TargetExcute(Target, nextCard);

    }

    public override void CompleteEvent(TrackEntry entry)
    {
        cardData = StartCardData;
        base.CompleteEvent(entry);
    }
}
