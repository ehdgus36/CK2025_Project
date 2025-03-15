using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CemeteryUI : MonoBehaviour,IDropHandler
{
    [SerializeField] GameObject game;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("나 작동해");

        Destroy(eventData.pointerDrag.gameObject);

    }
}
