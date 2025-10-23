using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class EnemySkill_MultiAttack_State : BaseAIState // 여러번 때리기
{
    Vector3 StargPos;
    [SerializeField, ReadOnly] int _AttackCount;

     public int AttackCount { get { return _AttackCount; } }

    public EnemySkill_MultiAttack_State(int attackCount)
    {
        _AttackCount = attackCount;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }
   
    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Debug.Log("실행" + this.GetType().ToString());
        Enemy enemy = (Enemy)unit;

        EnemyStateAction enemyAction = new EnemyStateAction();
        
        // 위치 이동
        StargPos = enemy.transform.position;
        enemyAction.MoveEnemy(enemy.gameObject, GameManager.instance.Player.transform.position, enemy.AttackOffset);             
        yield return new WaitForSeconds(.1f);

        //애니메이션 재생및 공격
        yield return enemyAction.AttackEnemy(enemy.EnemyData.CurrentDamage, AttackCount, enemy, GameManager.instance.Player).ToCoroutine();
        yield return new WaitForSeconds(.5f);

        //완료 이벤트
        enemy.isAttackEnd = true; // 공격함
        enemy.transform.position = StargPos;
        yield return null;

        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


[System.Serializable]
public class EnemySkill_AttackRecoverHP_State : BaseAIState // 때린 데미지 만큼 힐
{
    Vector3 StargPos;

    [SerializeField, ReadOnly] int _AttackCount;

    public int AttackCount { get { return _AttackCount; } }



    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) {
        _AttackCount = 1;
    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Debug.Log("실행" + this.GetType().ToString());
        Enemy enemy = (Enemy)unit;
        EnemyStateAction enemyAction = new EnemyStateAction();
        // 위치 이동
        StargPos = enemy.transform.position;
        enemyAction.MoveEnemy(enemy.gameObject, GameManager.instance.Player.transform.position, enemy.AttackOffset);
       
        
        yield return new WaitForSeconds(.1f);
        //애니메이션 재생및 공격
        yield return enemyAction.AttackEnemy(enemy.EnemyData.CurrentDamage, AttackCount, enemy, GameManager.instance.Player).ToCoroutine();
        yield return new WaitForSeconds(.5f);
        //완료 이벤트
        enemy.isAttackEnd = true; // 공격함
        enemy.transform.position = StargPos;

        //체력회복
        enemy.RecoverHP(enemy.EnemyData.CurrentDamage * AttackCount);
        //이펙트 추가
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


[System.Serializable]
public class EnemySkill_AllEnemyRecoverHP_State : BaseAIState // 전체힐
{

    [SerializeField, ReadOnly] int _RecoverValue;

    public int RecoverValue { get { return _RecoverValue; } }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {
        _RecoverValue = 5;
    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Debug.Log("실행" + this.GetType().ToString());
        Enemy enemy = (Enemy)unit;

        yield return new WaitForSeconds(.3f);
        //전체회복

        //체력 회복 시전자 먼져
        enemy.RecoverHP(RecoverValue);
        //이펙트토  RecoverHP_Effect

       
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            if (GameManager.instance.EnemysGroup.Enemys[i] == enemy) continue;

            GameManager.instance.EnemysGroup.Enemys[i].RecoverHP(_RecoverValue);
           
            //이펙트도
            yield return new WaitForSeconds(.2f);
        }

        

        enemy.isAttackEnd = true; // 공격함

        yield return null;

        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}

[System.Serializable]
public class EnemySkill_DackAttack_State : BaseAIState // 덱기반 공격
{

    [SerializeField, ReadOnly] int _dackCount;

     public int dackCount { get { return _dackCount; } }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Debug.Log("실행" + this.GetType().ToString());
        Enemy enemy = (Enemy)unit;
        yield return new WaitForSeconds(.3f);

        // 덱기반 공격기능 만들기
        _dackCount = GameManager.instance.PlayerCDSlotGroup.GetPlayerDack[0].GetDackDatas.Count;
        EnemyStateAction enemyAction = new EnemyStateAction();
        yield return enemyAction.AttackEnemy(dackCount, 1, enemy, GameManager.instance.Player).ToCoroutine();

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }


}


