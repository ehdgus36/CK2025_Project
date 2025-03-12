using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGroup : MonoBehaviour
{ 
    SlotUI[] Slots;

    // Start is called before the first frame update
    void Start()
    {
        Slots = GetComponentsInChildren<SlotUI>();
        if (Slots.Length == 0)
        {
            Debug.LogError("Name:" + this.gameObject.name + "SlotGroup에 Slot이 없습니다 생성해 주세요"); 
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<GameObject> ReadData()
    {
        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].gameObject.transform.childCount == 1)
            {
                objs.Add(Slots[i].gameObject.transform.GetChild(0).gameObject);
            }
        }

       
        return objs;
    }
}
