using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaGauge : MonoBehaviour
{
    [SerializeField] List<GameObject> ManaImage;
    int MaxMana;
    int CurrentMana;
    Color startColor;

    int Cost;
    // Start is called before the first frame update
    void Start()
    {
        if (ManaImage.Count == 0) return;
        MaxMana = ManaImage.Count;


        startColor = ManaImage[0].GetComponent<Image>().color;
        for (int i = 0; i < ManaImage.Count; i++)
        {
            ManaImage[i].SetActive(false);
        }

    }

    public void Initialize()
    {
         MaxMana =5;
         CurrentMana =5;

        for (int i = 0; i < CurrentMana; i++)
        {
            ManaImage[i].transform.parent.gameObject.SetActive(true);
        }
    }

    public void UseMana()
    {
        CurrentMana -= Cost;
        for (int i = 0; i < ManaImage.Count; i++)
        {
            ManaImage[i].transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i <CurrentMana; i++)
        {
            ManaImage[i].transform.parent.gameObject.SetActive(true);
        }
        
    }

    public void SetManaCost(int cost)
    {
        if (cost < 0)
        {
            for (int i = 0; i < ManaImage.Count; i++)
            {
                ManaImage[i].GetComponent<Image>().color = Color.red;
                ManaImage[i].SetActive(false);
            }
            return;
        }


        if (CurrentMana < cost)
        {
            for (int i = 0; i < CurrentMana; i++)
            {
                ManaImage[i].SetActive(true);
                ManaImage[i].GetComponent<Image>().color = Color.red;
            }
        }
        else
        {
            for (int i = 0; i < cost; i++)
            {
                ManaImage[i].SetActive(true);
                ManaImage[i].GetComponent<Image>().color = startColor;
            }
            for (int i = cost; i < ManaImage.Count; i++)
            {
                ManaImage[i].SetActive(false);
                ManaImage[i].GetComponent<Image>().color = startColor;
            }
        }

        Cost = cost;
    }
}
