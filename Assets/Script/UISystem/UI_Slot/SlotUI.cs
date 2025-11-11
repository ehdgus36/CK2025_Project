using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.VisualScripting;

public class SlotUI : MonoBehaviour,IDropHandler
{
    
    GameObject Data;
    UnityAction<SlotUI> InsertDataEvent; // 슬롯에 데이터가 들어가면 실행되는 이벤트
    UnityAction<SlotUI> RemoveDataEvent;
    [SerializeField] Vector3 imageScale = new Vector3(1.0f,1.0f,1.0f);
    [SerializeField] GameObject InsertEffect;


    public void AddInsertEvent(UnityAction<SlotUI> funtion)
    {
        InsertDataEvent += funtion;
    }

    public void AddRemoveEvent(UnityAction<SlotUI> funtion)
    {
        RemoveDataEvent += funtion;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {          
        InsertData(eventData.pointerDrag);
        this.transform.localScale = new Vector3(.8f, .8f, .8f);
    }

    public virtual void InsertData(GameObject data)
    {
        if (transform.childCount != 0 || data == null) return;
        
        if (data.GetComponent<DragDropUI>())
        {
            data.GetComponent<DragDropUI>().startScale = imageScale;
            if (data.GetComponent<DragDropUI>()?.startParent != null)
                data.GetComponent<DragDropUI>().startParent.GetComponent<SlotUI>().RemoveSlotItem();
        }

        data.transform.position = transform.position;
        data.transform.rotation = transform.rotation;

        data.transform.SetParent(this.transform);
        data.transform.localScale = imageScale;

        Data = data;


        InsertDataEvent?.Invoke(this);

        if (InsertEffect != null)
        {
            InsertEffect.SetActive(false);
            InsertEffect.SetActive(true);
        }
    }



    public virtual void RemoveSlotItem()
    { 
        RemoveDataEvent?.Invoke(this);
    }

    public virtual T ReadData<T>()
    {
        if (this.transform.childCount == 0)
        {
            return default(T);
        }

        T obj = default(T);
        if (typeof(T) == typeof(GameObject))
        {
            if (this.transform.childCount > 0)
            {
                object objdata = this.transform.GetChild(0).gameObject;
                obj = (T)objdata;
            }
            if (this.transform.childCount == 0)
            {
                obj = default(T);
            }
        }
        else
        {
            obj = this.transform.GetChild(0).gameObject.GetComponent<T>();
        }

        if (obj == null)
        {
            return default(T);
        }

        return obj;
    }

}
