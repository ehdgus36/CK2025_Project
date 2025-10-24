using System.Collections;
using UnityEngine;

public class PlayerBuffCardAction 
{
    
}

public class DistortionAction : PlayerBaseCardAction
{
    //"괴상한 음악을 연주해 적 하나에게<color=#BF4D13>취약 2턴</color>을 부여한다."} 버즈 : 적의 공격력이 20% 약화됩니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return new WaitUntil(() => bit2 == true);
        //음파 날아가기


        yield return new WaitUntil(()=> bit3 == true);
        Target.AddBuff(cardData.CardBuff);
    }
}

public class FireStrokeAction : PlayerBaseCardAction
{
    //일반_단일 번업_1레벨
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return null;
    }
}

public class CursedShieldAction : PlayerBaseCardAction
{
    //희귀_전체 버즈 + 베리어 1 증가_1레벨 버즈 : 적의 공격력이 20% 약화됩니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return null;
    }
}

public class BurningStageAction : PlayerBaseCardAction
{
    //희귀_전체 번업 + 체력 회복_1레벨
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return null;
    }
}

public class EncoreAction : PlayerBaseCardAction
{
    //희귀_단일 몬스터 스킬 게이지 감소 + 감소만큼 랜덤 번업 2씩_1레벨 감소한 만큼 
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return null;
    }
}

public class STFUAction : PlayerBaseCardAction
{
    //전설_단일 뮤트 1턴 + 단일 데미지_1레벨	뮤트 : 적의 공격력이 100% 약화됩니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return new WaitUntil(() => bit2 == true);
        //음파 날아가기


        yield return new WaitUntil(() => bit3 == true);
        Target.AddBuff(cardData.CardBuff);
    }
}

public class HellfireAction : PlayerBaseCardAction
{
    //전설_이번 턴동안 번업 적용시 번아웃으로 적용 + 전체 번아웃 1_1레벨	번업 : 적의 적용 턴 동안 2데미지를 받습니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return null;
    }
}

public class BlessingofRockAction : PlayerBaseCardAction
{
    //전설_플레이어가 혼란을 받음 + 성공 노트만큼 볼륨업 4씩 획득_1레벨	혼란 : 플레이어의 적용 턴 동안 리듬 박자가 반대로 나옵니다.
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return new WaitUntil(() => bit2 == true);
        //이펙트


        yield return new WaitUntil(() => bit3 == true);
        //이펙트
        //볼륨어
    }
}
