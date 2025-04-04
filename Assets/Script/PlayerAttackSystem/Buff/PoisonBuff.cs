using UnityEngine;

public class PoisonBuff : Buff
{
    [SerializeField] int Damage;
    int _Damage;
    public override Buff DeepCopy()
    {
        _Damage = Damage;
        PoisonBuff buff = new PoisonBuff();
        buff.Damage = this._Damage;
        buff.BuffDurationTurn = this._BuffDurationTurn;
        buff.CurrentBuffTurn = this.CurrentBuffTurn;

        return buff;
    }

    public override void StartBuff(Unit unit)
    {
        if (CurrentBuffTurn == BuffDurationTurn) return;

        unit.TakeDamage(Damage);
        CurrentBuffTurn++;
    }
}
