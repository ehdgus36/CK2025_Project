using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;


[System.Flags]
public enum BuffLayer
{
    None = 0,                  
    Fire_1 = 1 << 0,            
    Eletric_1 = 1 << 1,           
    Captivate_1 = 1 << 2,      
    Curse_1 = 1 << 3,
    Everything = ~0            
}

public class Enemy : Unit , IPointerDownHandler
{

    [SerializeField] string enemyName;
    [SerializeField] Skill Skill;
    [SerializeField] int MaxDamage;
    [SerializeField] public int CurrentDamage;

    [SerializeField] int MaxDefense;
    [SerializeField] public int CurrentDefense;

    [SerializeField] Animator EnemyAnimator;
    [SerializeField] EnemyStatus EnemyStatus;
    protected UnityAction DieEvent;
    protected bool IsAttack;

    [SerializeField] BuffLayer BuffLayer;

    int EnemyIndex = 1; //일단 고정

    int startLayer = 0;
    bool isDescription = false;



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
        EnemyStatus.Initialize(UnitMaxHp, MaxDamage, EnemyIndex, enemyName);

        UnitCurrentHp = UnitMaxHp; 

        CurrentBuff.Add(new FireBuff(0, 0, 1));
        CurrentBuff.Add(new ElecBuff(0, 0, 1));
        CurrentBuff.Add(new CaptivBuff(0, 0, 1));
        CurrentBuff.Add(new CurseBuff(0, 0, 1));

        StartTurnEvent = () =>
        {


            EnemyStatus.UpdateBuffIcon(CurrentBuff);
            GameManager.instance.GetHpManager().UpdatHpbar();
            StartCoroutine("SampleAi");

        };


        EndTurnEvent = () =>
        {
            EnemyStatus.UpdateStatus(UnitCurrentHp, CurrentDamage, EnemyIndex);
            EnemyStatus.UpdateBuffIcon(CurrentBuff);
            //턴 종료시 버프로 감소된 변수 원상복구
            CurrentDamage = MaxDamage;
            CurrentDefense = MaxDefense;
            StopCoroutine("SampleAi");
            GameManager.instance.GetHpManager().UpdatHpbar();
        };

      
    }

    public void SetDieEvent(UnityAction dieEvent)
    {
        DieEvent += dieEvent;
    }

    IEnumerator SampleAi()
    {

        EnemyAnimator.Play("attack");
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.GetPlayer().TakeDamage(CurrentDamage);

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
            for (int i = 0; i < CurrentBuff.Count; i++)
            {
                if (CurrentBuff[i].GetType() == buff.GetType())
                {
                    CurrentBuff[i].AddBuffTurnCount(buff.GetBuffDurationTurn());
                    break;
                }
            }
          
        }

       
        Debug.Log(CurrentBuff.Count);
        Debug.Log(CurrentBuff[0].GetBuffType());
        base.TakeDamage(damage - CurrentDefense);
        GameManager.instance.GetHpManager().UpdatHpbar();
        EnemyAnimator.Play("hit");
        EnemyStatus.UpdateStatus(UnitCurrentHp, CurrentDamage, EnemyIndex);
        EnemyStatus.UpdateBuffIcon(CurrentBuff);
       
    }

    protected override void Die()
    {
        this.gameObject.SetActive(false);
        DieEvent?.Invoke();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDescription == false)
        {

            EnemyStatus.OnPassiveDescription();
            startLayer = this.gameObject.layer;

            ChangeLayerRecursively(this.gameObject, 7);
            isDescription = true;
            return;
        }

        if (isDescription == true)
        {

            ChangeLayerRecursively(this.gameObject, startLayer);
            isDescription = false;
            return;
        }
    }

    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
    }
}