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
        Debug.Log("onDrop");
        Data = eventData.pointerDrag;
        Data.transform.SetParent(transform);
        Data.transform.position = transform.position;
       
    }



}
