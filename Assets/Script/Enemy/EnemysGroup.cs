using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public delegate void DieEnemy(Enemy enemy);

public class EnemysGroup :Unit
{

    public List<Enemy> Enemys { get => _Enemys; }
    [SerializeField] private List<Enemy> _Enemys; // 인스펙터에 보이게
    [SerializeField] RhythmGameSystem RhythmGameSystem;
    
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

        StartTurnEvent += () => { StartCoroutine(AttackSequenceEvent()); };

        EndTurnEvent += () =>
        {
            StopCoroutine(AttackSequenceEvent());
            for (int i = 0; i < Enemys.Count; i++)
            {
                Enemys[i].EndTurn();
            }
        };
    }

    void EnemysDieEvent(Enemy thisEnemy)
    {
        //사망한 Enemy 삭제
        RhythmGameSystem?.RhythmGameTracksRemove(Enemys.IndexOf(thisEnemy));

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
    IEnumerator AttackSequenceEvent()
    {
        RhythmGameSystem?.StartGame();

        yield return new WaitUntil(() => RhythmGameSystem?.isEndGame == true);

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < Enemys.Count; i++)
        {
            Enemys[i].StartTurn();
            yield return new WaitUntil(() => Enemys[i].isAttackEnd == true);
            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(.5f);
        GameManager.instance.TurnSwap();
        yield return null;
    }
}
