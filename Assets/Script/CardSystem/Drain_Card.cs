using UnityEngine;

public class Drain_Card : Card
{
    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
        int Recover_hp = 0;
        if (nextCard != null)
        {
            Recover_hp = nextCard.cardData.Attack_DMG;
            nextCard.Buff_Recover_HP = Recover_hp;
        }

        
        base.TargetExcute(Target, nextCard);
    }
}
