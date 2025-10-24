using Spine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAttackCardAction 
{
  
   
}

//브레이크 아웃
public class SingleAttackAction : PlayerBaseCardAction
{
    public SingleAttackAction(Card card) : base(card)
    {
    }

    protected virtual int SingleAttackCount { get { return 1; } }
   

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent , CompleteEvent);



        //bit4 일때 데미지 처리
        yield return new WaitUntil(() => bit4 == true);
        player.PlayerEffectSystem.EffectObject("Break_Effect", Target.transform.position);
        GameManager.instance.UIInputSetActive(true);
        yield return SingleAttack(cardData,Target,SingleAttackCount);
    }

    public IEnumerator SingleAttack(CardData cardData,Enemy Target , int attackCount)
    {
        int AttackCount = attackCount;

        for (int i = 0; i < AttackCount; i++)
        {
            Target.TakeDamage(GameManager.instance.Player, cardData.Attack_DMG, cardData.CardBuff);

            if (i < AttackCount - 1)
                yield return new WaitForSeconds(.2f);
        }
    }
}

public class MultiAttackAction : PlayerBaseCardAction
{
    public MultiAttackAction(Card card) : base(card)
    {
    }

    protected virtual int MultiAttackCount { get { return 1; } }
    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);


        yield return new WaitUntil(() => bit4 == true);
        player.PlayerEffectSystem.EffectObject("Notebomb_Effect", Target.transform.position);
        yield return MultiAttack(cardData, Target, MultiAttackCount);
    }

    public IEnumerator MultiAttack(CardData cardData, Enemy Target , int attackCount)
    {
        int AttackCount = attackCount;
        List<Enemy> enemies = GameManager.instance.EnemysGroup.Enemys;

        for (int i = 0; i < AttackCount; i++)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                enemies[j].TakeDamage(GameManager.instance.Player, cardData.Attack_DMG, cardData.CardBuff);
            }
            if (i < AttackCount - 1)
                yield return new WaitForSeconds(.2f);
        }
    }
}


public class NoteBombAction : MultiAttackAction
{
    Player Player;

    public NoteBombAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        Player = player;
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        yield return new WaitUntil(() => bit2 == true);
        GameObject ball = Player.PlayerEffectSystem.EffectObject("NoteBomb_Effect_ball", Player.transform.position);

        System.Func<Vector3, Vector3, Vector3, float, Vector3> Bezier =
        (P0, P1, P2, t) =>
        {
            var M0 = Vector3.Lerp(P0, P1, t);
            var M1 = Vector3.Lerp(P1, P2, t);
            return Vector3.Lerp(M0, M1, t);
        };


        float T = 0f;

        for (int i = 0; i < 20; i++)
        {
            ball.transform.position = Bezier(GameManager.instance.Player.transform.position, Target.transform.position + new Vector3(0, 3, 0), Target.transform.position, T);
            T += 0.05f;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitUntil(() => bit4 == true);
        Player.PlayerEffectSystem.EffectObject("NoteBomb_Effect", Target.transform.position);
        GameManager.instance.UIInputSetActive(true);
        yield return MultiAttack(cardData, Target, int.Parse(cardData.Attack_Count));
    }

    
}


//아직 애니메이션 미정
public class PowerBreakAction : SingleAttackAction
{
    public PowerBreakAction(Card card) : base(card)
    {
    }

    protected override int SingleAttackCount => 2;

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        //bit4 일때 데미지 처리
        yield return new WaitUntil(() => bit4 == true);
        player.PlayerEffectSystem.EffectObject("PowerBreak_Effect", Target.transform.position); // 수정 예정
        yield return SingleAttack(cardData, Target, SingleAttackCount);
    }
}

public class SoloAction : SingleAttackAction
{
    public SoloAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);


        yield return new WaitUntil(() => bit4 == true);
        //이펙트 추가
        yield return SingleAttack(cardData, Target, SingleAttackCount); // 단일 데미지


        //덱, 핸드, 묘지에 있는 모든 C1021 카드를 C2021로 변환
        
    }
}

public class WildRiffAction :MultiAttackAction
{
    public WildRiffAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        //덱, 핸드, 묘지에 있는 모든 C1021 카드를 C2021로 변환 카드 버리기
        yield return new WaitUntil(() => bit4 == true);
        //이펙트 추가

        //핸드에 있는 모든 카드버리기(버린 카드 수 만큼 공격 횟수 증가)
        yield return MultiAttack(cardData, Target, MultiAttackCount); // 단일 데미지

    }
}

public class FreestyleSoloAction: SingleAttackAction 
{
    public FreestyleSoloAction(Card card) : base(card)
    {
    }

    //덱, 핸드, 묘지에 있는 모든 C2021 카드를 C3011로 변환
    //랜덤 단일데미지

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);


        yield return new WaitUntil(() => bit3 == true);
        //이펙트 추가

        //랜덤 공격
        for (int i = 0; i < int.Parse(cardData.Attack_Count); i++)
        {
            Enemy randTarget = GameManager.instance.EnemysGroup.Enemys[Random.Range(0, GameManager.instance.EnemysGroup.Enemys.Count)];

            yield return SingleAttack(cardData, randTarget, SingleAttackCount);

            
        }// 단일 데미지


        //덱, 핸드, 묘지에 있는 모든 C1021 카드를 C2021로 변환

    }
}


public class LegendarySoloAction : MultiAttackAction
{
    public LegendarySoloAction(Card card) : base(card)
    {
    }

    protected override int MultiAttackCount => 5;

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        yield return new WaitUntil(() => bit3 == true);
        //날아가는 오브젝트

        yield return new WaitUntil(() => bit4 == true);
        //이펙트 추가
        yield return MultiAttack(cardData, Target, MultiAttackCount);
    }

}

public class SoulShoutingAction : MultiAttackAction
{
    public SoulShoutingAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        yield return new WaitUntil(() => bit3 == true);
        //날아가는 오브젝트

        yield return new WaitUntil(() => bit4 == true);
        //이펙트 추가
        
    }
}