using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDefensiveCardAction
{
    
}


//튜닝
public class RecoverAction : PlayerBaseCardAction
{
    

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 타이밍
        player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent);
        
        yield return new WaitUntil(() => bit3 == true);

        RecoverHP(player, cardData);

        yield break;
    }

    protected void RecoverHP(Player player, CardData cardData)
    {
        player.PlayerEffectSystem.PlayEffect("bresth_Effect", player.transform.position);
        player.addHP(cardData.HP_Recover);
    }
}

//기타 쉴드
public class GetBarrierAction : PlayerBaseCardAction
{
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent);
        yield break;
    }

    protected void GetBarrier(Player player, CardData cardData)
    {
        player.PlayerEffectSystem.PlayEffect("bresth_Effect", player.transform.position);
        player.AddBarrier(cardData.Barrier_Get);
    }
}

public class VolumeShieldAction : GetBarrierAction
{
   

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent);

        //볼륨업 구현
        //볼륨없은 데이터 테이블 수치조작하고 덱 묘지 플레이어 손에 있는카드 전부 초기화
        yield break;
    }


}

//나머지 이름 작업하면 ㄱㄱ그

public class SoftEchoAction : PlayerBaseCardAction
{
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return null;
        //노트당 1씩 회복
    }
}

public class EnergizerAction : RecoverAction
{
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 타이밍
        player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent);

        yield return new WaitUntil(() => bit2 == true);
        //이펙트


        yield return new WaitUntil(() => bit3 == true);

        //일반_캐릭터 스킬 게이지 증가 + 체력 회복_1레벨
        player.addHP(cardData.HP_Recover);

        //스킬 포인트 증가
        int skillPoint = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, out skillPoint);
        skillPoint += cardData.Char_SkillPoint_Get;
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, skillPoint);


        yield break;
    }
}

public class BuildUpAction : PlayerBaseCardAction
{
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return new WaitUntil(() => bit2 == true);
        //이펙트


        yield return new WaitUntil(() => bit3 == true);

        //노트성공당 스킬게이지 1;
    }
}

public class RockSpiritAction : GetBarrierAction
{
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {

        yield return new WaitUntil(() => bit2 == true);
        //균열에서 빛이내려옴

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