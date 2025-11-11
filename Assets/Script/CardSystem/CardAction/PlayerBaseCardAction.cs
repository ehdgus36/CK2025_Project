using Google.GData.AccessControl;
using Spine;
using System.Collections;
using UnityEngine;

public abstract class PlayerBaseCardAction
{
    protected bool bit1, bit2, bit3, bit4;

    Card thisCard;
    public PlayerBaseCardAction(Card card)
    {
        bit1 = false;
        bit2 = false;
        bit3 = false;
        bit4 = false;

        thisCard = card;
    }

    public abstract IEnumerator StartAction(Player player, Card card , CardData cardData, Enemy Target);

    protected void AnimationEvent(TrackEntry entry, Spine.Event e)
    {
        if (e.Data.Name == "1bit")
        {
            Beat1();
            bit1 = true;
        }
        if (e.Data.Name == "2bit")
        {
            Beat2();
            bit2 = true;
        }
        if (e.Data.Name == "3bit")
        {
            Beat3();
            bit3 = true;
        }
        if (e.Data.Name == "4bit")
        {
            Beat4();
            bit4 = true;
        }
    }

    protected virtual void Beat1() { }
    protected virtual void Beat2() { }
    protected virtual void Beat3() { }
    protected virtual void Beat4() { }

    protected virtual void CompleteEvent(TrackEntry entry)
    {
        if (bit1 == false) bit1 = true;
        if (bit2 == false) bit2 = true;
        if (bit3 == false) bit3 = true;
        if (bit4 == false) bit4 = true;


        thisCard.IsCardEnd = true;
        
    }
}
