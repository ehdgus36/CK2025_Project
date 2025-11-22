using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///힙합 쥐, 힙팝 최종보스 데미지 (데미지 값 조절 가능하게) + 돈 (돈 개수 조절 가능하게)
/// </summary>
public class EnemySkill_DMG_Gold_State: EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    public int ADD_CoinCount { get; private set; }
    public int Damage { get; private set; }

    public EnemySkill_DMG_Gold_State(int attackCount, int AddCoinCount, int damage = 0) : base(attackCount) { 
    
        ADD_CoinCount = AddCoinCount;
        Damage = damage;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {
        Enemy enemy = (Enemy)unit;


        Buff buff = enemy.EnemyData.EnemyUnitData.buffs.Find(c => c is AttackDamageDownBuff || c is AttackDamageDownBuff_Mute);
        
        

        if (buff != null )
        {
            if (buff.GetBuffDurationTurn() > 0)
            {

                int resultDamage = 0;
                buff.PreviewBuffEffect<int>(Damage, out resultDamage);

                Damage = resultDamage;
            }
        }

       
    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        //사운드
        if (enemy.EnemyData.Enemy_ID == "E12")
        {
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Hip_Hop_Boss/Money_Attack");
            animeCode = "Skill2_Ani";
        }

        isAttackEndControll = false;
        AttackDamage = Damage;
        enemy.isMove = false;


        yield return base.Excut(unit, aIBehavior);

        enemy.isMove = true;

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

public class EnemySkill_Buff_Burnup_State : EnemySkill_MultiAttack_State 
{
    int BurnUP_Turn;

    public EnemySkill_Buff_Burnup_State(int attackCount, int Turn) : base(attackCount)
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
public class EnemySkill_Buff_Burnout_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    int BurnUP_Turn;

    public EnemySkill_Buff_Burnout_State(int attackCount, int Turn) : base(attackCount)
    {
        BurnUP_Turn = Turn;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;


        //사운드
        if (enemy.EnemyData.Enemy_ID == "E12")
        {
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Hip_Hop_Boss/Fire_Attack");
            animeCode = "Skill_Ani";
        }

        isAttackEndControll = false;
        enemy.isMove = false;
        yield return base.Excut(unit, aIBehavior);

        enemy.isMove = true;

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
public class EnemySkill_HP_Volumeup_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    float LossValueHP = .2f;

    int VolumeUP_Value;

    public EnemySkill_HP_Volumeup_State(int attackCount, float LossValue , int volumeup) : base(attackCount)
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


        if (enemy.EnemyData.Enemy_ID == "E09" || enemy.EnemyData.Enemy_ID == "E11")
        {
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Hiphop_Monster/Speaker_Attack");
            animeCode = "Skill_Ani";
        }


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

public class EnemySkill_Heal_Lowest_State : EnemySkill_MultiAttack_State // 때린 데미지 만큼 힐
{
    
    int Damage = 0;

    float HP_Percent = .2f;

    public EnemySkill_Heal_Lowest_State(int attackCount , int damage , float hp) : base(attackCount , damage)
    {
        Damage = damage;
        HP_Percent = hp;
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
        LowEnemy.GetEffectSystem.PlayEffect("RecoverHP_Effect", LowEnemy.transform.position);
        

        if (enemy.EnemyData.Enemy_ID == "E13" || enemy.EnemyData.Enemy_ID == "E16" || enemy.EnemyData.Enemy_ID == "E24")
        {
            animeCode = "Skill_Ani";
            Debug.Log("칵테일 파랑 스킬 코드" + animeCode);
        }

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

    public int PoisonBuffTurn { get; private set; } 

    public EnemySkill_PoisonAttack_State(int attackCount , int Turn) : base(attackCount)
    {
        PoisonBuffTurn = Turn;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {

    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;

        animeCode = "Skill2_Ani";

        if (enemy.EnemyData.Enemy_ID == "E22" || enemy.EnemyData.Enemy_ID == "E25")
        {
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Jazz_Monster/Poison_Attack");
            animeCode = "Skill_Ani";
        }


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
public class EnemySkill_Self_Volumeup_State : EnemySkill_MultiAttack_State 
{
    int VolumeUP_Value;
    int Damage = 0;

    public EnemySkill_Self_Volumeup_State(int attackCount,  int volumeup , int damage) : base(attackCount , damage)
    {
        VolumeUP_Value = volumeup;
        Damage = damage;
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
    int Damage = 0;
    Vector3 startPos;

    public EnemySkill_BarrierAttack_State(int attackCount, int barrier , int customDamage = 0) : base(attackCount , customDamage)
    {
        Barrier_Value = barrier;
        Damage = customDamage;
       
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior)
    {
        base.Enter(unit, aIBehavior);
       
    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;
       

        if (enemy.EnemyData.Enemy_ID == "E29" )
        {
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Jazz_Boss/Drum_Attack");
           
        }

        //이펙트 넣기

        yield return new WaitForSeconds(.2f);

        // 데이미지 주기
        isAttackEndControll = false;
        startPos = enemy.transform.position;

        enemy.transform.position = GameManager.instance.Player.transform.position + enemy.AttackOffset;


        if (enemy.EnemyData.Enemy_ID == "E29")
        {
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Jazz_Boss/Jump_Attack");

        }
        Debug.Log("재즈 보스 데미지 : " + AttackDamage);
        enemy.UnitAnimationSystem.PlayAnimation("Skill_Ani", false, (entry, e) => { GameManager.instance.Player.TakeDamage(enemy, AttackDamage, null); enemy.EnemyData.EnemyUnitData.CurrentBarrier += Barrier_Value / 5; }, 
                                                 (entry) => { enemy.transform.position = startPos; });



        yield return new WaitForSeconds(.1f);

       

        enemy.isAttackEnd = true; // 공격함
        yield return null;
        yield break;
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
}

public class EnemySkill_JAZZBOSS_ALL_VolumeUp_State : EnemySkill_MultiAttack_State 
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

        if (enemy.EnemyData.Enemy_ID == "E29")
        {
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Jazz_Boss/Jump_Attack");
            animeCode = "Skill2_Ani";
        }

        


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


        enemy.isMove = false;
        yield return base.Excut(unit, aIBehavior);
        enemy.isMove = true;

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

    public EnemySkill_BarbedArmor_Barrier_PlayerVolumeUp_State(int attackConut , int barrier ,int barbedArmorTurn , int volumeUP ) : base(attackConut) 
    {
        BarbedArmorTurn = barbedArmorTurn;
        BarrierValue = barrier;
        PlayerVolumeUP = volumeUP;
    }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) {
    
    base.Enter(unit, aIBehavior);
    }

    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Enemy enemy = (Enemy)unit;


        if (enemy.EnemyData.Enemy_ID == "E41")
        {
            //GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Jazz_Boss/Drum_Attack");
            animeCode = "Skill2_Ani";
        }

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