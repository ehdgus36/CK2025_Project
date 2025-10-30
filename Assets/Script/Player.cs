using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataSystem;
using UnityEngine.EventSystems;

public class Player : Unit, IPointerEnterHandler,IPointerExitHandler
{
   
    [SerializeField] PlayerCDSlotGroup CDSlotGroup;
    [SerializeField] ImageFontSystem fontSystem;
    [SerializeField] UnitAnimationSystem AnimationSystem;
   
    [SerializeField] GameObject TurnEnd;

    [SerializeField] EffectSystem _PlayerEffectSystem;
    [SerializeField]Vector3 StartPos;

    public Enemy AttackEnemy { get; private set; } //나를 공격한 enemy

    Vector3 StartPlayerPos;
    public EffectSystem PlayerEffectSystem { get { return _PlayerEffectSystem; } }
    public UnitAnimationSystem PlayerAnimator { get { return AnimationSystem; } }

    public UnitData PlayerUnitData { get { return UnitData; } }

    //노트 버프



    public void MaxButtonDisable()    
    {
        
    }

    public void MaxButtonEnable()
    {
        
    }

    public void Initialize()
    {
        UnitData.DataKey = GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA;


        StartPlayerPos = this.transform.position;
        
        if (!DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, out UnitData))
        {
            Debug.LogError("Player데이터를 가져오지 못함");
        }       
       
        StartTurnEvent += () => {
            CDSlotGroup.PlayerTurnDrow();
           
            TurnEnd.SetActive(true);

            AttackEnemy = null;

            PlayerUnitData.CurrentBarrier = 0;
            GameManager.instance.ExcutSelectCardSystem.StartTurnRest();
            GameManager.instance.UIManager.ManaUI.gameObject.SetActive(true);
            DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
            GameManager.instance.UIInputSetActive(true);

            GameManager.instance.EnemysGroup.EnemyUIAllUpdata();
        };

       

        EndTurnEvent += () => {
            HellfireAction.EndHellFire();

            CDSlotGroup.ReturnCard();
            GameManager.instance.PlayerCardCastPlace.Reset();
            GameManager.instance.ExcutSelectCardSystem.Reset();
          
            TurnEnd.SetActive(false);
            GameManager.instance.UIManager.ManaUI.gameObject.SetActive(false);
            GameManager.instance.UIInputSetActive(false);
            GameManager.instance.EnemysGroup.EnemyUIAllUpdata();
            DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);

            StaticGameDataSchema.CARD_DATA_BASE.ResetTable();
        };

        DieEvent += PlayerDieEvent;
        UnitData.MaxHp = GameDataSystem.StaticGameDataSchema.StartPlayerData.MaxHp +GameManager.instance.ItemDataLoader.PCMaxHP_UP;

        HellfireAction.EndHellFire();

        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
    }

    void PlayerDieEvent()
    {
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/PC/PC_Die");
        GameManager.instance.GameFail();
    }

    protected override void TakeDamageEvent(Unit form, int damage, int resultDamage, Buff buff = null)
    {       
        AnimationSystem?.PlayAnimation("hit");
        
        //카메라 효과 , 사운드 , 이펙트효과
        GameManager.instance.Shake.PlayShake();
        GameManager.instance.PostProcessingSystem.ChangeVolume("Player_Hit", true , 0.2f, 0.0f , 0.2f);
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/PC/PC_Hurt");

        _PlayerEffectSystem.PlayEffect("Hit_Effect", this.transform.position);
        
        //UI 갱신
        DynamicGameDataSchema.UpdateDynamicDataBase(UnitData.DataKey, UnitData);

        fontSystem.FontConvert(damage.ToString());

        
        GameManager.instance.AbilitySystem.PlayeEvent(AbilitySystem.KEY_IS_PLAYER_HIT, this);
    }

    

    public void PlayerSave()
    {
        PlayerPrefs.SetInt("PlayerHP", UnitData.CurrentHp);
    }


    public void PlayerCardAnime()
    {
        //AnimationSystem.PlayAnimation("card");
    }

    public void addHP(int HP)
    {
        Debug.Log("리듬게임 회복" + HP);
        UnitData.CurrentHp += HP;
        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);   
    }

    public void LossHP(int HP)
    {
        UnitData.CurrentHp -= HP;

        if (UnitData.CurrentHp <= 0)
            TakeDamage(this,1);
        
        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
    }


    public void AddBarrier(int Barrie)
    {
        UnitData.CurrentBarrier += Barrie;

        if (UnitData.CurrentBarrier <= 0 )
        {
            UnitData.CurrentBarrier = 0;
        }

        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
    }

    public void LossBarrier(int Barrie)
    {
        UnitData.CurrentBarrier -= Barrie;

        if (UnitData.CurrentBarrier <= 0)
        {
            UnitData.CurrentBarrier = 0;
        }

        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
    }

    public override void AddBuff(Buff buff)
    {
        base.AddBuff(buff);

        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
    }
    public void ReturnPlayer()
    {
        this.transform.position = StartPlayerPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.instance.ExcutSelectCardSystem.SetTargetPlayer(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.instance.ExcutSelectCardSystem.SetTargetPlayer(null);
    }
}
