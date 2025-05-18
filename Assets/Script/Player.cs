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
