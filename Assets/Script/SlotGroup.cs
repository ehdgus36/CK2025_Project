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

   
    public  List<T> ReadData<T>()
    {
        if (Slots == null) 
        {
            Initialize();
        }


        List<T> objs = new List<T>();
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].gameObject.transform.childCount == 1)
            {

                T obj = Slots[i].gameObject.transform.GetChild(0).gameObject.GetComponent<T>();
                if (obj != null)
                {
                    objs.Add(obj);
                }
            }
        }

       
        return objs;
    }

    public void RemoveDataAll()
    {
        List<GameObject> RemoveObj = ReadData<GameObject>(); // 삭제할 데이터 가져오기
        for (int i = 0; i< RemoveObj.Count; i++)
        {
            Destroy(RemoveObj[i]);
        }
    }

    
}
