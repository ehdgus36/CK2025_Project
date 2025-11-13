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

    protected int AttackDamage = 0;

    protected string animeCode = "";


    public EnemySkill_MultiAttack_State(int attackCount)
    {
        _AttackCount = attackCount;
        isAttackEndControll = true;
    }
    public EnemySkill_MultiAttack_State(int attackCount, int customDamage)
    {
        _AttackCount = attackCount;
        isAttackEndControll = true;
        AttackDamage = customDamage;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {
        isAttackEndControll = true;
    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        EnemyStateAction enemyAction = new EnemyStateAction();

        // 위치 이동
        StartPos = enemy.transform.position;

        if (enemy.isMove == true)
            enemyAction.MoveEnemy(enemy.gameObject, GameManager.instance.Player.transform.position, enemy.AttackOffset);


        Debug.Log("몬스터 공격");
        yield return new WaitForSeconds(.1f);

        //애니메이션 재생및 공격
        yield return enemyAction.AttackEnemy(AttackDamage != 0 ? AttackDamage : enemy.EnemyData.CurrentDamage, AttackCount, enemy, GameManager.instance.Player, null,animeCode != "" ? animeCode : "attack");
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

/// <summary>
/// 자신 회복 (최대 체력 비례 회복)
/// </summary>
[System.Serializable]
public class EnemySkill_AttackRecoverHP_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    float HP_Percent = .2f;


    public EnemySkill_AttackRecoverHP_State(int attackCount, float hppercent) : base(attackCount) { HP_Percent = hppercent; }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);
        //체력회복
        //enemy.RecoverHP(enemy.EnemyData.CurrentDamage * AttackCount);
        enemy.RecoverHP(Mathf.CeilToInt((float)enemy.EnemyData.EnemyUnitData.MaxHp * HP_Percent));
        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


[System.Serializable]
public class EnemySkill_AllEnemyRecoverHP_State : EnemySkill_MultiAttack_State // 전체힐
{

    float HP_Percent = .2f;

    public EnemySkill_AllEnemyRecoverHP_State(int attackCount, float hp) : base(attackCount) { HP_Percent = hp; }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    { }

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

            GameManager.instance.EnemysGroup.Enemys[i].RecoverHP(
            Mathf.CeilToInt((float)GameManager.instance.EnemysGroup.Enemys[i].EnemyData.EnemyUnitData.MaxHp * HP_Percent));


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


/// <summary>
/// 혼란
/// </summary>
[System.Serializable]
public class EnemySkill_RhythmReverse_State : BaseAIState // 덱기반 공격
{
    Vector3 StartPos;

    int reversRhythm = 0;
    public int CustomDamage { get; private set; }

    string animeCode = "attack";

    public EnemySkill_RhythmReverse_State()
    {
        reversRhythm = 2;
    }

    public EnemySkill_RhythmReverse_State(int Turn, int Damage = 0)
    {
        reversRhythm = Turn;
        CustomDamage = Damage;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Debug.Log("실행" + this.GetType().ToString());
        Enemy enemy = (Enemy)unit;
        EnemyStateAction enemyAction = new EnemyStateAction();

        if (enemy.EnemyData.Enemy_ID == "E41")
        {
            //GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Jazz_Boss/Drum_Attack");
            animeCode = "Skill_Ani";
        }



        StartPos = enemy.transform.position;

        if(enemy.isMove == true)
            enemyAction.MoveEnemy(enemy.gameObject, GameManager.instance.Player.transform.position, enemy.AttackOffset);
        
        yield return new WaitForSeconds(.3f);

        // 덱기반 공격기능 만들기
        yield return enemyAction.AttackEnemy(CustomDamage != 0 ? CustomDamage : enemy.EnemyData.CurrentDamage, 1,
                                             enemy, GameManager.instance.Player, new RhythmDebuff(BuffType.End, reversRhythm) , animeCode);

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
public class EnemySkill_BarbedArmor_State : EnemySkill_MultiAttack_State // 전체힐 // 덱기반 공격
{
    Vector3 StartPos;


    public EnemySkill_BarbedArmor_State(int attackConut) : base(attackConut) { }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        Buff buff = new BarbedArmorBuff(BuffType.Start, 2);
        buff.StartBuff(enemy);
        enemy.AddBuff(buff);
        yield return new WaitForSeconds(.1f);

        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);


        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }


    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}

/// <summary>
/// 전체 몬스터 가시 (피격시 적에게 3 데미지. 턴수 조절 가능하게) K-POP CD 앨범

/// </summary>
public class EnemySkill_AllBarbedArmor_State : EnemySkill_MultiAttack_State // 전체힐 // 덱기반 공격
{
    Vector3 StartPos;

    int BuffTurn = 2;
    public EnemySkill_AllBarbedArmor_State(int attackConut) : base(attackConut) { }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            Buff buff = new BarbedArmorBuff(BuffType.Start, BuffTurn);
            buff.StartBuff(enemy);
            GameManager.instance.EnemysGroup.Enemys[i].AddBuff(buff);

            //이펙트도
            yield return new WaitForSeconds(.2f);
        }
        yield return new WaitForSeconds(.1f);

        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);


        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }


    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


/// <summary>
/// 플레이어 혼란 (턴수 조절 가능하게) + 셀프 회복 (최대 체력 비례 회복)
/// </summary>
public class EnemySkill_HpRecover_ReversRhythm_State : EnemySkill_RhythmReverse_State // 전체힐 // 덱기반 공격
{
    Vector3 StartPos;
    float HP_Percent = .2f;

    public EnemySkill_HpRecover_ReversRhythm_State(int buffTrun, float hp) : base(buffTrun, 0) { HP_Percent = hp; }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        enemy.RecoverHP(Mathf.CeilToInt((float)enemy.EnemyData.EnemyUnitData.MaxHp * HP_Percent));
        yield return new WaitForSeconds(.2f);

        yield return base.Excut(unit, aIBehavior);
        yield break;
    }


    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}




