
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
    [HideInInspector] public int Buff_Recover_HP = 0 ;

    bool isCardEnd = false;
    Enemy EnemyTarget;
    public bool IsCardEnd { get { return isCardEnd; } }

    protected SlotGroup CardSloats;

    Vector3 PlayerStartPos;

    Dictionary<string, int> CardDataVariable = new Dictionary<string, int>();

    int DiscardCnt = 0; //어빌리티 버린카드 갯수

    public virtual void Initialized(SlotGroup slotGroup ) 
    {
        CardSloats = slotGroup;

        isCardEnd = false;
        object data = null;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(CardID, out data))
        {
            cardData = (CardData)data;
            Debug.Log("카드 데이터 입력완료");
        }
        else
        {
           Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다 "+ this.gameObject.name);
        }

        Debug.Log("수치 :" + cardData.Damage);

        //Range_Type Attack_Count    Damage  Status_Type Status_Turn Damage_Buff HP_Recover  HP_Loss Barrier_Get Barrier_Loss)
        //사용변수 초기화 
        CardDataVariable.Clear();


        CardDataVariable["Range_Type"] = cardData.Range_Type;
        CardDataVariable["Attack_Count"] = cardData.Attack_Count;
        CardDataVariable["Damage"] = cardData.Damage;
        CardDataVariable["Status_Type"] = cardData.Status_Type;
        CardDataVariable["Status_Turn"] = cardData.Status_Turn;
        CardDataVariable["Damage_Buff"] = cardData.Damage_Buff;
        CardDataVariable["HP_Recover"] = cardData.Recover_HP;
        CardDataVariable["HP_Loss"] = cardData.HP_Loss;
        CardDataVariable["Barrier_Get"] = cardData.Barrier_Get;
        CardDataVariable["Barrier_Loss"] = cardData.Barrier_Loss;

    }

    public virtual void Initialized(string cardID)
    {
        if (cardID == "" || cardID == null) return;
        object data = null;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(cardID, out data))
        {
            cardData = (CardData)data;
            Debug.Log("카드 데이터 입력완료");
        }
        else
        {
            Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다 " + this.gameObject.name);
        }

       

        string Path = "CardImage/" + cardData.Card_Im;
        Sprite cardSprite= Resources.Load<Sprite>(Path);
        
        Material instanceMaterial = Instantiate(BaseMaterial);
        instanceMaterial.SetTexture("_OverlayTex", cardSprite.texture);

        if (cardImage != null)
        {
            cardImage.material = instanceMaterial;
        }

        this.CardID = cardID;

        this.gameObject.name = cardData.Card_Name_EN;
    }

    public void AbilieySystem()
    {
        //삭제 예정
        
    }

  


    void AbilityFun(Unit unit)
    {
        bool IsStatus = false;

        //발동 조건을 검증 AbilityTarget_Status
        switch (cardData.AbilityTarget_Status)
        {
            case "0":
                IsStatus = true;
                    break;

            case "IsBarrierActive":
                if(GameManager.instance.Player.PlayerUnitData.CurrentBarrier > 0) 
                    IsStatus = true;
                break;

            case "IsBarrierNotActive":
                if (GameManager.instance.Player.PlayerUnitData.CurrentBarrier <= 0)
                    IsStatus = true;
                break;

            case "IsFullHP":
                if (GameManager.instance.Player.PlayerUnitData.CurrentHp == GameManager.instance.Player.PlayerUnitData.MaxHp)
                    IsStatus = true;
                break;

            case "IsNotFullHP":
                if (GameManager.instance.Player.PlayerUnitData.CurrentHp < GameManager.instance.Player.PlayerUnitData.MaxHp)
                    IsStatus = true;
                break;
        }


        if (IsStatus == true)
        {
            //이후 발동타입 검증 AbilityTarget_Type ? 이게 굳이 필요한가?
            switch (cardData.AbilityTarget_Type)
            {
                case "0":
                    return; // 값 참조 안함
                case "1":

                    break;
                case "2":
                    break;
                case "3":
                    break;
            }

            //추가효과 횟수 검증 AbilityGet_Count
           

            int value = 0;
            //이게 갯수를 셀때 이번 턴동안인지 카드가 발동된 시점부터 세는건지
            switch (cardData.AbilityGet_Count)
            {
                case "ManaUseCnt":
                    value = GameManager.instance.ExcutSelectCardSystem.UseManaCount;
                    break;
                case "DicordCnt":
                    value = DiscardCnt;
                    break;
                case "SuccessNoteCnt":
                    value = 1;
                    break;
                case "AttackDamageCnt": //이거 기준이 지금까지인지 
                    break;
            }

            //추가효과 타입 검증 AbilityGet1_Type
            //가중치 적용 AbilityGet1_Value1 ,AbilityGet1_Value2




            GameManager.instance.Player.AddBarrier(CardDataVariable["Barrier_Get"]);
            GameManager.instance.Player.LossBarrier(CardDataVariable["Barrier_Loss"]);

            switch (cardData.AbilityGet1_Type)
            {
                case "AttackCount":
                    CardDataVariable["Attack_Count"] = value;
                   
                    break;
                case "AttackDamage":
                    value = DiscardCnt;
                    break;
                case "HPRecover":
                    GameManager.instance.Player.addHP(value * int.Parse(cardData.AbilityGet1_Value1));
                    break;
                case "HPLoss": 
                    GameManager.instance.Player.LossHP(value * int.Parse(cardData.AbilityGet1_Value1));
                    break;
                case "BarrierGet": 

                    GameManager.instance.Player.AddBarrier(value * int.Parse( cardData.AbilityGet1_Value1));
                    break;
                case "BarrierLoss": 
                    GameManager.instance.Player.LossBarrier(value *  int.Parse(cardData.AbilityGet1_Value1));
                    break;

                case "BuffGroup": 
                    
                    break;

                case "CardChange":
                    GameManager.instance.PlayerCDSlotGroup.ChangeDackCard(cardData.AbilityGet1_Value1, cardData.AbilityGet1_Value2);

                    break;
                case "CardAdd": //이거 기준이 지금까지인지 
                    break;
            }
            
        }


    }

   

public virtual void TargetExcute(Enemy Target , Card nextCard = null)
    {

        //카드 디세이블 체크
        if (cardData.DiscordAllCard == 1)
        {
            Reroll_Card reroll = GetComponent<Reroll_Card>();
            if (reroll == null)
            {
                this.gameObject.AddComponent<Reroll_Card>();
                reroll = GetComponent<Reroll_Card>();
            }

            reroll.Excute(CardSloats, out DiscardCnt);
        }


        //어빌리티 체크

        if (cardData.Ability_Timing == "0")
        {

        }
        else
        {
            GameManager.instance.AbilitySystem.AddAvilityEvent(cardData.Ability_Timing, null); //일단 null
        }


        

        PlayerStartPos = GameManager.instance.Player.transform.position;// 플레이어 처음 위치 저장

        if (Target.isDie == true) //카드 넣기
        {         
            SetOutLineColor(Color.white);
            isCardEnd = true;
            return;
        }

        // 사용한 카드 묘지로 보내는 기능
        GameManager.instance.CardCemetery.Insert(this);


        AbilieySystem();



        if (cardData.MoveType == "M")
        {
            GameManager.instance.Player.transform.position = Target.gameObject.transform.position - new Vector3(6, 0, 0);
        } 


        if (nextCard != null) nextCard.DamageBuff = cardData.Damage_Buff; // 조건문 만족시 버프 추가
        Debug.Log("이번 공격 애니메이션에서 Slash 이벤트 감지!"); // 대충 데미지 넣는거 구현   

        EnemyTarget = Target;
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code,false ,AttackEvent , CompleteEvent); // 최종형
       // GameManager.instance.UIInputSetActive(false);
                                                                              
    }


    //스파인에서 AttackEvent가 발생할 때 실행할거
    public virtual void AttackEvent(TrackEntry entry, Spine.Event e)
    {

        if (e.Data.Name != "4bit") return;

        DamageBuff = GameManager.instance.ExcutSelectCardSystem.BuffDamage;

        Debug.Log("데미지 버프" + DamageBuff);
        for (int i = 0; i < CardDataVariable["Attack_Count"]; i++)
        {

            if (cardData.Range_Type == 1) // 단일 공격
            {
                EnemyTarget.TakeDamage(CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);

                if (cardData.Effect_Pos == "E") GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);

                if (cardData.Effect_Pos == "P") GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, GameManager.instance.Player.transform.position);



            }



            if (cardData.Range_Type == 2) // 전체 공격
            {
                if (cardData.Ani_Code == "notebomb_Ani") //  임시 로직 새로 생각하기
                {
                    //GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);
                    StartCoroutine(NoteBomb());
                }
                else
                {

                    List<Enemy> AttackEnemies = new List<Enemy>(GameManager.instance.EnemysGroup.Enemys);
                    GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);

                    for (int j = 0; j < AttackEnemies.Count; j++)
                    {
                        AttackEnemies[j].TakeDamage(CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);
                    }
                }
            }

            if (cardData.Range_Type == 3)//PlayerBuff
            {
                if (cardData.Effect_Pos == "E") GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);

                if (cardData.Effect_Pos == "P") GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, GameManager.instance.Player.transform.position);


                //EnemyTarget.TakeDamage(CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);
            }


            if (cardData.Range_Type == 4) // 랜덤 공격
            {
                List<Enemy> AttackEnemies = new List<Enemy>(GameManager.instance.EnemysGroup.Enemys);
                GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);
                AttackEnemies[Random.Range(0, AttackEnemies.Count)].TakeDamage(CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);
            }
        }
        Debug.Log("이번 공격 애니메이션에서 Slash 이벤트 감지!"); // 대충 데미지 넣는거 구현       
        GameManager.instance.ComboUpdate(Random.Range(17010, 21204));
        GameManager.instance.Player.addHP(CardDataVariable["HP_Recover"] + Buff_Recover_HP);
        GameManager.instance.Player.LossHP(CardDataVariable["HP_Loss"]);// 배리어 혹은 체력 까기

        GameManager.instance.Player.AddBarrier(CardDataVariable["Barrier_Get"]);
        GameManager.instance.Player.LossBarrier(CardDataVariable["Barrier_Loss"]);

        GameManager.instance.ExcutSelectCardSystem.BuffDamage += cardData.Damage_Buff;

        GameManager.instance.FMODManagerSystem.PlayEffectSound(cardData.Sound_Code);
    }

    // 애니메이션이 마무리 될때 할거
    public virtual void CompleteEvent(TrackEntry entry) 
    {
        isCardEnd=true;
         // 추가버프는 1회용이기 때문에 항상 초기화
        Buff_Recover_HP = 0;

        

        if (cardData.Ani_Code == "notebomb_Ani") // 공격이 끝날때 데미지
        {
            List<Enemy> AttackEnemies = new List<Enemy>(GameManager.instance.EnemysGroup.Enemys);
            GameManager.instance.Player.PlayerEffectSystem.PlayEffect(cardData.Effect_Code, EnemyTarget.transform.position);

            for (int i = 0; i < AttackEnemies.Count; i++)
            {
                AttackEnemies[i].TakeDamage(CardDataVariable["Damage"] + DamageBuff, cardData.CardBuff);
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
       
        GameObject controllObject =  GameManager.instance.Player.PlayerEffectSystem.EffectObject("notebomb_Effect_ball", EnemyTarget.transform.position);

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
            controllObject.transform.position = Bezier(GameManager.instance.Player.transform.position, EnemyTarget.transform.position + new Vector3(0,3,0), EnemyTarget.transform.position, T);
            T +=0.05f;
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
