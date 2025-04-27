using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataSystem;

public class Player : Unit
{
    [SerializeField] Animator DamageEffect;
    [SerializeField] PlayerCDSlotGroup CDSlotGroup;
   // [SerializeField] PlayerStatus playerStatus;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip[] audioClip;

  
    public void Initialize()
    {
        StartTurnEvent += PlayableSystemOn;
        StartTurnEvent += CDSlotGroup.PlayerTurnDrow;

        EndTurnEvent += PlayableSystemOff;


        if (PlayerPrefs.HasKey("PlayerHP") == false)
        {
            UnitData.CurrentHp = 10;
        }

        if (PlayerPrefs.HasKey("PlayerHP") == true)
        {
            UnitData.CurrentHp = 10; //PlayerPrefs.GetInt("PlayerHP");
            //UnitCurrentHp = UnitMaxHp;
        }

        DynamicGameDataSchema.AddDynamicDataBase(UnitData.DataKey, UnitData);
        //playerStatus.UpdataStatus(UnitData.MaxHp, UnitData.CurrentHp);
    }



    void PlayableSystemOn()
    {
        GameManager.instance.PlayerAttackSystem.gameObject.SetActive(true);

    }

    void PlayableSystemOff()
    {
        GameManager.instance.PlayerAttackSystem.gameObject.SetActive(false);
       
    }

    protected override void Die()
    {
        GameManager.instance.GameOver.SetActive(true);
    }

   

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("hit");
        if (DamageEffect != null)
        {

            DamageEffect.Play("hit");
        }
        AudioSource.PlayOneShot(audioClip[2]);

        //playerStatus.UpdataStatus(UnitData.MaxHp, UnitData.CurrentHp);
        //DynamicGameDataSchema.UpdateDynamicDataBase(UnitData.DataKey, UnitData);
    }

    public void PlayerSave()
    {
        PlayerPrefs.SetInt("PlayerHP", UnitData.CurrentHp);
    }

    public void PlayerAttackAnime()
    {
        DamageEffect.Play("attack");
        AudioSource.PlayOneShot(audioClip[0]);
        AudioSource.PlayOneShot(audioClip[1]);
    }

    public void PlayerCardAnime()
    {
        AudioSource.PlayOneShot(audioClip[3]);
        DamageEffect.Play("card");
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
