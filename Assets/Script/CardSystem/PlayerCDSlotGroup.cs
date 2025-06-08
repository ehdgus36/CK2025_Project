using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;


public class PlayerCDSlotGroup : MonoBehaviour
{
    [SerializeField] GameObject[] Player_CardSlot;
    [SerializeField] Transform[] player;
    [SerializeField] List<Dack> PlayerDacks;
    int index; 
    private void Start()
    {
        for (int i = 0; i < Player_CardSlot.Length; i++)
        {
            Player_CardSlot[i].gameObject.SetActive(false);
        }

        if (PlayerDacks.Count == 0)
        {
            for (int i = 0; i < Player_CardSlot.Length; i++)
            {
                PlayerDacks.Add(Player_CardSlot[i].gameObject.GetComponent<Dack>());
                PlayerDacks[i].DrawFromDeck();
            }
        }


        Player_CardSlot[0].gameObject.SetActive(true);
        //Player_CardSlot[0].GetComponent<Animator>().Play("Drow");
       
    }

    public void PlayerTurnDrow()
    {
        if (PlayerDacks.Count == 0)
        {
            for (int i = 0; i < Player_CardSlot.Length; i++)
            {
                PlayerDacks.Add(Player_CardSlot[i].gameObject.GetComponent<Dack>());
                PlayerDacks[i].DrawFromDeck();
            }
        }



        for (int i = 0; i < Player_CardSlot.Length; i++)
        {
            Player_CardSlot[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < Player_CardSlot.Length; i++)
        {
            PlayerDacks[i].DrawFromDeck();
        }

        for (int i = 0; i < Player_CardSlot.Length; i++)
        {
            Player_CardSlot[i].gameObject.SetActive(false);
        }

        Player_CardSlot[0].gameObject.SetActive(true);
        //Player_CardSlot[0].GetComponent<Animator>().Play("Drow");
    }

    public void SwapCardSlots()
    {
        
    }

    public void ReturnCard()
    {
       // Player_CardSlot[0].GetComponent<Animator>().Play("CardReturn");

        
    }
    
}
