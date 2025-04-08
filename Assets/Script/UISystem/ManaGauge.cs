using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaGauge : MonoBehaviour
{
    [SerializeField] List<GameObject> ManaImage;
    [SerializeField] TextMeshProUGUI ManaCountText; 
    [SerializeField] int MaxMana;
    [SerializeField] int CurrentMana;
    Color startColor;

    [SerializeField] int Cost;

    public int GetCurrentMana() { return CurrentMana; }
    // Start is called before the first frame update

    private void OnEnable()
    {
        Initialize();
    }


    public void Initialize()
    {
        if (ManaImage.Count == 0) return;

        MaxMana = ManaImage.Count;
        CurrentMana = MaxMana;

        ManaCountText.text = CurrentMana.ToString();

        startColor = ManaImage[0].GetComponent<Image>().color;
        for (int i = 0; i < ManaImage.Count; i++)
        {
            ManaImage[i].SetActive(true);
        }


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
        if (CurrentMana - Cost < 0) return;

        CurrentMana -= Cost;
        Debug.Log("currentMana" + CurrentMana);
        for (int i = 0; i < ManaImage.Count; i++)
        {
            ManaImage[i].transform.gameObject.SetActive(false);
        }
        ManaCountText.text = CurrentMana.ToString();
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
           
            ManaCountText.text = "Over";
            return;
        }
        else
        {
            for (int i = (MaxMana - CurrentMana); i < ManaImage.Count; i++)
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

        ManaCountText.text = (CurrentMana - cost).ToString();
    }
}
