using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGroup : MonoBehaviour
{ 
    SlotUI[] Slots;

    // Start is called before the first frame update

    public SlotUI[] Getsloat() { return Slots;  }

    void Initialize()
    {
        Slots = GetComponentsInChildren<SlotUI>();
        if (Slots.Length == 0)
        {
            Debug.LogError("Name:" + this.gameObject.name + "SlotGroup에 Slot이 없습니다 생성해 주세요");
            return;
        }
    }

    void Start()
    {
        Initialize();
    }

   
    public List<GameObject> ReadData()
    {
        if (Slots == null) 
        {
            Initialize();
        }


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

    public void RemoveDataAll()
    {
        List<GameObject> RemoveObj = ReadData(); // 삭제할 데이터 가져오기
        for (int i = 0; i< RemoveObj.Count; i++)
        {
            Destroy(RemoveObj[i]);
        }
    }

    
}
