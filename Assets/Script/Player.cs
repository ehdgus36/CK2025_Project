using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataSystem;

public class Player : Unit
{
    [SerializeField] Animator _PlayerAnimator;
    [SerializeField] PlayerCDSlotGroup CDSlotGroup;
   // [SerializeField] PlayerStatus playerStatus;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip[] audioClip;
    [SerializeField] ImageFontSystem fontSystem;


    public Animator PlayerAnimator { get { return _PlayerAnimator; } }
    public void Initialize()
    {
       
        UnitData.DataKey = GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA;


        if (!DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, out UnitData))
        {
            Debug.LogError("Player데이터를 가져오지 못함");
        }
       
        StartTurnEvent += GameManager.instance.PlayerAttackSystem.GuitarSetUp;
        StartTurnEvent += CDSlotGroup.PlayerTurnDrow;


        EndTurnEvent += GameManager.instance.PlayerAttackSystem.Return;


       

        DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, UnitData);
        
    }



 

    protected override void Die()
    {
        GameManager.instance.GameOver.SetActive(true);
    }

   

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("hit");

        
        if (_PlayerAnimator != null)
        {

            _PlayerAnimator.Play("hit");
        }
        AudioSource.PlayOneShot(audioClip[2]);
        GameManager.instance.Shake.PlayShake();

        //playerStatus.UpdataStatus(UnitData.MaxHp, UnitData.CurrentHp);
        DynamicGameDataSchema.UpdateDynamicDataBase(UnitData.DataKey, UnitData);
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

        fontSystem.FontConvert(damage.ToString(), null , fontColor);

    }

    public void PlayerSave()
    {
        PlayerPrefs.SetInt("PlayerHP", UnitData.CurrentHp);
    }

    public void PlayerAttackAnime()
    {
        _PlayerAnimator.Play("attack");
        AudioSource.PlayOneShot(audioClip[0]);
        AudioSource.PlayOneShot(audioClip[1]);
    }

    public void PlayerHamoniAttackAnime()
    {
        _PlayerAnimator.Play("hamoni");
        AudioSource.PlayOneShot(audioClip[0]);
        AudioSource.PlayOneShot(audioClip[1]);
    }

    public void PlayerCardAnime()
    {
        AudioSource.PlayOneShot(audioClip[3]);
        _PlayerAnimator.Play("card");
    }

    public void addHP(int HP)
    {
        UnitData.CurrentHp += HP;

        if (UnitData.CurrentHp > UnitData.MaxHp)
        {
            UnitData.CurrentHp = UnitData.MaxHp;
        }

       // playerStatus.UpdataStatus(UnitData.MaxHp, UnitData.CurrentHp);
    }
}
