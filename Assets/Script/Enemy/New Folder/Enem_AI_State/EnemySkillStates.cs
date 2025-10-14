using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


// enemy가 취할수 있는 액션을 정리한 클래스(예 / 플레이어까지 이동 , 공격 , 마무리)
public class EnemyStateAction 
{

    Vector3 StartPos;
    public void MoveEnemy(GameObject moveObject, Vector3 formPos , Vector3 attackOffset)
    {
        StartPos = moveObject.transform.position;
        moveObject.transform.position = GameManager.instance.Player.transform.position + attackOffset;
    }

   

    public async UniTaskVoid AttackEnemy(int damage,int attackCount, Enemy attackEnemy, Unit targetUnit)
    {
        for (int i = 0; i < attackCount; i++)
        {
            attackEnemy.UnitAnimationSystem.PlayAnimation("attack", false, (entry, e) => { GameManager.instance.Player.TakeDamage(attackEnemy, attackEnemy.EnemyData.CurrentDamage); }, null);
            await UniTask.Delay(800);
        }
    }
}




[System.Serializable]
public class EnemySkill_MultiAttack_State : BaseAIState // 여러번 때리기
{
    Vector3 StargPos;
    [SerializeField] int AttackCount;


    [SerializeField] Vector3 AttackOffset;

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }
   
    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Enemy enemy = (Enemy)unit;

        EnemyStateAction enemyAction = new EnemyStateAction();
        
        // 위치 이동
        StargPos = enemy.transform.position;
        enemyAction.MoveEnemy(enemy.gameObject, GameManager.instance.Player.transform.position, AttackOffset);             
        yield return new WaitForSeconds(.1f);

        //애니메이션 재생및 공격

        yield return enemyAction.AttackEnemy(enemy.EnemyData.CurrentDamage, AttackCount, enemy, GameManager.instance.Player).;

        for (int i = 0; i < AttackCount; i++)
        {
            enemy.UnitAnimationSystem.PlayAnimation("attack", false, (entry, e) => { GameManager.instance.Player.TakeDamage(enemy, enemy.EnemyData.CurrentDamage); }, null);
            yield return new WaitForSeconds(.8f);
        }

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
    [SerializeField] int AttackCount;


    [SerializeField] Vector3 AttackOffset;

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Enemy enemy = (Enemy)unit;

        // 위치 이동
        StargPos = enemy.transform.position;
        enemy.transform.position = GameManager.instance.Player.transform.position + AttackOffset;
        yield return new WaitForSeconds(.0f);

        //애니메이션 재생및 공격
        for (int i = 0; i < AttackCount; i++)
        {
            enemy.UnitAnimationSystem.PlayAnimation("attack", false, (entry, e) => { GameManager.instance.Player.TakeDamage(enemy, enemy.EnemyData.CurrentDamage); }, null);
            yield return new WaitForSeconds(.8f);
        }

       

        yield return new WaitForSeconds(.5f);

        //완료 이벤트
        enemy.isAttackEnd = true; // 공격함
        enemy.transform.position = StargPos;

        //체력 회복
        enemy.EnemyData.EnemyUnitData.CurrentHp += enemy.EnemyData.CurrentDamage * AttackCount;
        //체력 회복 이펙트도

        yield return null;

        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }


}


[System.Serializable]
public class EnemySkill_AllEnemyRecoverHP_State : BaseAIState // 전체힐
{
    
    [SerializeField] int RecoverValue;

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Enemy enemy = (Enemy)unit;

        yield return new WaitForSeconds(.3f);
        //전체회복

        //체력 회복 시전자 먼져
        enemy.EnemyData.EnemyUnitData.CurrentHp += RecoverValue;
        //이펙트토


        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            if (GameManager.instance.EnemysGroup.Enemys[i] == enemy) continue;

            GameManager.instance.EnemysGroup.Enemys[i].EnemyData.EnemyUnitData.CurrentHp += RecoverValue;

            //이펙트도
            yield return new WaitForSeconds(.1f);
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

    [SerializeField] int RecoverValue;

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Enemy enemy = (Enemy)unit;
        yield return new WaitForSeconds(.3f);

        // 덱기반 공격기능 만들기
        int dackCount = GameManager.instance.PlayerCDSlotGroup.GetPlayerDack[0].GetDackDatas.Count;

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }


}


