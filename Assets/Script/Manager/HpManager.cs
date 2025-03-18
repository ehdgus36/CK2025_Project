using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    [SerializeField] GameObject HpBarPrefab;
    [SerializeField] Vector3 HpbarOffset;
    Unit[] Units;
    Image[] HpFills;

    
    private void Initialize()
    {
        Units = FindObjectsByType<Unit>(FindObjectsSortMode.None);
        HpFills = new Image[Units.Length];

        Transform HpBarParent = FindFirstObjectByType<Canvas>().transform; 
        for (int i = 0; i < Units.Length; i++)
        {
            GameObject Hpbar = Instantiate(HpBarPrefab);
            Hpbar.transform.SetParent(HpBarParent);
            Hpbar.transform.position = Camera.main.WorldToScreenPoint(Units[i].transform.position + HpbarOffset);
            if (Hpbar.transform.GetChild(0).GetComponent<Image>())
            {
                HpFills[i] = Hpbar.transform.GetChild(0).GetComponent<Image>();
            }
        }

        for (int i = 0; i < Units.Length; i++)
        {
            if (Units[i].GetMaxHp() == 0)
            {
                HpFills[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdatHpbar();
    }

    void UpdatHpbar()
    {
        for (int i = 0; i < Units.Length; i++)
        {
            HpFills[i].fillAmount = (float)Units[i].GetUnitCurrentHp() / (float)Units[i].GetMaxHp();
        }
    }
}
