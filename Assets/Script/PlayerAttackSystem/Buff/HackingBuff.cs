using UnityEngine;

public class HackingBuff : Buff
{
    public override Buff DeepCopy()
    {
        throw new System.NotImplementedException();
    }

    public override void StartBuff(Unit unit)
    {
        if (CurrentBuffTurn == BuffDurationTurn) return;

        if (unit.GetComponent<Enemy>())
        {
            unit.GetComponent<Enemy>().ResetSkillPoint();
        }

        CurrentBuffTurn++;
    }
}
