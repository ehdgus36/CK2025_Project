
using GameDataSystem;
using Spine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct dicobj
{
    public string key;
    public bool value;
}

public class Card : MonoBehaviour
{
    public static float UsePos = -4f; //카드가 절대죄표기준 어디 이상 올라가야 작동하는지

    [SerializeField] public string CardID;
    [SerializeField] Image cardImage;
    [SerializeField] private Material BaseMaterial;

    [SerializeField] EffectSystem effectSystem;



    Buff CardBuff = null;
    public CardData cardData { get; protected set; }

    [HideInInspector] public int DamageBuff = 0;
    [HideInInspector] public int Buff_Recover_HP = 0;

    bool isCardEnd = false;
    Enemy EnemyTarget;
    public bool IsCardEnd { get { return isCardEnd; } set { isCardEnd = value; } }
    public EffectSystem EffectSystem { get => effectSystem; }

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

        if (effectSystem != null)
        {
            effectSystem.StopEffect("CardHold_Effect");
            effectSystem.StopEffect("CardHold_Effect");
            effectSystem.StopEffect("CardHold_Effect_Epic");
            effectSystem.StopEffect("CardHold_Effect_Legend");
        }
      
        SetOutLineColor(Color.white);
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

        //Material instanceMaterial = new Material(BaseMaterial);
        if (cardSprite != null)
        {

            //instanceMaterial.SetTexture("_OverlayTex", cardSprite.texture);
        }

        if (cardImage != null)
        {
            //cardImage.material = instanceMaterial;
            cardImage.sprite = cardSprite;

            SetOutLineColor(Color.white);
        }

        this.CardID = cardData.Card_ID;

        this.gameObject.name = cardData.Card_Name_EN;

        if (effectSystem == null) effectSystem = GetComponent<EffectSystem>();

        CardActionInitialized(cardID);
    }

    public void CardActionInitialized(string cardID)
    {

        if ("CA0011" == cardID) { CardAction = new SingleAttackAction(this); }
        if ("CA0012" == cardID) { CardAction = new SingleAttackAction(this); }
        if ("CA0021" == cardID) { CardAction = new NoteBombAction(this); }
        if ("CA0022" == cardID) { CardAction = new NoteBombAction(this); }
        if ("CB0031" == cardID) { CardAction = new GetBarrierAction(this); }
        if ("CB0032" == cardID) { CardAction = new GetBarrierAction(this); }
        if ("CB0041" == cardID) { CardAction = new RecoverAction(this); }
        if ("CB0042" == cardID) { CardAction = new RecoverAction(this); }
        if ("CA1011" == cardID) { CardAction = new PowerBreakAction(this); }
        if ("CA1012" == cardID) { CardAction = new PowerBreakAction(this); }
        if ("CA1021" == cardID) { CardAction = new SoloAction(this); }
        if ("CA1022" == cardID) { CardAction = new SoloAction(this); }
        if ("CB1031" == cardID) { CardAction = new VolumeShieldAction(this); }
        if ("CB1032" == cardID) { CardAction = new VolumeShieldAction(this); }
        if ("CD1041" == cardID) { CardAction = new DistortionAction(this); }
        if ("CD1042" == cardID) { CardAction = new DistortionAction(this); }
        if ("CD1051" == cardID) { CardAction = new FireStrokeAction(this); }
        if ("CD1052" == cardID) { CardAction = new FireStrokeAction(this); }
        if ("CB1061" == cardID) { CardAction = new SoftEchoAction(this); }
        if ("CB1062" == cardID) { CardAction = new SoftEchoAction(this); }
        if ("CB1071" == cardID) { CardAction = new EnergizerAction(this); }
        if ("CB1072" == cardID) { CardAction = new EnergizerAction(this); }
        if ("" == cardID) { CardAction = new WildRiffAction(this); }
        if ("" == cardID) { CardAction = new WildRiffAction(this); }
        if ("CA2021" == cardID) { CardAction = new FreestyleSoloAction(this); }
        if ("CA2022" == cardID) { CardAction = new FreestyleSoloAction(this); }
        if ("CD2031" == cardID) { CardAction = new CursedShieldAction(this); }
        if ("CD2032" == cardID) { CardAction = new CursedShieldAction(this); }
        if ("CD2041" == cardID) { CardAction = new BurningStageAction(this); }
        if ("CD2042" == cardID) { CardAction = new BurningStageAction(this); }
        if ("CB2051" == cardID) { CardAction = new BuildUpAction(this); }
        if ("CB2052" == cardID) { CardAction = new BuildUpAction(this); }
        if ("CD2061" == cardID) { CardAction = new EncoreAction(this); }
        if ("CD2062" == cardID) { CardAction = new EncoreAction(this); }
        if ("CA3011" == cardID) { CardAction = new LegendarySoloAction(this); }
        if ("CA3012" == cardID) { CardAction = new LegendarySoloAction(this); }
        if ("CB3021" == cardID) { CardAction = new RockSpiritAction(this); }
        if ("CB3022" == cardID) { CardAction = new RockSpiritAction(this); }
        if ("CD3031" == cardID) { CardAction = new STFUAction(this); }
        if ("CD3032" == cardID) { CardAction = new STFUAction(this); }
        if ("CD3041" == cardID) { CardAction = new HellfireAction(this); }
        if ("CD3042" == cardID) { CardAction = new HellfireAction(this); }
        if ("CD3051" == cardID) { CardAction = new BlessingofRockAction(this); }
        if ("CD3052" == cardID) { CardAction = new BlessingofRockAction(this); }
        if ("CA3061" == cardID) { CardAction = new SoulShoutingAction(this); }
        if ("CA3062" == cardID) { CardAction = new SoulShoutingAction(this); }

        if (this.gameObject.GetComponent<DragDropUI>() != null)
        {
            if (cardData.Target_Type == "2") this.gameObject.GetComponent<DragDropUI>().enabled = false;
            else this.gameObject.GetComponent<DragDropUI>().enabled = true;
        }

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


        StartCoroutine(DelayCard());

    }

    IEnumerator DelayCard()
    {

        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/PC_Start_ATK");

        yield return new WaitForSeconds(.5f);
        GameManager.instance.FMODManagerSystem.PlayEffectSound(cardData.Sound_Code);
        StartCoroutine(CardAction.StartAction(GameManager.instance.Player, this, this.cardData, EnemyTarget));
    }



    public void SetOutLineColor(Color color)
    {
        if (cardImage != null)
        {
            GetComponent<Image>().color = color;
        }

    }


    public void DisableCard()
    {

        SetOutLineColor(new Color(1, 1, 1, 0));
    }
}

