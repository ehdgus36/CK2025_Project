using Spine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        yield return new WaitUntil(() => bit3 == true);

        yield return new WaitForSeconds(0.1f);
        player.PlayerEffectSystem.EffectObject("Break_Effect", Target.transform.position);
        yield return SingleAttack(cardData,Target,SingleAttackCount);
        yield return new WaitUntil(() => bit4 == true);
        //player.PlayerEffectSystem.StopEffect("Break_Effect");

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
        
        List<Enemy> enemies = new List<Enemy>();

        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            enemies.Add(GameManager.instance.EnemysGroup.Enemys[i]);
        }
     
        for (int i = 0; i < attackCount; i++)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                enemies[j].TakeDamage(GameManager.instance.Player, cardData.Attack_DMG, cardData.CardBuff);
            }
            if (i < attackCount - 1)
                yield return new WaitForSeconds(.3f);
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
        Vector3 targetPos = GameObject.Find("CenterPoint").transform.position;

        for (int i = 0; i < 20; i++)
        {
            ball.transform.position = Bezier(GameManager.instance.Player.transform.position, Target.transform.position + new Vector3(0, 3, 0), targetPos, T);
            T += 0.05f;
            yield return new WaitForSeconds(0.02f);
        }

      
        Player.PlayerEffectSystem.EffectObject("NoteBomb_Effect", targetPos);
        
        yield return MultiAttack(cardData, Target, 1);
        GameManager.instance.UIInputSetActive(true);
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
        yield return new WaitUntil(() => bit3 == true);

        yield return new WaitForSeconds(0.08f);
        player.PlayerEffectSystem.EffectObject("PowerBreak_Effect", Target.transform.position); // 수정 예정
        yield return SingleAttack(cardData, Target, SingleAttackCount);
    }
}

public class SoloAction : SingleAttackAction
{
    Card Card;

    public SoloAction(Card card) : base(card)
    {
    }

    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        //애니메이션 실행
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AnimationEvent, CompleteEvent);

        //이펙트
        GameObject SoloEffect = player.PlayerEffectSystem.EffectObject("Solo_Effect", player.transform.position);


       System.Func<Vector3, Vector3, Vector3, float, Vector3> Bezier =
       (P0, P1, P2, t) =>
       {
           var M0 = Vector3.Lerp(P0, P1, t);
           var M1 = Vector3.Lerp(P1, P2, t);
           return Vector3.Lerp(M0, M1, t);
       };

        Card = card;

        yield return new WaitUntil(() => bit1 == true);

        yield return new WaitForSeconds(0.3f);

        //이펙트 이동 부분
        float T = 0f;
       
        Vector3 StartPos = SoloEffect.transform.position;

        for (int i = 0; i < 21; i++)
        {
            SoloEffect.transform.position = Bezier(StartPos, Target.transform.position + new Vector3(0, 3, 0), Target.transform.position + new Vector3(0,1,0),T);
            T += 0.05f - (i / 1000);

           

            yield return new WaitForSeconds(0.01f);
        }
        yield return SingleAttack(cardData, Target, SingleAttackCount); // 단일 데미지
        player.PlayerEffectSystem.StopEffect("Solo_Effect");
        yield return new WaitUntil(() => bit3 == true);
    }

    protected override void CompleteEvent(TrackEntry entry)
    {
        base.CompleteEvent(entry);

        List<Card> changeCard = new List<Card>();
        changeCard.AddRange(Card.GetCardSloat.ReadData<Card>().Where(id => id.cardData.Card_ID == "C1021").ToList());
        changeCard.AddRange(GameManager.instance.PlayerCDSlotGroup.GetPlayerDack[0].GetDackDatas.Where(id => id.cardData.Card_ID == "C1021").ToList());
        changeCard.AddRange(GameManager.instance.CardCemetery.CemeteryCardList.Where(id => id.cardData.Card_ID == "C1021").ToList());
        //덱, 핸드, 묘지에 있는 모든 C1021 카드를 C2021로 변환


        for (int i = 0; i < changeCard.Count; i++)
        {
            changeCard[i].Initialized("C2021");
        }
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


        yield return new WaitUntil(() => bit3 == true);
        Reroll_Card reroll = card.GetComponent<Reroll_Card>();

        if (card.GetComponent<Reroll_Card>() == null)
        {
            card.gameObject.AddComponent<Reroll_Card>();
            reroll = card.GetComponent<Reroll_Card>();
        }
        int disCard = 0;

        reroll.Excute(card.GetCardSloat, out disCard);


        yield return new WaitUntil(() => bit4 == true);
        //이펙트 추가

        //핸드에 있는 모든 카드버리기(버린 카드 수 만큼 공격 횟수 증가)
        

        yield return MultiAttack(cardData, Target, disCard); // 단일 데미지

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

       System.Func<Vector3, Vector3, Vector3, float, Vector3> Bezier =
       (P0, P1, P2, t) =>
       {
           var M0 = Vector3.Lerp(P0, P1, t);
           var M1 = Vector3.Lerp(P1, P2, t);
           return Vector3.Lerp(M0, M1, t);
       };

        GameObject[] SoloEffects = new GameObject[3];
        Enemy[] randTarget = new Enemy[3];

        SoloEffects[0] = player.PlayerEffectSystem.EffectObject("FreestyleSolo_Effect1", player.transform.position);
        SoloEffects[1] = player.PlayerEffectSystem.EffectObject("FreestyleSolo_Effect2", player.transform.position);
        SoloEffects[2] = player.PlayerEffectSystem.EffectObject("FreestyleSolo_Effect3", player.transform.position);


        yield return new WaitUntil(() => bit3 == true);
        //이펙트 추가

        

        //랜덤 타겟 받기
        for (int i = 0; i < int.Parse(cardData.Attack_Count)-1; i++)
        {
            randTarget[i] = GameManager.instance.EnemysGroup.Enemys[Random.Range(0, GameManager.instance.EnemysGroup.Enemys.Count)];
        }

        randTarget[2] = Target;


        Vector3[] StartPos = new Vector3[3];
        float T = 0;

        StartPos[0] = SoloEffects[0].transform.position;
        StartPos[1] = SoloEffects[1].transform.position;
        StartPos[2] = SoloEffects[2].transform.position;


        for (int i = 0; i < 21; i++)
        {

            SoloEffects[0].transform.position = Bezier(StartPos[0], Target.transform.position + new Vector3(0, 3, 0), randTarget[0].transform.position + new Vector3(0, 1, 0), T);
            SoloEffects[1].transform.position = Bezier(StartPos[1], Target.transform.position + new Vector3(0, .5f, 0), randTarget[1].transform.position + new Vector3(0, 1, 0), T);
            SoloEffects[2].transform.position = Bezier(StartPos[2], Target.transform.position + new Vector3(0, -3, 0), randTarget[2].transform.position + new Vector3(0, 1, 0), T);
            T += 0.05f - (i / 1000f);
            yield return new WaitForSeconds(0.03f);
        }



        //랜덤 공격
        for (int i = 0; i < int.Parse(cardData.Attack_Count); i++)
        {
            yield return SingleAttack(cardData, randTarget[i], SingleAttackCount);
        }


        player.PlayerEffectSystem.StopEffect("FreestyleSolo_Effect1");
        player.PlayerEffectSystem.StopEffect("FreestyleSolo_Effect2");
        player.PlayerEffectSystem.StopEffect("FreestyleSolo_Effect3");

        // 단일 데미지


        //덱, 핸드, 묘지에 있는 모든 C1021 카드를 C2021로 변환
        List<Card> changeCard = new List<Card>();
        changeCard.AddRange( card.GetCardSloat.ReadData<Card>().Where(id => id.cardData.Card_ID == "C2021").ToList());
        changeCard.AddRange(GameManager.instance.PlayerCDSlotGroup.GetPlayerDack[0].GetDackDatas.Where(id => id.cardData.Card_ID == "C2021").ToList());
        changeCard.AddRange(GameManager.instance.CardCemetery.CemeteryCardList.Where(id => id.cardData.Card_ID == "C2021").ToList());

        for (int i = 0; i < changeCard.Count; i++)
        {
            changeCard[i].Initialized("C3011");
        }

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
        GameObject ChargeObj = player.PlayerEffectSystem.EffectObject("LegendarySoloCharge_Effect", player.transform.position);

        yield return new WaitUntil(() => bit1 == true);
        //전설_전체 몬스터 스킬 게이지 흡수 + 흡수량 만큼 전체 데미지 2씩_1레벨
       
        

        yield return new WaitUntil(() => bit2 == true);
        //정기빨기


        yield return new WaitUntil(() => bit3 == true);
        yield return new WaitForSeconds(0.03f);
        
        float T = 0f;

        Vector3 starPos = ChargeObj.transform.position;
        Vector3 targetPos = GameObject.Find("CenterPoint").transform.position;
        for (int i = 0; i < 11; i++)
        {
            ChargeObj.transform.position = Vector3.Lerp(starPos, targetPos, T);
            T += 0.1f;
            yield return new WaitForSeconds(0.015f);
        }
        //이펙트 추가
        player.PlayerEffectSystem.PlayEffect("LegendarySoloHit_Effect", targetPos);
      
        yield return MultiAttack(cardData, Target, int.Parse(cardData.Attack_Count));
        player.PlayerEffectSystem.StopEffect("LegendarySoloCharge_Effect");
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

      
        //전설_전체 몬스터 스킬 게이지 흡수 + 흡수량 만큼 전체 데미지 2씩_1레벨
        player.PlayerEffectSystem.PlayEffect("SoulShoutingCharge_Effect", player.transform.position);
        int CountValue = GameManager.instance.EnemysGroup.DrainSkillPoint();

        yield return new WaitUntil(() => bit2 == true);
        //정기빨기

      
        player.PlayerEffectSystem.StopEffect("SoulShoutingCharge_Effect");
        player.PlayerEffectSystem.PlayEffect("SoulShoutingPlayer_Effect", player.transform.position);
        player.PlayerEffectSystem.PlayEffect("SoulShoutingShoot_Effect", player.transform.position);
        

        yield return new WaitUntil(() => bit4 == true);
        //이펙트 추가
        yield return MultiAttack(cardData, Target, CountValue);

    }
}


public class SkillAction : MultiAttackAction
{
    public SkillAction(Card card) : base(card)
    {
    }


    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return new WaitForSeconds(1);
        //이펙트 추가

        Vector3 targetPos = GameObject.Find("CenterPoint").transform.position;

        player.PlayerEffectSystem.PlayEffect("PowerBreak_Effect", targetPos);
       
        yield return MultiAttack(cardData, Target, 1);

      

        CompleteEvent(null);
        card.transform.parent.gameObject.SetActive(false);
        GameManager.instance.UIInputSetActive(true);

    }
}

//"적 전체에게 <color=#ff2e2e>12데미지</color>와 <color=#ff2e2e>화상 2턴</color>을 부여한다."
public class Skill2Action : MultiAttackAction
{
    public Skill2Action(Card card) : base(card)
    {
    }


    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return new WaitForSeconds(1);
        //이펙트 추가

        Vector3 targetPos = GameObject.Find("CenterPoint").transform.position;

        player.PlayerEffectSystem.PlayEffect("PowerBreak_Effect", targetPos);

        yield return MultiAttack(cardData, Target, 1);



        CompleteEvent(null);
        card.transform.parent.gameObject.SetActive(false);
        GameManager.instance.UIInputSetActive(true);


    }
}


//"노이즈의 체력을 <color=#0ab52b>30</color> 회복시킨다."
public class Skill3Action : MultiAttackAction
{
    public Skill3Action(Card card) : base(card)
    {
    }


    public override IEnumerator StartAction(Player player, Card card, CardData cardData, Enemy Target)
    {
        yield return new WaitForSeconds(1);
        //이펙트 추가

        Vector3 targetPos = GameObject.Find("CenterPoint").transform.position;

        player.addHP(cardData.HP_Recover);

        



        CompleteEvent(null);
        card.transform.parent.gameObject.SetActive(false);
        GameManager.instance.UIInputSetActive(true);

    }
}