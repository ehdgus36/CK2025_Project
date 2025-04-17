using UnityEngine;

public class PlayerCDSlotGroup : MonoBehaviour
{
    [SerializeField]GameObject[] Player_CardSlot;
    [SerializeField] Transform[] player;
    int index;
    private void Start()
    {
        for (int i = 0; i < Player_CardSlot.Length; i++)
        {
            Player_CardSlot[i].gameObject.SetActive(false);
        }
        Player_CardSlot[0].gameObject.SetActive(true);
        Player_CardSlot[0].GetComponent<Animator>().Play("Drow");
        index = player[0].childCount;
    }

    public void aaaa()
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
