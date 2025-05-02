using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void DieEnemy(Enemy enemy);

public class EnemysGroup :Unit
{

    public List<Enemy> Enemys { get => _Enemys; }
    [SerializeField] private List<Enemy> _Enemys; // 인스펙터에 보이게

    
    public void Initialize()
    {
        for (int i = 0; i < Enemys.Count; i++)
        {
            Enemys[i].Initialize(i);
        }

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

    void EnemysDieEvent(Enemy thisEnemy)
    {
        Enemys.Remove(thisEnemy);

        if (Enemys.Count == 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        GameManager.instance.GameClearFun();
    }


    // 이것도 나중에 시퀀스 다시
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
