using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGroup : Enemy
{

    [SerializeField] List<Enemy> Enemys;


   
    public Enemy GetEnemy() { return Enemys[0]; }

    protected override void Initialize()
    {
        for (int i = 0; i < Enemys.Count; i++)
        {
            Enemys[i].SetDieEvent(EnemysDieEvent);
        }

        StartTurnEvent += () => { StartCoroutine("AttackEvent"); };

        EndTurnEvent += () =>
        {
            StopCoroutine("AttackEvent");
            for (int i = 0; i < Enemys.Count; i++)
            {
                Enemys[i].EndTurn();
            }
        };
    }

    public override void TakeDamage(int damage)
    {
        if (Enemys.Count == 0)
        {
            return;
        }

        Enemys[0].TakeDamage(damage);
    }

    public void TakeAllDamage(int damage )
    {
        if (Enemys.Count == 0)
        {
            return;
        }
        for (int i = 0; i < Enemys.Count; i++)
        {
            Enemys[i].TakeDamage(damage);
        }
    }

    protected override void Die()
    {
        GameManager.instance.NextWave();
        Destroy(this.gameObject);
    }

    private void Update()
    {
        EnemysDieEvent();
    }

    void EnemysDieEvent()
    {
        for (int i = 0; i < Enemys.Count; i++)
        {
            if (Enemys[i] == null || Enemys[i].gameObject.activeSelf == false)
            {
                Enemys.RemoveAt(0);
            }
        }

        if (Enemys.Count == 0)
        {
            Die();
        }
    }

    IEnumerator AttackEvent()
    {
       
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < Enemys.Count; i++)
        {
           
            Enemys[i].StartTurn();
           
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(2f);
        GameManager.instance.TurnSwap();
        yield return null;
    }
}
