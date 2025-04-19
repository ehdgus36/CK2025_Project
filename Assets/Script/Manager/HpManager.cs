using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpManager : MonoBehaviour
{
    [SerializeField] EnemyStatus EnemyStatusPrefab;
    [SerializeField] Vector3 HpbarOffset;
    [SerializeField] Enemy[] Units;
    [SerializeField] EnemyStatus[] EnemyStatuses;
    [SerializeField] Transform HpBarParent;


    public void Initialize()
    {
        if (HpBarParent == null) return;

        if (EnemyStatuses.Length != 0)
        {
            for (int i = 0; i < EnemyStatuses.Length; i++)
            {
                EnemyStatuses[i].gameObject.SetActive(false);
            }
        }
      

        Units = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        EnemyStatuses = new EnemyStatus[Units.Length];
        
        
        if (Units.Length == 0)
        {
            return;
        }

       
        for (int i = 0; i < Units.Length; i++)
        {
            EnemyStatus status = Instantiate(EnemyStatusPrefab.gameObject).GetComponent<EnemyStatus>();
            status.transform.SetParent(HpBarParent);
            status.transform.position = Camera.main.WorldToScreenPoint(Units[i].transform.position + HpbarOffset);

            //status.Initialize(Units[i].GetMaxHp(), Units[i].GetMaxSkillCount());

            EnemyStatuses[i] = status;
        }

        for (int i = 0; i < Units.Length; i++)
        {
            if (Units[i].GetMaxHp() == 0)
            {
                EnemyStatuses[i].gameObject.SetActive(false);
            }


            if (Units[i].gameObject == null || Units[i].gameObject.activeSelf == false)
            {
                EnemyStatuses[i].gameObject.SetActive(false);
            }

        }
    }

 
    public void UpdatHpbar()
    {
        if (Units.Length == 0) return;

        for (int i = 0; i < Units.Length; i++)
        {
              
            if (Units[i].gameObject.activeSelf == true || Units[i] !=null)
            {
                //EnemyStatuses[i].SetCurrentHp(Units[i].GetUnitCurrentHp());
                //EnemyStatuses[i].SetCurrentSkill(Units[i].GetCurrentSkillCount());
            }
        }
    }
}
