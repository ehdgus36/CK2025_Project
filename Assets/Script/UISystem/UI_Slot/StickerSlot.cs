using UnityEngine;
using UnityEngine.EventSystems;

public class StickerSlot : SlotUI
{

    public override void OnDrop(PointerEventData eventData)
    {

    }

    public override void InsertData(GameObject data)
    {
        if (data.GetComponent<StickerItem>())
        {
            base.InsertData(data);
        }
    }

    public override T ReadData<T>()
    {       
        return base.ReadData<T>();
    }
}
