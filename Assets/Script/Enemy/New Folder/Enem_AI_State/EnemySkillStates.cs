using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemySkill_MultiAttack_State : BaseAIState // 여러번 때리기
{
     Vector3 StartPos;
    [SerializeField] int _AttackCount;

    public int AttackCount { get { return _AttackCount; } }



    protected bool isAttackEndControll = true;
   
   

    public EnemySkill_MultiAttack_State(int attackCount)
    {
        _AttackCount = attackCount;
        isAttackEndControll = true;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) {
        isAttackEndControll = true;
    }
   
    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
       
        Enemy enemy = (Enemy)unit;

        EnemyStateAction enemyAction = new EnemyStateAction();
        
        // 위치 이동
        StartPos = enemy.transform.position;
        enemyAction.MoveEnemy(enemy.gameObject, GameManager.instance.Player.transform.position, enemy.AttackOffset);             
        yield return new WaitForSeconds(.1f);

        //애니메이션 재생및 공격
        yield return enemyAction.AttackEnemy(enemy.EnemyData.CurrentDamage, AttackCount, enemy, GameManager.instance.Player);
        yield return new WaitForSeconds(.5f);

        
        //완료 이벤트
        enemy.isAttackEnd = isAttackEndControll;
       
        // 공격함
        enemyAction.MoveEnemy(enemy.gameObject, StartPos, Vector3.zero); 
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) 
    {
        
    }
}


[System.Serializable]
public class EnemySkill_AttackRecoverHP_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{

    public EnemySkill_AttackRecoverHP_State(int attackCount) : base(attackCount) { }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) {
        
    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
       
        Enemy enemy = (Enemy)unit;

        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);
        //체력회복
        enemy.RecoverHP(enemy.EnemyData.CurrentDamage * AttackCount);
       
        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


[System.Serializable]
public class EnemySkill_AllEnemyRecoverHP_State : EnemySkill_MultiAttack_State // 전체힐
{
    public EnemySkill_AllEnemyRecoverHP_State(int attackCount) : base(attackCount) { }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {}

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        
        Enemy enemy = (Enemy)unit;
       
        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);
        //체력 회복 시전자 먼져
        enemy.RecoverHP(enemy.EnemyData.CurrentDamage);
        
     
        yield return new WaitForSeconds(.4f);
        
        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            if (GameManager.instance.EnemysGroup.Enemys[i] == enemy) continue;

            GameManager.instance.EnemysGroup.Enemys[i].RecoverHP(enemy.EnemyData.CurrentDamage);
           
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
    Vector3 StartPos;
    [SerializeField] int _dackCount;

     public int dackCount { get { _dackCount = GameManager.instance.PlayerCDSlotGroup.GetPlayerDack[0].GetDackDatas.Count; return _dackCount; } }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Debug.Log("실행" + this.GetType().ToString());
        Enemy enemy = (Enemy)unit;
        EnemyStateAction enemyAction = new EnemyStateAction();

        StartPos = enemy.transform.position;
        enemyAction.MoveEnemy(enemy.gameObject, GameManager.instance.Player.transform.position, enemy.AttackOffset);
        yield return new WaitForSeconds(.3f);

        // 덱기반 공격기능 만들기
        yield return enemyAction.AttackEnemy(dackCount, 1, enemy, GameManager.instance.Player);
        yield return new WaitForSeconds(.5f);

        enemyAction.MoveEnemy(enemy.gameObject, StartPos, Vector3.zero);
        yield return new WaitForSeconds(.1f);
        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}

[System.Serializable]
public class EnemySkill_RhythmReverse_State : BaseAIState // 덱기반 공격
{
    Vector3 StartPos;
    
    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Debug.Log("실행" + this.GetType().ToString());
        Enemy enemy = (Enemy)unit;
        EnemyStateAction enemyAction = new EnemyStateAction();

        StartPos = enemy.transform.position;
        enemyAction.MoveEnemy(enemy.gameObject, GameManager.instance.Player.transform.position, enemy.AttackOffset);
        yield return new WaitForSeconds(.3f);

        // 덱기반 공격기능 만들기
        yield return enemyAction.AttackEnemy(enemy.EnemyData.CurrentDamage, 1, enemy, GameManager.instance.Player,new RhythmDebuff(BuffType.End,2));
        yield return new WaitForSeconds(.5f);

        enemyAction.MoveEnemy(enemy.gameObject, StartPos, Vector3.zero);
        yield return new WaitForSeconds(.1f);
        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}

//Barbed Armor

[System.Serializable]
public class EnemySkill_BarbedArmor_State: EnemySkill_MultiAttack_State // 전체힐 // 덱기반 공격
{
    Vector3 StartPos;


    public EnemySkill_BarbedArmor_State(int attackConut) : base(attackConut) { }
 
    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);
        Buff buff = new BarbedArmorBuff(BuffType.Start, 2);
        buff.StartBuff(enemy);
        enemy.AddBuff(buff);

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }


    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}
