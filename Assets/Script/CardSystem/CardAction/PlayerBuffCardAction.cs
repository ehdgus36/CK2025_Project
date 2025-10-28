using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBuffCardAction 
{
    
}

public class DistortionAction : PlayerBaseCardAction
{
    public DistortionAction(Card card) : base(card)
    {
    }

    //"괴상한 음악을 연주해 적 하나에게<color=#BF4D13>취약 2턴</color>을 부여한다."} 버즈 : 적의 공격력이 20% 약화됩니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);
        yield return new WaitUntil(() => bit2 == true);
        //음파 날아가기



        yield return new WaitUntil(()=> bit3 == true);

        float T = 0;
        GameObject ball = new GameObject();

        for (int i = 0; i < 20; i++)
        {
            ball.transform.position = Vector3.Lerp(GameManager.instance.Player.transform.position, Target.transform.position , T);
            T += 0.05f;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitUntil(() => bit4 == true);
        Target.AddBuff(cardData.CardBuff);
    }
}


//플레이 스트로크
public class FireStrokeAction : PlayerBaseCardAction
{
    public FireStrokeAction(Card card) : base(card)
    {
    }

    //일반_단일 번업_1레벨
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        yield return new WaitUntil(() => bit3 == true);
        Target.GetEffectSystem.PlayEffect("Big_Fire_Effect", Target.transform.position);
        Target.AddBuff(cardData.CardBuff);
        yield return null;
    }
}

public class CursedShieldAction : GetBarrierAction
{
    CardData data;
    Player Player;

    public CursedShieldAction(Card card) : base(card)
    {
    }

    //기타로 불길한 보호막을 만들어 리듬 게임 성공 시 베리어 1을 추가로 획득하고 적 전체에게 버즈 1턴을 적용합니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        Player = player;
        data = cardData;


        yield return new WaitUntil(() => bit1 == true);
        yield return new WaitForSeconds(.03f);
        

        // 적에게 디버프 주는 기능만들기
        List<Enemy> enemies = GameManager.instance.EnemysGroup.Enemys;



        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].AddBuff(cardData.CardBuff);
           
            //이펙트 발생 시키기
        }
        yield return null;
    }

    void GetBarrier()
    {
        GetBarrier(Player, data, false);
        Player.PlayerEffectSystem.PlayEffect("CursedShield_Effect", Player.transform.position);
    }
}

public class BurningStageAction : PlayerBaseCardAction
{
    public BurningStageAction(Card card) : base(card)
    {
    }

    //희귀_전체 번업 + 체력 회복_1레벨
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);
        yield return new WaitUntil(() => bit2 == true);
        player.PlayerEffectSystem.PlayEffect("Small_Fire_Effect", player.transform.position);
        player.PlayerEffectSystem.PlayEffect("Tuning_Effect", player.transform.position);
        //체력회복
        player.addHP(cardData.HP_Recover);

        yield return new WaitUntil(() => bit3 == true);

        // 번업주기
        List<Enemy> enemies = GameManager.instance.EnemysGroup.Enemys;

        for (int i = 0; i < enemies.Count; i++)
        { 
            enemies[i].GetEffectSystem.PlayEffect("Small_Fire_Effect", Target.transform.position);
            enemies[i].AddBuff(cardData.CardBuff);
        }
        
        yield return null;
    }
}

public class EncoreAction : PlayerBaseCardAction
{
    public EncoreAction(Card card) : base(card)
    {
    }

    //희귀_단일 몬스터 스킬 게이지 감소 + 감소만큼 랜덤 번업 2씩_1레벨 감소한 만큼 
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);
        yield return null;
    }
}

public class STFUAction : SingleAttackAction
{
    public STFUAction(Card card) : base(card)
    {
    }

    //전설_단일 뮤트 1턴 + 단일 데미지_1레벨	뮤트 : 적의 공격력이 100% 약화됩니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);
        yield return new WaitUntil(() => bit2 == true);
        //음파 날아가기


        yield return new WaitUntil(() => bit3 == true);
        yield return SingleAttack(cardData, Target, int.Parse( cardData.Attack_Count));
        Target.AddBuff(cardData.CardBuff);
    }
}

public class HellfireAction : PlayerBaseCardAction
{


    public static bool _isHellFire = false;
    public static bool isHellFire { get => _isHellFire;}

    public HellfireAction(Card card) : base(card)
    {
    }

    //전설_이번 턴동안 번업 적용시 번아웃으로 적용 + 전체 번아웃 1_1레벨	번업 : 적의 적용 턴 동안 2데미지를 받습니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        _isHellFire = true;
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);


        yield return new WaitUntil(() => bit2 == true);
        player.PlayerEffectSystem.PlayEffect("Fire_Strum_Effect", player.transform.position);


        yield return new WaitUntil(() => bit3 == true);
        List<Enemy> enemies = GameManager.instance.EnemysGroup.Enemys;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetEffectSystem.PlayEffect("Big_Fire_Effect", Target.transform.position);
            enemies[i].GetEffectSystem.PlayEffect("Small_Fire_Effect", Target.transform.position);
            enemies[i].AddBuff(cardData.CardBuff);
        }
        
    }

    public static void EndHellFire()
    {
        _isHellFire = false;
    }
}

public class BlessingofRockAction : PlayerBaseCardAction
{
    CardData datas;
    Player players;
    Card Card;
    public BlessingofRockAction(Card card) : base(card)
    {
    }

    //전설_플레이어가 혼란을 받음 + 성공 노트만큼 볼륨업 4씩 획득_1레벨	혼란 : 플레이어의 적용 턴 동안 리듬 박자가 반대로 나옵니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);
        players = player;
        datas = cardData;
        Card = card;
        player.PlayerEffectSystem.PlayEffect("BlessingofRock_Effect", player.transform.position);
        yield return new WaitUntil(() => bit2 == true);
        //이펙트


        yield return new WaitUntil(() => bit3 == true);
        //이펙트
        //볼륨어
        players.AddBuff(cardData.CardBuff);
        GameManager.instance.EnemysGroup.GetRhythmSystem.GetRhythmInput.SuccessNoteEvent += VolumeUpEvent;
    }

    void VolumeUpEvent(GameObject obj)
    {
        players.AddBuff(new VolumeUPBuff(BuffType.End, 1));
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.AddValueDamage(datas.Buff_VolumeUp, Card.GetCardSloat.ReadData<Card>());
    }
}
