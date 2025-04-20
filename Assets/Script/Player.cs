using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] Animator DamageEffect;
    [SerializeField] PlayerCDSlotGroup CDSlotGroup;
    [SerializeField] PlayerStatus playerStatus;

    private void Awake()
    {
        StartTurnEvent += PlayableSystemOn;
        StartTurnEvent += CDSlotGroup.PlayerTurnDrow;

        EndTurnEvent += PlayableSystemOff;
        EndTurnEvent += DackCordReturn;


        if (PlayerPrefs.HasKey("PlayerHP") == false)
        {
            UnitCurrentHp = UnitMaxHp;
        }

        if (PlayerPrefs.HasKey("PlayerHP") == true)
        {
            UnitCurrentHp = PlayerPrefs.GetInt("PlayerHP");
        }

        playerStatus.UpdataStatus(UnitMaxHp, UnitCurrentHp);
    }



    void DackCordReturn()
    {
        // GameManager.instance.PlayerCardReturn();
    }
    void PlayableSystemOn()
    {
        GameManager.instance.GetPlayerAttackSystem().gameObject.SetActive(true);

        //GameManager.instance.GetTurnButton().gameObject.SetActive(true);
    }

    void PlayableSystemOff()
    {
        GameManager.instance.GetPlayerAttackSystem().gameObject.SetActive(false);
        //GameManager.instance.GetTurnButton().gameObject.SetActive(false);
    }

    protected override void Die()
    {
        GameManager.instance.GameOver.SetActive(true);
    }

    public override void TakeDamage(AttackData data)
    {
        base.TakeDamage(data);
        Debug.Log("hit");
        if (DamageEffect != null)
        {
            DamageEffect.Play("hit");
        }

        playerStatus.UpdataStatus(UnitMaxHp, UnitCurrentHp);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("hit");
        if (DamageEffect != null)
        {
            DamageEffect.Play("hit");
        }

        playerStatus.UpdataStatus(UnitMaxHp, UnitCurrentHp);
    }

    public void PlayerSave()
    {
        PlayerPrefs.SetInt("PlayerHP", UnitCurrentHp);
    }
}
