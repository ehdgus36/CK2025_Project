using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotGroup : MonoBehaviour
{ 
    [SerializeField] SlotUI[] Slots;

  

    // Start is called before the first frame update

    public SlotUI[] Getsloat() { Initialize(); return Slots;  }

 
    public virtual void Initialize()
    {
        if (Slots.Length == 0)
        {
            Slots = GetComponentsInChildren<SlotUI>();
            if (Slots.Length == 0)
            {
                Debug.LogError("Name:" + this.gameObject.name + "SlotGroup에 Slot이 없습니다 생성해 주세요");
                return;
            }
        }
    }

    public virtual void  Initialize(UnityAction<SlotUI> funtion)
    {
        if (Slots.Length == 0)
        {
            Slots = GetComponentsInChildren<SlotUI>();
            if (Slots.Length == 0)
            {
                Debug.LogError("Name:" + this.gameObject.name + "SlotGroup에 Slot이 없습니다 생성해 주세요");
                return;
            }
        }

        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].AddInsertEvent(funtion);
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
        List<RectTransform> RemoveObj = ReadData<RectTransform>(); // 삭제할 데이터 가져오기

        //Debug.Log("Destroy" + RemoveObj[0].name);
        for (int i = 0; i< RemoveObj.Count; i++)
        {
            
           // Destroy(RemoveObj[i].gameObject);
          
        }
    }

    
}
