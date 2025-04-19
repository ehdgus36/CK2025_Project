using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{
    // Start is called before the first frame update

    //[SerializeField] int SkillTurnCount = 2;
    //[SerializeField] int CurrentSkillCount = 0;

    [SerializeField] Skill Skill;
    [SerializeField] int MaxDamage;
    [SerializeField] public int CurrentDamage;

    [SerializeField] int MaxDefense;
    [SerializeField] public int CurrentDefense;

    [SerializeField] Animator EnemyAnimator;
    [SerializeField] EnemyStatus EnemyStatus;
    protected UnityAction DieEvent;
    protected bool IsAttack;


    int EnemyIndex = 1; //일단 고정

    //public int GetMaxSkillCount() { return SkillTurnCount; }
    //public int GetCurrentSkillCount() { return CurrentSkillCount; }

    //public void ResetSkillPoint() { CurrentSkillCount = 0; }




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
        EnemyStatus.Initialize(UnitMaxHp, MaxDamage, EnemyIndex);

        StartTurnEvent = () =>
        {


            //if (CurrentSkillCount == SkillTurnCount) // 포인트가 맞으면 스킬 실행
            //{
            //    Skill.StartSkill();
            //    ResetSkillPoint();
            //    return;
            //}

            //CurrentSkillCount++;

            EnemyStatus.UpdateStatus(UnitCurrentHp, CurrentDamage, EnemyIndex);
            GameManager.instance.GetHpManager().UpdatHpbar();
            StartCoroutine("SampleAi");

        };


        EndTurnEvent = () =>
        {

            //턴 종료시 버프로 감소된 변수 원상복구
            CurrentDamage = MaxDamage;
            CurrentDefense = MaxDefense;
            StopCoroutine("SampleAi");
            GameManager.instance.GetHpManager().UpdatHpbar();
        };

        // AttackData.FromUnit = this;
    }

    public void SetDieEvent(UnityAction dieEvent)
    {
        DieEvent += dieEvent;
    }

    IEnumerator SampleAi()
    {

        EnemyAnimator.Play("attack");
        yield return new WaitForSeconds(1.0f);
        // GameManager.instance.GetAttackManager().Attack(this, GameManager.instance.GetPlayer(), AttackData);

        yield return new WaitForSeconds(1.0f);

        yield return null;
    }

    public override void TakeDamage(AttackData data)
    {
        //if (data.Damage < 0)
        //{
        //    Debug.Log("TakeDamge함수에 0보다 작은 수치가 들어옴");
        //    return;
        //}

        base.TakeDamage(data);
        GameManager.instance.GetHpManager().UpdatHpbar();
        EnemyAnimator.Play("hit");
    }
    public override void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            Debug.Log("TakeDamge함수에 0보다 작은 수치가 들어옴");
            return;
        }

        base.TakeDamage(damage);
        GameManager.instance.GetHpManager().UpdatHpbar();
        EnemyAnimator.Play("hit");
    }

    public void TakeDamage(int damage, Buff buff)
    {
        if (damage <= 0)
        {
            Debug.Log("TakeDamge함수에 0보다 작은 수치가 들어옴");
            return;
        }

        if (buff != null)
        {
            CurrentBuff.Add(buff);
        }
        
        Debug.Log(CurrentBuff.Count);
        Debug.Log(CurrentBuff[0].GetBuffType());
        base.TakeDamage(damage - CurrentDefense);
        GameManager.instance.GetHpManager().UpdatHpbar();
        EnemyAnimator.Play("hit");
    }

    protected override void Die()
    {
        this.gameObject.SetActive(false);
        DieEvent?.Invoke();

    }



}