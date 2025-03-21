using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    
    private void Awake()
    {
        StartTurnEvent += PlayableSystemOn;
        StartTurnEvent += DackDrow;

        EndTurnEvent += PlayableSystemOff;
        EndTurnEvent += DackCordReturn;
    }

    

    void DackDrow()
    {
        GameManager.instance.PlayerCardDrow();
        Debug.Log("카드 드로우");
    }

    void DackCordReturn()
    {
        GameManager.instance.PlayerCardReturn();
    }
    void PlayableSystemOn()
    {
        GameManager.instance.GetPlayerAttackSystem().gameObject.SetActive(true);
        GameManager.instance.GetTurnButton().gameObject.SetActive(true);
    }

    void PlayableSystemOff()
    {
        GameManager.instance.GetPlayerAttackSystem().gameObject.SetActive(false);
        GameManager.instance.GetTurnButton().gameObject.SetActive(false);
    }
}
