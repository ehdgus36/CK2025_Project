using UnityEngine;
using System.Collections.Generic;


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
        Player_CardSlot[0].GetComponent<Animator>().Play("Drow");
       
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
        Player_CardSlot[0].GetComponent<Animator>().Play("Drow");
    }

    public void SwapCardSlots()
    {
        if (player[0].childCount >0)
        {
           
            Player_CardSlot[0].GetComponent<Animator>().Play("CardReturn");

            Player_CardSlot[1].gameObject.SetActive(true);
            Player_CardSlot[1].GetComponent<Animator>().Play("Drow");
        }

        if (player[1].childCount > 0)
        {
           
            Player_CardSlot[1].GetComponent<Animator>().Play("CardReturn");

            Player_CardSlot[2].gameObject.SetActive(true);
            Player_CardSlot[2].GetComponent<Animator>().Play("Drow");
        }

        if (player[2].childCount > 0)
        {
            Player_CardSlot[0].gameObject.SetActive(false);
            Player_CardSlot[1].gameObject.SetActive(false);
            Player_CardSlot[2].GetComponent<Animator>().Play("CardReturn");


        }
    }
}
