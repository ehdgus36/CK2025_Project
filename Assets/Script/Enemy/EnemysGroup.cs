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
            if (GameManager.instance.Player.isDie == true) break;

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

//CA0011
//CA0012
//CA0021
//CA0022
//CB0031
//CB0032
//CB0041
//CB0042
//CA1011
//CA1012
//CA1021
//CA1022
//CB1031
//CB1032
//CD1041
//CD1042
//CD1051
//CD1052
//CB1061
//CB1062
//CB1071
//CB1072
//CA2021
//CA2022
//CD2031
//CD2032
//CD2041
//CD2042
//CB2051
//CB2052
//CD2061
//CD2062
//CA3011
//CA3012
//CB3021
//CB3022
//CD3031
//CD3032
//CD3041
//CD3042
//CD3051
//CD3052
//CA3061
//CA3062