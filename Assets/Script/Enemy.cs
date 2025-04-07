using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{
    // Start is called before the first frame update

    [SerializeField] int SkillTurnCount = 2;
    [SerializeField] int CurrentSkillCount = 0;
    
    [SerializeField] Skill Skill;
    [SerializeField] AttackData AttackData;
    [SerializeField] Animator EnemyAnimator;
    protected UnityAction DieEvent;
    protected bool IsAttack;

    public int GetMaxSkillCount() { return SkillTurnCount; }
    public int GetCurrentSkillCount() { return CurrentSkillCount; }

    public void SetIsAttack(bool b)
    {
        IsAttack = b;
    }
    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        StartTurnEvent = () => {

            if (CurrentSkillCount == SkillTurnCount) // 포인트가 맞으면 스킬 실행
            {
                Skill.StartSkill();
            }

            CurrentSkillCount++;
            
            GameManager.instance.GetHpManager().UpdatHpbar();
            StartCoroutine("SampleAi");
        
        };


        EndTurnEvent += () => { 
            StopCoroutine("SampleAi");
            GameManager.instance.GetHpManager().UpdatHpbar();
        };

        AttackData.FromUnit = this;
    }

    public void SetDieEvent(UnityAction dieEvent)
    {
        DieEvent += dieEvent;
    }

    IEnumerator SampleAi()
    {
      
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.GetAttackManager().Attack(this, GameManager.instance.GetPlayer(), AttackData);

        yield return new WaitForSeconds(1.0f);
        
        yield return null;
    }

    public override void TakeDamage(AttackData data)
    {
        base.TakeDamage(data);
        GameManager.instance.GetHpManager().UpdatHpbar();
        EnemyAnimator.Play("hit");
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GameManager.instance.GetHpManager().UpdatHpbar();
        EnemyAnimator.Play("hit");
    }

    protected override void Die()
    {
        this.gameObject.SetActive(false);
        DieEvent?.Invoke();
        
    }

  

}