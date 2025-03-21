using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    [SerializeField] GameObject HpBarPrefab;
    [SerializeField] Vector3 HpbarOffset;
    [SerializeField]Unit[] Units;
    [SerializeField]Image[] HpFills;
    [SerializeField] Transform HpBarParent;


    public void Initialize()
    {
        if (HpBarParent == null) return;

        if (HpFills.Length != 0)
        {
            for (int i = 0; i < HpFills.Length; i++)
            {
                Destroy(HpFills[i].transform.parent.gameObject);
            }
        }
        Units = FindObjectsByType<Unit>(FindObjectsSortMode.None);
        HpFills = new Image[Units.Length];

       
        for (int i = 0; i < Units.Length; i++)
        {
            GameObject Hpbar = Instantiate(HpBarPrefab);
            Hpbar.transform.SetParent(HpBarParent);
            Hpbar.transform.position = Camera.main.WorldToScreenPoint(Units[i].transform.position + HpbarOffset);
            if (Hpbar.transform.GetChild(0).GetComponent<Image>())
            {
                HpFills[i] = Hpbar.transform.GetChild(0).GetComponent<Image>();
                HpFills[i].fillAmount = 1;
            }
        }

        for (int i = 0; i < Units.Length; i++)
        {
            if (Units[i].GetMaxHp() == 0)
            {
                HpFills[i].transform.parent.gameObject.SetActive(false);
            }


            if (Units[i].gameObject == null || Units[i].gameObject.activeSelf == false )
            {
                HpFills[i].transform.parent.gameObject.SetActive(false);
            }

        }
    }

   

    private void Update()
    {
        UpdatHpbar();
    }

    void UpdatHpbar()
    {
        for (int i = 0; i < Units.Length; i++)
        {
            if (Units[i] == null || Units[i].gameObject.activeSelf == false)
            {
                HpFills[i].transform.parent.gameObject.SetActive(false);

                continue;
            }

            HpFills[i].fillAmount = (float)Units[i].GetUnitCurrentHp() / (float)Units[i].GetMaxHp();

            
        }
    }
}
