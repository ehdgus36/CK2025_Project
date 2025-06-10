using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SlotUI : MonoBehaviour,IDropHandler
{
    
    GameObject Data;
    UnityAction InsertDataEvent; // 슬롯에 데이터가 들어가면 실행되는 이벤트
    [SerializeField] Vector3 imageScale;
    [SerializeField] GameObject InsertEffect;


    public void AddInsertEvent(UnityAction funtion)
    {
        InsertDataEvent += funtion;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {          
        InsertData(eventData.pointerDrag);
        this.transform.localScale = imageScale;
    }

    public virtual void InsertData(GameObject data)
    {
        if (transform.childCount != 0) return;
        if (data.GetComponent<DragDropUI>())
        {
            data.GetComponent<DragDropUI>().startScale = imageScale;
        }
       
        data.transform.position = transform.position;
        data.transform.rotation = transform.rotation;

        data.transform.SetParent(transform);
        data.transform.localScale = imageScale;

        

        InsertDataEvent?.Invoke();

        if (InsertEffect != null)
        {
            InsertEffect.SetActive(false);
            InsertEffect.SetActive(true);
        }
    }

    public virtual T ReadData<T>()
    {
        if (this.transform.childCount == 0)
        {
            return default(T);
        }
        T obj = this.transform.GetChild(0).gameObject.GetComponent<T>();

        if (obj == null)
        {
            return default(T);
        }

        return obj;
    }

}
