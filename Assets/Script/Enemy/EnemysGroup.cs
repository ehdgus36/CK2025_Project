using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class EnemysGroup : Unit
{

    public List<Enemy> Enemys { get => _Enemys; }
    [SerializeField] private List<Enemy> _Enemys; // 인스펙터에 보이게
    [SerializeField] RhythmSystem RhythmGameSystem;

    public RhythmSystem GetRhythmSystem { get { return RhythmGameSystem; } }

    public void Initialize()
    {
        if (RhythmGameSystem == null)
        {
            RhythmGameSystem = GameObject.Find("RhyremGameSystem").GetComponent<RhythmSystem>();
        }

        for (int i = 0; i < Enemys.Count; i++)
        {
            Enemys[i].Initialize(i, this);
        }

        StartTurnEvent += () =>
        {
            StartCoroutine(AttackSequenceEvent());
        };


        EndTurnEvent += () =>
        {
            StopCoroutine(AttackSequenceEvent());
            for (int i = 0; i < Enemys.Count; i++)
            {
                Enemys[i].EndTurn();
            }

            RhythmGameSystem.ReverseNote(false); //노트 방향 초기
        };
    }

    public void RemoveSelf(Enemy thisEnemy)
    {
        if (thisEnemy == null) return;

        Enemys.Remove(thisEnemy);


        if (Enemys.Count == 0)
        {
            GameManager.instance.GameClearFun();
        }
    }


    IEnumerator AttackSequenceEvent()
    {
        //리듬게임 시작
        RhythmGameSystem?.StartEvent();

        yield return new WaitUntil(() => RhythmGameSystem?.IsEndGame == true);

       
        //리듬게임 종료후 Enemy공격 시작
        for (int i = 0; i < Enemys.Count;)
        {
            Enemy startEnemy = Enemys[i];
            startEnemy.StartTurn();
            yield return new WaitUntil(() => startEnemy.isAttackEnd == true || startEnemy.isDie == true);
            if (startEnemy.isDie == false)i++;
            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(.5f);

        //턴 바꾸면서 마무리
        GameManager.instance.TurnSwap();
        yield return null;
    }

    public void EnemyUIAllUpdata()
    {
        if (_Enemys == null) return;

        for (int i = 0; i < _Enemys.Count; i++)
        {
            _Enemys[i].GetEnemyStatus.UpdateStatus();
        }
    }

    public int DrainSkillPoint()
    {
        int value = 0;
        for (int i = 0; i < Enemys.Count; i++)
        {
            value += _Enemys[i].EnemyData.CurrentSkillPoint;
            _Enemys[i].EnemyData.CurrentSkillPoint = 0;
            _Enemys[i].GetEnemyStatus.UpdateStatus();
        }

        return value;
    }
}
