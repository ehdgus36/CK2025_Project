using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour,IDropHandler
{
    GameObject Data;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;
        


        InsertData(eventData.pointerDrag);
        Debug.Log("onDrop");
    }

    public void InsertData(GameObject data)
    {
        
        data.transform.SetParent(transform);
        data.transform.position = transform.position;
       
    }

}
