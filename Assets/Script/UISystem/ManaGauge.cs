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

    public int GetCurrentMana() { return CurrentMana; }
    // Start is called before the first frame update
    void Start()
    {
        if (ManaImage.Count == 0) return;
        MaxMana = ManaImage.Count;
        CurrentMana = MaxMana;

        startColor = ManaImage[0].GetComponent<Image>().color;
        for (int i = 0; i < ManaImage.Count; i++)
        {
            ManaImage[i].SetActive(true);
        }

        //Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < CurrentMana; i++)
        {
            ManaImage[i].transform.parent.gameObject.SetActive(true);
        }

        for (int i = 0; i < ManaImage.Count; i++)
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
            for (int i = cost; i < ManaImage.Count; i++)
            {
                ManaImage[i].SetActive(true);
                ManaImage[i].GetComponent<Image>().color = startColor;
            }

            for (int i = 0; i < cost; i++)
            {
                ManaImage[i].SetActive(false);
                ManaImage[i].GetComponent<Image>().color = startColor;
            }
        }

        Cost = cost;
    }
}
