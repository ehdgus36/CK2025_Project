
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
        CardDataVariable["Damage"]= cardData.Damage;
        CardDataVariable["Status_Type"]= cardData.Status_Type;
        CardDataVariable["Status_Turn"]= cardData.Status_Turn;
        CardDataVariable["Damage_Buff"]= cardData.Damage_Buff;
        CardDataVariable["HP_Recover"]= cardData.Recover_HP;
        CardDataVariable["HP_Loss"]= cardData.HP_Loss;
        CardDataVariable["Barrier_Get"]= cardData.Barrier_Get;
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

        Debug.Log("수치 :" + cardData.Damage);

        string Path = "CardImage/" + cardData.Card_Im;
        Sprite cardSprite= Resources.Load<Sprite>(Path);
        
        Material instanceMaterial = Instantiate(BaseMaterial);
        instanceMaterial.SetTexture("_OverlayTex", cardSprite.texture);

        cardImage.material = instanceMaterial;

        this.CardID = cardID;
        this.gameObject.name = cardData.Card_Name_EN;
    }

    public void AbilieySystem()
    {
        //Ability_Con1
        if (GameManager.instance.ExcutSelectCardSystem.GetAbiltyCondition(cardData.Ability_Con1))
        {
            Debug.Log("조건1 통과");
            //Ability_Con2
            if (GameManager.instance.ExcutSelectCardSystem.GetAbiltyCondition(cardData.Ability_Con2))
            {
                Debug.Log("조건2 통과");
                //Ability_Act1
                AbilityActEvent(cardData.Ability_Act1);

                //Ability_Act2
                AbilityActEvent(cardData.Ability_Act2);
            }
        }
        
    }

    void AbilityActEvent(string Ability_Act)
    {
        if (Ability_Act == "0") return;


        Debug.Log("어빌리티 실행");
        Debug.Log(Ability_Act);
        if (Ability_Act[0] == 'F')
        {
            string[] Abilit = Ability_Act.Split('/');

            Debug.Log(Abilit[1]+":");
            DataTableFuntionCode(Abilit[1]);
        }
        if (Ability_Act[0] == 'C')
        {
            string[] Abilit = Ability_Act.Split('/');// C/칼럼=변수값 을 C,칼럼=변수값 ;
            Debug.Log(Abilit[1]);
            Abilit = Abilit[1].Split('='); // 칼럼=변수값 다시 칼럼,변수;
            CardDataVariable[Abilit[0]] = DataTableVariableCode(Abilit[1]);

            Debug.Log(CardDataVariable[Abilit[0]]+":" + DataTableVariableCode(Abilit[1]).ToString());
        }


        
        
    }

    public int DataTableVariableCode(string code)
    {
        Debug.Log("카드 능력 변수 능력 발동");
        int value = 0;
        switch (code)
        {
            case "CurBarrier":
                value = GameManager.instance.Player.PlayerUnitData.CurrentBarrier;
                break;

            case "DiscardCnt*3":
                value = DiscardCnt * 3; 
                break;

            case "ManaUseCnt":
                value = GameManager.instance.ExcutSelectCardSystem.UseManaCount;
                break;

            case "HitEnemyDmg":
                if (GameManager.instance.Player.AttackEnemy)
                {
                    value = GameManager.instance.Player.AttackEnemy.EnemyData.CurrentDamage;
                }
                break;

            case "Add2Damage":
                value = CardDataVariable["Damage"]+2;
                break;
                
        }

        return value;
    }

    public void DataTableFuntionCode(string code)
    {
        
        switch (code)
        {
            case "DiscardAllCards":
                Reroll_Card reroll = GetComponent<Reroll_Card>();
                if (reroll == null)
                {
                    this.gameObject.AddComponent<Reroll_Card>();
                    reroll = GetComponent<Reroll_Card>();
                }
                 
                reroll.Excute(CardSloats,out DiscardCnt);
                //카드 정리
                break;

            case "GetBarrier5":
                GameManager.instance.Player.PlayerUnitData.CurrentBarrier += 5;
                
                break;

            case "GetRecoverHP":
                GameManager.instance.Player.addHP(CardDataVariable["HP_Recover"]);
                GameManager.instance.Player.PlayerEffectSystem.PlayEffect("bresth_Effect",GameManager.instance.Player.gameObject.transform.position);

                break;


            case "AttackEnemy":
                if ( GameManager.instance.Player.AttackEnemy != null)
                {
                    GameManager.instance.Player.AttackEnemy.TakeDamage(CardDataVariable["Damage"]);
                    GameManager.instance.Player.PlayerEffectSystem.PlayEffect("codemiss_Effect", GameManager.instance.Player.AttackEnemy.gameObject.transform.position);
                }


                break;
                

        }
    }


    public virtual void TargetExcute(Enemy Target , Card nextCard = null)
    {
        AbilieySystem();

        PlayerStartPos = GameManager.instance.Player.transform.position;// 플레이어 처음 위치 저장

        if (Target.isDie == true) //카드 넣기
        {
            for (int i = 0; i < CardSloats.Getsloat().Length; i++)
            {
                if (CardSloats.Getsloat()[i].ReadData<Card>() == null)
                {
                    CardSloats.Getsloat()[i].InsertData(this.gameObject);
                }
            }
            isCardEnd = true;
            return;
        }

        

        if (cardData.MoveType == "M")
        {
            GameManager.instance.Player.transform.position = Target.gameObject.transform.position - new Vector3(6, 0, 0);
        } 


        if (nextCard != null) nextCard.DamageBuff = cardData.Damage_Buff; // 조건문 만족시 버프 추가
        Debug.Log("이번 공격 애니메이션에서 Slash 이벤트 감지!"); // 대충 데미지 넣는거 구현   

        EnemyTarget = Target;
        GameManager.instance.Player.PlayerAnimator.PlayAnimation(cardData.Ani_Code,false ,AttackEvent , CompleteEvent); // 최종형
        GameManager.instance.UIInputSetActive(false);
                                                                              
    }


    //스파인에서 AttackEvent가 발생할 때 실행할거
    public virtual void AttackEvent(TrackEntry entry, Spine.Event e)
    {
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

        GameManager.instance.UIInputSetActive(true);
        if (this.gameObject.activeInHierarchy == true)
        {
            StartCoroutine(PlayerReturnDelay());
        }

        GameManager.instance.UIInputSetActive(true);
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

    
}
