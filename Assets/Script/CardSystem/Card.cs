
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
    [SerializeField] private Material BaseMaterial;



    Buff CardBuff = null;
    public CardData cardData { get; protected set; }

    [HideInInspector] public int DamageBuff = 0;
    [HideInInspector] public int Buff_Recover_HP = 0;

    bool isCardEnd = false;
    Enemy EnemyTarget;
    public bool IsCardEnd { get { return isCardEnd; } set { isCardEnd = value; } }

    protected SlotGroup CardSloats;

    public SlotGroup GetCardSloat { get => CardSloats; }

    Vector3 PlayerStartPos;

    Dictionary<string, int> CardDataVariable = new Dictionary<string, int>();

    int DiscardCnt = 0; //어빌리티 버린카드 갯수

    protected PlayerBaseCardAction CardAction;

    public virtual void Initialized(SlotGroup slotGroup)
    {
        CardSloats = slotGroup;

        isCardEnd = false;
        object data = null;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(CardID, out data))
        {
            cardData = ((CardData)data).Clone();

        }
        else
        {
            Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다 " + this.gameObject.name);
        }

        CardActionInitialized(cardData.Card_ID);
    }

    public void ReflashCardData()
    {
        object data = null;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(CardID, out data))
        {
            cardData = ((CardData)data).Clone();

        }
        else
        {
            Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다 " + this.gameObject.name);
        }
    }

    public virtual void Initialized(string cardID)
    {
        if (cardID == "" || cardID == null) return;

        object data = null;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(cardID, out data))
        {
            cardData = ((CardData)data).Clone();

        }
        else
        {
            Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다 " + this.gameObject.name);
        }

        string Path = "CardImage/" + cardData.Card_Im;
        Sprite cardSprite = Resources.Load<Sprite>(Path);

        Material instanceMaterial = new Material(BaseMaterial);
        if (cardSprite != null)
        {
           
            instanceMaterial.SetTexture("_OverlayTex", cardSprite.texture);
        }

        if (cardImage != null)
        {
            cardImage.material = instanceMaterial;
        }

        this.CardID = cardData.Card_ID;

        this.gameObject.name = cardData.Card_Name_EN;

        CardActionInitialized(cardID);
    }

    public void CardActionInitialized(string cardID)
    {

        if ("C0011" == cardID) { CardAction = new SingleAttackAction(this); }
        if ("C0012" == cardID) { CardAction = new SingleAttackAction(this); }
        if ("C0021" == cardID) { CardAction = new NoteBombAction(this); }
        if ("C0022" == cardID) { CardAction = new NoteBombAction(this); }
        if ("C0031" == cardID) { CardAction = new GetBarrierAction(this); }
        if ("C0032" == cardID) { CardAction = new GetBarrierAction(this); }
        if ("C0041" == cardID) { CardAction = new RecoverAction(this); }
        if ("C0042" == cardID) { CardAction = new RecoverAction(this); }
        if ("C1011" == cardID) { CardAction = new PowerBreakAction(this); }
        if ("C1012" == cardID) { CardAction = new PowerBreakAction(this); }
        if ("C1021" == cardID) { CardAction = new SoloAction(this); }
        if ("C1022" == cardID) { CardAction = new SoloAction(this); }
        if ("C1031" == cardID) { CardAction = new VolumeShieldAction(this); }
        if ("C1032" == cardID) { CardAction = new VolumeShieldAction(this); }
        if ("C1041" == cardID) { CardAction = new DistortionAction(this); }
        if ("C1042" == cardID) { CardAction = new DistortionAction(this); }
        if ("C1051" == cardID) { CardAction = new FireStrokeAction(this); }
        if ("C1052" == cardID) { CardAction = new FireStrokeAction(this); }
        if ("C1061" == cardID) { CardAction = new SoftEchoAction(this); }
        if ("C1062" == cardID) { CardAction = new SoftEchoAction(this); }
        if ("C1071" == cardID) { CardAction = new EnergizerAction(this); }
        if ("C1072" == cardID) { CardAction = new EnergizerAction(this); }
        if ("C2011" == cardID) { CardAction = new WildRiffAction(this); }
        if ("C2012" == cardID) { CardAction = new WildRiffAction(this); }
        if ("C2021" == cardID) { CardAction = new FreestyleSoloAction(this); }
        if ("C2022" == cardID) { CardAction = new FreestyleSoloAction(this); }
        if ("C2031" == cardID) { CardAction = new CursedShieldAction(this); }
        if ("C2032" == cardID) { CardAction = new CursedShieldAction(this); }
        if ("C2041" == cardID) { CardAction = new BurningStageAction(this); }
        if ("C2042" == cardID) { CardAction = new BurningStageAction(this); }
        if ("C2051" == cardID) { CardAction = new BuildUpAction(this); }
        if ("C2052" == cardID) { CardAction = new BuildUpAction(this); }
        if ("C2061" == cardID) { CardAction = new EncoreAction(this); }
        if ("C2062" == cardID) { CardAction = new EncoreAction(this); }
        if ("C3011" == cardID) { CardAction = new LegendarySoloAction(this); }
        if ("C3012" == cardID) { CardAction = new LegendarySoloAction(this); }
        if ("C3021" == cardID) { CardAction = new RockSpiritAction(this); }
        if ("C3022" == cardID) { CardAction = new RockSpiritAction(this); }
        if ("C3031" == cardID) { CardAction = new STFUAction(this); }
        if ("C3032" == cardID) { CardAction = new STFUAction(this); }
        if ("C3041" == cardID) { CardAction = new HellfireAction(this); }
        if ("C3042" == cardID) { CardAction = new HellfireAction(this); }
        if ("C3051" == cardID) { CardAction = new BlessingofRockAction(this); }
        if ("C3052" == cardID) { CardAction = new BlessingofRockAction(this); }
        if ("C3061" == cardID) { CardAction = new SoulShoutingAction(this); }
        if ("C3062" == cardID) { CardAction = new SoulShoutingAction(this); }
       
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

        GameManager.instance.FMODManagerSystem.PlayEffectSound(cardData.Sound_Code);
        EnemyTarget = Target;

        
        StartCoroutine(CardAction.StartAction(GameManager.instance.Player,this, this.cardData, EnemyTarget));

    }


    public void SetOutLineColor(Color color)
    {
        if (cardImage != null)
            cardImage.material.SetColor("_BgColor", color);
    }
}
