
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GameDataSystem;
using Spine;
using UnityEngine.UI;


[System.Serializable]
public struct dicobj
{
    public string key;
    public bool value;
}

public class Card : MonoBehaviour
{
    [SerializeField] public string CardID;
    [SerializeField] public Sprite DescSprite;
    [SerializeField] Image cardImage;
    [SerializeField] Material BaseMaterial;



    Buff CardBuff = null;
    public CardData cardData { get; protected set; }

    [HideInInspector] public int DamageBuff = 0;
    [HideInInspector] public int Buff_Recover_HP = 0;

    bool isCardEnd = false;
    Enemy EnemyTarget;
    public bool IsCardEnd { get { return isCardEnd; } }

    protected SlotGroup CardSloats;

    Vector3 PlayerStartPos;

    Dictionary<string, int> CardDataVariable = new Dictionary<string, int>();

    int DiscardCnt = 0; //어빌리티 버린카드 갯수

    PlayerBaseCardAction CardAction;

    public virtual void Initialized(SlotGroup slotGroup)
    {
        CardSloats = slotGroup;

        isCardEnd = false;
        object data = null;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(CardID, out data))
        {
            cardData = (CardData)data;

        }
        else
        {
            Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다 " + this.gameObject.name);
        }



        //Range_Type Attack_Count    Damage  Status_Type Status_Turn Damage_Buff HP_Recover  HP_Loss Barrier_Get Barrier_Loss)
        //사용변수 초기화 
        CardDataVariable.Clear();
    }

    public virtual void Initialized(string cardID)
    {
        if (cardID == "" || cardID == null) return;
        object data = null;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(cardID, out data))
        {
            cardData = (CardData)data;

        }
        else
        {
            Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다 " + this.gameObject.name);
        }

        string Path = "CardImage/" + cardData.Card_Im;
        Sprite cardSprite = Resources.Load<Sprite>(Path);

        Material instanceMaterial = Instantiate(BaseMaterial);
        instanceMaterial.SetTexture("_OverlayTex", cardSprite.texture);

        if (cardImage != null)
        {
            cardImage.material = instanceMaterial;
        }

        this.CardID = cardID;

        this.gameObject.name = cardData.Card_Name_EN;

        CardActionInitialized(cardID);
    }

    public void CardActionInitialized(string cardID)
    {

        if ("C0011" == cardID) { CardAction = new SingleAttackAction(); }
        if ("C0012" == cardID) { CardAction = new SingleAttackAction(); }
        if ("C0021" == cardID) { CardAction = new NoteBombAction(); }
        if ("C0022" == cardID) { CardAction = new NoteBombAction(); }
        if ("C0031" == cardID) { CardAction = new GetBarrierAction(); }
        if ("C0032" == cardID) { CardAction = new GetBarrierAction(); }
        if ("C0041" == cardID) { CardAction = new RecoverAction(); }
        if ("C0042" == cardID) { CardAction = new RecoverAction(); }
        if ("C1011" == cardID) { CardAction = new PowerBreakAction(); }
        if ("C1012" == cardID) { CardAction = new PowerBreakAction(); }
        if ("C1021" == cardID) { CardAction = new SoloAction(); }
        if ("C1022" == cardID) { CardAction = new SoloAction(); }
        if ("C1031" == cardID) { CardAction = new VolumeShieldAction(); }
        if ("C1032" == cardID) { CardAction = new VolumeShieldAction(); }
        if ("C1041" == cardID) { CardAction = new DistortionAction(); }
        if ("C1042" == cardID) { CardAction = new DistortionAction(); }
        if ("C1051" == cardID) { CardAction = new FireStrokeAction(); }
        if ("C1052" == cardID) { CardAction = new FireStrokeAction(); }
        if ("C1061" == cardID) { CardAction = new SoftEchoAction(); }
        if ("C1062" == cardID) { CardAction = new SoftEchoAction(); }
        if ("C1071" == cardID) { CardAction = new EnergizerAction(); }
        if ("C1072" == cardID) { CardAction = new EnergizerAction(); }
        if ("C2011" == cardID) { CardAction = new WildRiffAction(); }
        if ("C2012" == cardID) { CardAction = new WildRiffAction(); }
        if ("C2021" == cardID) { CardAction = new FreestyleSoloAction(); }
        if ("C2022" == cardID) { CardAction = new FreestyleSoloAction(); }
        if ("C2031" == cardID) { CardAction = new CursedShieldAction(); }
        if ("C2032" == cardID) { CardAction = new CursedShieldAction(); }
        if ("C2041" == cardID) { CardAction = new BurningStageAction(); }
        if ("C2042" == cardID) { CardAction = new BurningStageAction(); }
        if ("C2051" == cardID) { CardAction = new BuildUpAction(); }
        if ("C2052" == cardID) { CardAction = new BuildUpAction(); }
        if ("C2061" == cardID) { CardAction = new EncoreAction(); }
        if ("C2062" == cardID) { CardAction = new EncoreAction(); }
        if ("C3011" == cardID) { CardAction = new LegendarySoloAction(); }
        if ("C3012" == cardID) { CardAction = new LegendarySoloAction(); }
        if ("C3021" == cardID) { CardAction = new RockSpiritAction(); }
        if ("C3022" == cardID) { CardAction = new RockSpiritAction(); }
        if ("C3031" == cardID) { CardAction = new STFUAction(); }
        if ("C3032" == cardID) { CardAction = new STFUAction(); }
        if ("C3041" == cardID) { CardAction = new HellfireAction(); }
        if ("C3042" == cardID) { CardAction = new HellfireAction(); }
        if ("C3051" == cardID) { CardAction = new BlessingofRockAction(); }
        if ("C3052" == cardID) { CardAction = new BlessingofRockAction(); }
        if ("C3061" == cardID) { CardAction = new SoulShoutingAction(); }
        if ("C3062" == cardID) { CardAction = new SoulShoutingAction(); }
    }

    public virtual void TargetExcute(Enemy Target, Card nextCard = null)
    {
        PlayerStartPos = GameManager.instance.Player.transform.position;// 플레이어 처음 위치 저장

        if (Target.isDie == true) //카드 넣기
        {
            SetOutLineColor(Color.white);
            isCardEnd = true;
            return;
        }
        // 사용한 카드 묘지로 보내는 기능
        GameManager.instance.CardCemetery.Insert(this);

        if (cardData.Move_Type == "M")
        {
            GameManager.instance.Player.transform.position = Target.gameObject.transform.position - new Vector3(6, 0, 0);
        }
        //if (nextCard != null) nextCard.DamageBuff = cardData.Damage_Buff; // 조건문 만족시 버프 추가


        EnemyTarget = Target;
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code, false, AttackEvent, CompleteEvent); // 최종형
                                                                                                                        // GameManager.instance.UIInputSetActive(false);

        StartCoroutine(CardAction.StartAction(GameManager.instance.Player,this, this.cardData, EnemyTarget));

    }


    //스파인에서 AttackEvent가 발생할 때 실행할거
    public virtual void AttackEvent(TrackEntry entry, Spine.Event e)
    {

        if (e.Data.Name != "4bit") return;

        DamageBuff = GameManager.instance.ExcutSelectCardSystem.BuffDamage;

        Debug.Log("데미지 버프" + DamageBuff);
        for (int i = 0; i < CardDataVariable["Attack_Count"]; i++)
        {

            //if (cardData.Range_Type == 1) // 단일 공격
            //{
            //    EnemyTarget.TakeDamage(GameManager.instance.Player, CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);

            //    if (cardData.Effect_Pos == "E") GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);

            //    if (cardData.Effect_Pos == "P") GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, GameManager.instance.Player.transform.position);



            //}



            //if (cardData.Range_Type == 2) // 전체 공격
            //{
            //    if (cardData.Ani_Code == "notebomb_Ani") //  임시 로직 새로 생각하기
            //    {
            //        //GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);
            //        StartCoroutine(NoteBomb());
            //    }
            //    else
            //    {

            //        List<Enemy> AttackEnemies = new List<Enemy>(GameManager.instance.EnemysGroup.Enemys);
            //        GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);

            //        for (int j = 0; j < AttackEnemies.Count; j++)
            //        {
            //            AttackEnemies[j].TakeDamage(GameManager.instance.Player,CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);
            //        }
            //    }
            //}

            //if (cardData.Range_Type == 3)//PlayerBuff
            //{
            //    if (cardData.Effect_Pos == "E") GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);

            //    if (cardData.Effect_Pos == "P") GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, GameManager.instance.Player.transform.position);


            //    //EnemyTarget.TakeDamage(CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);
            //}


            //if (cardData.Range_Type == 4) // 랜덤 공격
            //{
            //    List<Enemy> AttackEnemies = new List<Enemy>(GameManager.instance.EnemysGroup.Enemys);
            //    GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);
            //    AttackEnemies[Random.Range(0, AttackEnemies.Count)].TakeDamage(GameManager.instance.Player,CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);
            //}
        }
        Debug.Log("이번 공격 애니메이션에서 Slash 이벤트 감지!"); // 대충 데미지 넣는거 구현       
        GameManager.instance.ComboUpdate(Random.Range(17010, 21204));
        GameManager.instance.Player.addHP(CardDataVariable["HP_Recover"] + Buff_Recover_HP);
        GameManager.instance.Player.LossHP(CardDataVariable["HP_Loss"]);// 배리어 혹은 체력 까기

        GameManager.instance.Player.AddBarrier(CardDataVariable["Barrier_Get"]);
        GameManager.instance.Player.LossBarrier(CardDataVariable["Barrier_Loss"]);

        //GameManager.instance.ExcutSelectCardSystem.BuffDamage += cardData.Damage_Buff;

        GameManager.instance.FMODManagerSystem.PlayEffectSound(cardData.Sound_Code);
    }

    // 애니메이션이 마무리 될때 할거
    public virtual void CompleteEvent(TrackEntry entry)
    {
        isCardEnd = true;
        // 추가버프는 1회용이기 때문에 항상 초기화
        Buff_Recover_HP = 0;



        if (cardData.Ani_Code == "notebomb_Ani") // 공격이 끝날때 데미지
        {
            List<Enemy> AttackEnemies = new List<Enemy>(GameManager.instance.EnemysGroup.Enemys);
            GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);

            for (int i = 0; i < AttackEnemies.Count; i++)
            {
                AttackEnemies[i].TakeDamage(GameManager.instance.Player, CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);
            }
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Common_Attack/Break");
        }

        // GameManager.instance.UIInputSetActive(true);
        if (this.gameObject.activeInHierarchy == true)
        {
            StartCoroutine(PlayerReturnDelay());
        }

        //GameManager.instance.UIInputSetActive(true);
    }

    IEnumerator PlayerReturnDelay()
    {
        yield return new WaitForSeconds(.5f);
        GameManager.instance.Player.transform.position = PlayerStartPos;
    }

    IEnumerator NoteBomb() //분리
    {

        GameObject controllObject = GameManager.instance.Player.PlayerEffectSystem.EffectObject("notebomb_Effect_ball", EnemyTarget.transform.position);

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
            controllObject.transform.position = Bezier(GameManager.instance.Player.transform.position, EnemyTarget.transform.position + new Vector3(0, 3, 0), EnemyTarget.transform.position, T);
            T += 0.05f;
            yield return new WaitForSeconds(0.02f);
        }


        yield return null;
    }

    public void SetOutLineColor(Color color)
    {
        if (cardImage != null)
            cardImage.material.SetColor("_BgColor", color);
    }
}
