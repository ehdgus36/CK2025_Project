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

    public void SetManaCost(int cost)
    {
        if (cost < 0) return;
        if (MaxMana < cost)
        {
            for (int i = 0; i < ManaImage.Count; i++)
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
        }

        

    }
}
