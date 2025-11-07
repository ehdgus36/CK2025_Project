using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///힙합 쥐, 힙팝 최종보스 데미지 (데미지 값 조절 가능하게) + 돈 (돈 개수 조절 가능하게)
/// </summary>
public class EnemySkill_AttackAndCoin_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    int ADD_CoinCount;
    int Damage = 5;

    public EnemySkill_AttackAndCoin_State(int attackCount , int AddCoinCount) : base(attackCount) { 
    ADD_CoinCount = AddCoinCount;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        isAttackEndControll = false;
        AttackDamage = Damage;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);
        // 코인 획득
        int coin = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coin);
        coin = coin + ADD_CoinCount;
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, coin);

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


/// <summary>
/// 플레이어 번업 (턴수 조절 가능하게)
/// </summary>

public class EnemySkill_BurnUp_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    int BurnUP_Turn;

    public EnemySkill_BurnUp_State(int attackCount, int Turn) : base(attackCount)
    {
        BurnUP_Turn = Turn;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);
        // 코인 획득
        GameManager.instance.Player.AddBuff(new FireBuff(BuffType.Start, BurnUP_Turn, 4));

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}



/// <summary>
/// 플레이어 번아웃 (턴수 조절 가능하게) feat. 임페리얼 블레스터
/// </summary>
public class EnemySkill_BurnOut_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    int BurnUP_Turn;

    public EnemySkill_BurnOut_State(int attackCount, int Turn) : base(attackCount)
    {
        BurnUP_Turn = Turn;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);
        // 코인 획득
        GameManager.instance.Player.AddBuff(new FireBuffBrunOut(BuffType.Start, BurnUP_Turn, 12));

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


/// <summary>
/// 체력 소모 (남은 체력 비례) + 셀프 볼륨업 (중첩수 조절 가능하게)

/// </summary>
public class EnemySkill_VolumUPLossHP_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    float LossValueHP = .2f;

    int VolumeUP_Value;

    public EnemySkill_VolumUPLossHP_State(int attackCount, float LossValue , int volumeup) : base(attackCount)
    {
        LossValueHP = LossValue;
        VolumeUP_Value = volumeup;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        // 데미지 상승, 체력감소
        enemy.EnemyData.CurrentDamage += VolumeUP_Value;
        enemy.AddBuff(new VolumeUPBuff(BuffType.End, VolumeUP_Value));

        //체력 줄이기
        enemy.LossHP(Mathf.FloorToInt((float)enemy.EnemyData.EnemyUnitData.CurrentHp * LossValueHP));

        yield return new WaitForSeconds(.2f);

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
/// 가장 낮은 체력 회복 (최대 체력 비례 회복. 만약 가장낮은 체력이 두명이면 배열 순으로) + 데미지 (데미지 값 조절 가능하게);
/// </summary>

public class EnemySkill_LowEnemysRecoverHP_Attack_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    
    int Damage = 5;

    float HP_Percent;

    public EnemySkill_LowEnemysRecoverHP_Attack_State(int attackCount) : base(attackCount)
    {
        
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        //가장 낮은 몬스터 체력상승
        Enemy LowEnemy = GameManager.instance.EnemysGroup.Enemys[0];

        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            if (GameManager.instance.EnemysGroup.Enemys[i].EnemyData.EnemyUnitData.CurrentHp <= LowEnemy.EnemyData.EnemyUnitData.CurrentHp)
            {
                LowEnemy = GameManager.instance.EnemysGroup.Enemys[i];
            }
        }

        LowEnemy.EnemyData.EnemyUnitData.CurrentHp += Mathf.CeilToInt((float)enemy.EnemyData.EnemyUnitData.MaxHp * HP_Percent);
        //이펙트
        

        //데미지 주기
        yield return new WaitForSeconds(.2f);

        isAttackEndControll = false;
        AttackDamage = Damage;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


/// <summary>
/// 플레이어 중독 (턴수 만큼 데미지주고 턴 -1. 턴수 조절 가능하게)
/// </summary>
public class EnemySkill_PoisonAttack_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{

    int PoisonBuffTurn = 4;

    public EnemySkill_PoisonAttack_State(int attackCount) : base(attackCount)
    {

    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

       
        //데미지 주기
        yield return new WaitForSeconds(.2f);

        isAttackEndControll = false;
        yield return base.Excut(unit, aIBehavior);

        GameManager.instance.Player.AddBuff(new PoisonBuff(BuffType.Start, PoisonBuffTurn , 1));


        yield return new WaitForSeconds(.1f);

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}



/// <summary>
/// 셀프 볼륨업 (중첩수 조절 가능하게) + 데미지 (데미지 값 조절 가능하게)
/// </summary>
public class EnemySkill_VolumUPAttack_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    int VolumeUP_Value;
    int Damage = 5;

    public EnemySkill_VolumUPAttack_State(int attackCount,  int volumeup) : base(attackCount)
    {
        VolumeUP_Value = volumeup;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        // 데미지 상승,
        enemy.EnemyData.CurrentDamage += VolumeUP_Value;
        enemy.AddBuff(new VolumeUPBuff(BuffType.End, VolumeUP_Value));

 

        yield return new WaitForSeconds(.2f);

        // 데이미지 주기
        isAttackEndControll = false;
        AttackDamage = Damage;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}

/// <summary>
/// 베리어 (베리어 값 조절 가능하게) + 데미지 5번 (데미지 값 조절 가능하게)
/// 생성자에서 5로 값 잘 넣어주기
/// </summary>
public class EnemySkill_BarrierAttack_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    int Barrier_Value;
    int Damage = 5;

    public EnemySkill_BarrierAttack_State(int attackCount, int barrier , int customDamage = 0) : base(attackCount)
    {
        Barrier_Value = barrier;
        Damage = customDamage;
        
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;
        enemy.EnemyData.EnemyUnitData.CurrentBarrier += Barrier_Value;
        //이펙트 넣기

        yield return new WaitForSeconds(.2f);

        // 데이미지 주기
        isAttackEndControll = false;
        AttackDamage = Damage;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}

public class EnemySkill_JAZZBOSS_ALL_VolumeUp_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    
    int Damage = 5;
    int VolumeUP_Value;

    public EnemySkill_JAZZBOSS_ALL_VolumeUp_State(int attackCount, int volume, int customDamage = 0) : base(attackCount)
    {
        VolumeUP_Value = volume;
        Damage = customDamage;

    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        //볼륨업
        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            GameManager.instance.EnemysGroup.Enemys[i].EnemyData.CurrentDamage += VolumeUP_Value;
            GameManager.instance.EnemysGroup.Enemys[i].AddBuff(new VolumeUPBuff(BuffType.End, VolumeUP_Value));
        }

        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.AddValueDamage(VolumeUP_Value, new List<Card>());

        GameManager.instance.Player.AddBuff(new VolumeUPBuff(BuffType.End, VolumeUP_Value));

       
        //이펙트 넣기

        yield return new WaitForSeconds(.2f);

        // 데이미지 주기
        isAttackEndControll = false;
        AttackDamage = Damage;
        yield return base.Excut(unit, aIBehavior);

        yield return new WaitForSeconds(.1f);

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}


/// <summary>
/// 셀프 가시 (피격시 적에게 3 데미지. 턴수 조절 가능하게) + 셀프 베리어 (베리어 값 조절 가능하게) + 플레이어 볼륨업 (중첩수 조절 가능하게)
/// </summary>
[System.Serializable]
public class EnemySkill_BarbedArmor_Barrier_PlayerVolumeUp_State : EnemySkill_MultiAttack_State // 전체힐 // 덱기반 공격
{
    Vector3 StartPos;

    int BarbedArmorTurn = 2;
    int BarrierValue = 5;
    int PlayerVolumeUP = 5;

    public EnemySkill_BarbedArmor_Barrier_PlayerVolumeUp_State(int attackConut) : base(attackConut) { }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        //가시
        Buff buff = new BarbedArmorBuff(BuffType.Start, BarbedArmorTurn);
        buff.StartBuff(enemy);
        enemy.AddBuff(buff);

        //베리어
        enemy.EnemyData.EnemyUnitData.CurrentBarrier += BarrierValue;
        // 배리어 이펙트 추가

        //플레이어 볼륨업
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.AddValueDamage(PlayerVolumeUP, new List<Card>());
        GameManager.instance.Player.AddBuff(new VolumeUPBuff(BuffType.End, PlayerVolumeUP));
        //이펙트


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