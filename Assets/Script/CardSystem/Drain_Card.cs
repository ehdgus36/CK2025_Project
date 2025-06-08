using UnityEngine;

public class Drain_Card : Card
{
    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
        int Recover_hp = nextCard.cardData.Damage - Target.EnemyData.CurrentDefense;
        nextCard.Buff_Recover_HP = Recover_hp;
        base.TargetExcute(Target, nextCard);
    }
}
