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
    [SerializeField] GameObject Combo;
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
        Combo.GetComponent<ComboUIView>().DisableButton();
    }

    public void MaxButtonEnable()
    {
        Combo.GetComponent<ComboUIView>().EnableButton();
    }

    public void Initialize()
    {
        UnitData.DataKey = GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA;


        StartPlayerPos = this.transform.position;
        //StartPos = Combo.GetComponent<RectTransform>().anchoredPosition;
        if (!DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, out UnitData))
        {
            Debug.LogError("Player데이터를 가져오지 못함");
        }       
       
        StartTurnEvent += CDSlotGroup.PlayerTurnDrow;
        StartTurnEvent += () => {
            Combo.GetComponent<RectTransform>().anchoredPosition = StartPos; 
            Combo.transform.localScale = new Vector3(1, 1, 1);
            Combo.GetComponent<ComboUIView>().EnableButton();
            TurnEnd.SetActive(true);

            AttackEnemy = null;

            PlayerUnitData.CurrentBarrier = 0;
            GameManager.instance.ExcutSelectCardSystem.StartTurnRest();
            GameManager.instance.UIManager.ManaUI.gameObject.SetActive(true);
            DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
            GameManager.instance.UIInputSetActive(true);
        };

        EndTurnEvent += CDSlotGroup.ReturnCard;
        EndTurnEvent += GameManager.instance.PlayerCardCastPlace.Reset;
        EndTurnEvent += GameManager.instance.ExcutSelectCardSystem.Reset; ;

        EndTurnEvent += () => {
            Combo.GetComponent<RectTransform>().anchoredPosition = new Vector3(659, 94, 0);
            Combo.GetComponent<RectTransform>().transform.localScale = new Vector3(2, 2, 2);
            Combo.GetComponent<ComboUIView>().DisableButton();
            TurnEnd.SetActive(false);
            GameManager.instance.UIManager.ManaUI.gameObject.SetActive(false);
            GameManager.instance.UIInputSetActive(false);
        };

        UnitData.MaxHp = GameDataSystem.StaticGameDataSchema.StartPlayerData.MaxHp +GameManager.instance.ItemDataLoader.PCMaxHP_UP;


        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
    }

    protected override void Die()
    {
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Player_CH/Player_Die");
        GameManager.instance.GameFail();
    }

    public void TakeDamage(int damage, Enemy enemy)
    {
        AttackEnemy = enemy;
        TakeDamage(damage);
    }



    public override void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        int resultDamage = damage;

        if (UnitData.CurrentBarrier > 0)
        {
            UnitData.CurrentBarrier -= resultDamage;

            if (UnitData.CurrentBarrier >= 0)
            {
                resultDamage = 0;
            }

            if (UnitData.CurrentBarrier < 0)
            {
                resultDamage = -UnitData.CurrentBarrier;
                UnitData.CurrentBarrier = 0;
            }
        }




        base.TakeDamage(resultDamage);
              
        AnimationSystem?.PlayAnimation("hit");
        
        //카메라 효과 , 사운드 , 이펙트효과
        GameManager.instance.Shake.PlayShake();
        GameManager.instance.PostProcessingSystem.ChangeVolume("Player_Hit", true , 0.2f, 0.0f , 0.2f);
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Player_CH/Player_Hurt");

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
        UnitData.CurrentHp += HP;

        if (UnitData.CurrentHp > UnitData.MaxHp)
        {
            UnitData.CurrentHp = UnitData.MaxHp;
        }

        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
       // playerStatus.UpdataStatus(UnitData.MaxHp, UnitData.CurrentHp);
    }

    public void LossHP(int HP)
    {
        UnitData.CurrentHp -= HP;

        if (UnitData.CurrentHp <= 0)
        {
            GameManager.instance.Player.TakeDamage(1);
        }

        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
        // playerStatus.UpdataStatus(UnitData.MaxHp, UnitData.CurrentHp);
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
