using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickerSlot : SlotUI
{

    public override void OnDrop(PointerEventData eventData)
    {
        InsertData(eventData.pointerDrag);
    }

    public override void InsertData(GameObject data)
    {
        if (data.GetComponent<StickerItem>())
        {
            RuntimeManager.PlayOneShot("event:/UI/Item_Stage/Item_Set");
            base.InsertData(data);

        }
    }

    public override T ReadData<T>()
    {       
        return base.ReadData<T>();
    }
}
