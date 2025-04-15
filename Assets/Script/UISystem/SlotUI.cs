using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour,IDropHandler
{
    
    GameObject Data;
    [SerializeField] Vector3 imageScale;
   
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;
        
        InsertData(eventData.pointerDrag);
        Debug.Log("onDrop");
    }

    public virtual void InsertData(GameObject data)
    {

        if (data.GetComponent<DragDropUI>())
        {
            data.GetComponent<DragDropUI>().startScale = imageScale;
        }
       
        data.transform.position = transform.position;
        data.transform.rotation = transform.rotation;
        data.transform.localScale = imageScale;

        data.transform.SetParent(transform);
    }

    public virtual T ReadData<T>()
    {
        T obj = this.transform.GetChild(0).gameObject.GetComponent<T>();

        if (obj == null)
        {
            return default(T);
        }

        return obj;
    }

}
