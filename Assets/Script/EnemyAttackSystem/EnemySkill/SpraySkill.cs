using UnityEngine;

public class SpraySkill : Skill
{
    [SerializeField] int Damage = 1;

    public override void StartSkill()
    {
        AttackData attackData = new AttackData();
        attackData.Damage = Damage;
        attackData.FromUnit = this.GetComponent<Enemy>();


        GameManager.instance.GetPlayer().TakeDamage(attackData);
    }
}
