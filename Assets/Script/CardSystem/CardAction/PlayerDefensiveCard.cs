using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerDefensiveCardAction
{
    
}


//튜닝
public class RecoverAction : PlayerBaseCardAction
{
    public RecoverAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 타이밍
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        yield return new WaitUntil(() => bit3 == true);

        RecoverHP(player, cardData);

        yield break;
    }

    protected void RecoverHP(Player player, CardData cardData)
    {
        player.PlayerEffectSystem.PlayEffect("Tuning_Effect", player.transform.position);
        player.addHP(cardData.HP_Recover);
    }
}

//기타 쉴드
public class GetBarrierAction : PlayerBaseCardAction
{
    public GetBarrierAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        yield return new WaitUntil(() => bit1 == true);
        yield return new WaitForSeconds(.03f);
        GetBarrier(player, cardData);
        yield break;
    }

    protected void GetBarrier(Player player, CardData cardData, bool iseffect = true)
    {
        if(iseffect == true)
            player.PlayerEffectSystem.PlayEffect("GuitarShield_Effect", player.transform.position);
        
        player.AddBarrier(cardData.Barrier_Get);
    }
}

public class VolumeShieldAction : GetBarrierAction
{
    public VolumeShieldAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        yield return new WaitUntil(() => bit1 == true);
        yield return new WaitForSeconds(.03f);
        //볼륨업 구현
        //볼륨업은 데이터 테이블 수치조작하고 덱 묘지 플레이어 손에 있는카드 전부 초기화
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.AddValueDamage(cardData.Buff_VolumeUp);
        player.AddBuff(new VolumeUPBuff(BuffType.End, 1));
        //볼륨업 이펙트
        GetBarrier(player, cardData);
      
        yield break;
    }


}

//나머지 이름 작업하면 ㄱㄱ그

public class SoftEchoAction : PlayerBaseCardAction
{
    Player Player;
    CardData data;
    public SoftEchoAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);
        Player = player;
        data = cardData;
        yield return new WaitUntil(() => bit3 == true);
        //이펙트

        player.PlayerEffectSystem.PlayEffect("SoftEcho_Effect", player.transform.position);
        GameManager.instance.EnemysGroup.GetRhythmSystem.GetRhythmInput.SuccessNoteEvent += NoteEvent;
        //일반_성공 노트만큼 회복 1씩_1레벨

        yield return new WaitUntil(() => bit4 == true);
        player.PlayerEffectSystem.PlayEffect("SoftEcho_Buff_Effect", player.transform.position);
    }

    void NoteEvent(GameObject gameobject)
    {
        Player.PlayerEffectSystem.PlayEffect("Tuning_Effect", Player.transform.position);
        Player.addHP(data.HP_Recover);
    }
}

public class EnergizerAction : RecoverAction
{
    public EnergizerAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 타이밍
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        yield return new WaitUntil(() => bit2 == true);
        //이펙트


        yield return new WaitUntil(() => bit3 == true);

        //일반_캐릭터 스킬 게이지 증가 + 체력 회복_1레

        //체력 회복
        RecoverHP(player, cardData);

        //스킬 포인트 증가
        int skillPoint = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, out skillPoint);
        skillPoint += cardData.Char_SkillPoint_Get;
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, skillPoint);

        // 스킬포인트 이펙트

        yield break;
    }
}

public class BuildUpAction : PlayerBaseCardAction
{
    CardData datas;
    public BuildUpAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);
        datas = cardData;
        yield return new WaitUntil(() => bit3 == true);
        //이펙트
        player.PlayerEffectSystem.PlayEffect("BuildUp_Effect" , player.transform.position+ new Vector3(0,.8f,0));

        yield return new WaitUntil(() => bit4 == true);
        player.PlayerEffectSystem.PlayEffect("BuildUpBuff_Effect", player.transform.position);
        GameManager.instance.EnemysGroup.GetRhythmSystem.GetRhythmInput.SuccessNoteEvent += BuildUpEvent;
        //노트성공당 스킬게이지 1;
    }

    void BuildUpEvent(GameObject obj)
    {
        //스킬게이지
        int skill_Point = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA,out skill_Point);

        skill_Point += datas.Char_SkillPoint_Get;
    }
}

public class RockSpiritAction : GetBarrierAction
{
    public RockSpiritAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);
        yield return new WaitUntil(() => bit1 == true);
        yield return new WaitForSeconds(.03f);
        //균열에서 빛이내려옴
        player.PlayerEffectSystem.PlayEffect("RockSpirit_Effect", player.transform.position);

        //베리어
        player.AddBarrier(cardData.Barrier_Get);

        //스킬 포인트 증가
        int skillPoint = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, out skillPoint);
        skillPoint += cardData.Char_SkillPoint_Get;
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, skillPoint);

        yield return null;
        //베리어 2증가 스킬게이지 1 증가
    }
}