using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataSystem;

public class Player : Unit
{
   
    [SerializeField] PlayerCDSlotGroup CDSlotGroup;
    [SerializeField] ImageFontSystem fontSystem;
    [SerializeField] UnitAnimationSystem AnimationSystem;
    [SerializeField] GameObject Combo;
    [SerializeField] GameObject TurnEnd;

    [SerializeField] EffectSystem _PlayerEffectSystem;
    Vector3 StartPos;

    Vector3 StartPlayerPos;
    public EffectSystem PlayerEffectSystem { get { return _PlayerEffectSystem; } }
    public UnitAnimationSystem PlayerAnimator { get { return AnimationSystem; } }


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
        StartPos = Combo.transform.position;
        if (!DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, out UnitData))
        {
            Debug.LogError("Player데이터를 가져오지 못함");
        }       
       
        StartTurnEvent += CDSlotGroup.PlayerTurnDrow;
        StartTurnEvent += () => { Combo.transform.position = StartPos; Combo.transform.localScale = new Vector3(1, 1, 1);
            Combo.GetComponent<ComboUIView>().EnableButton();
            TurnEnd.SetActive(true);
        };

        EndTurnEvent += CDSlotGroup.ReturnCard;
        EndTurnEvent += GameManager.instance.PlayerCardCastPlace.Reset;
        
        EndTurnEvent += () => {
            Combo.GetComponent<RectTransform>().anchoredPosition = new Vector3(70, -271, 0);
            Combo.GetComponent<RectTransform>().transform.localScale = new Vector3(2, 2, 2);
            Combo.GetComponent<ComboUIView>().DisableButton();
            TurnEnd.SetActive(false);
        };

        UnitData.MaxHp = GameDataSystem.StaticGameDataSchema.StartPlayerData.MaxHp +GameManager.instance.ItemDataLoader.PCMaxHP_UP;


        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
    }

    protected override void Die()
    {
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Player_CH/Player_Die");
        GameManager.instance.GameFail();
    }

   

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
              
        AnimationSystem?.PlayAnimation("hit");
        
        //카메라 효과 , 사운드 , 이펙트효과
        GameManager.instance.Shake.PlayShake();
        GameManager.instance.PostProcessingSystem.ChangeVolume("Player_Hit", true , 0.2f, 0.0f , 0.2f);
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Player_CH/Player_Hurt");
        
        //UI 갱신
        DynamicGameDataSchema.UpdateDynamicDataBase(UnitData.DataKey, UnitData);

        fontSystem.FontConvert(damage.ToString());
    }

    public void TakeDamage(int damage , string notes)
    {
        TakeDamage(damage);

        Color fontColor = new Color(239f / 255f, 86.0f / 255.0f, 110f / 225f);

        switch (notes)
        {
            case "Miss":
                fontColor = new Color(239f/255f, 86f/ 255f, 110/255f);
                break;
            case "Bad":
                fontColor = new Color(46f / 255f, 223f / 255f, 210f / 255f);
                break;
            case "Normal":
                fontColor = new Color(46f / 255f, 223f / 255f, 210f / 255f);
                break;
            case "Good":
                fontColor = new Color(254f / 255f, 249f / 255f, 57f/255f);
                break;
        }

        

    }

    public void PlayerSave()
    {
        PlayerPrefs.SetInt("PlayerHP", UnitData.CurrentHp);
    }

    public void PlayerAttackAnime()
    {
        AnimationSystem.PlayAnimation("attack");
    }
       

    public void PlayerHamoniAttackAnime()
    {
        AnimationSystem.PlayAnimation("hamoni");
       
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


    public void ReturnPlayer()
    {
        this.transform.position = StartPlayerPos;
    }
}
