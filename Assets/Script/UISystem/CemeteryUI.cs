using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CemeteryUI : MonoBehaviour,IDropHandler
{
    [SerializeField] GameObject game;
    public void OnDrop(PointerEventData eventData)
    {
        game = eventData.pointerDrag.gameObject;

        Destroy(eventData.pointerDrag.gameObject);

    }
}
