using UnityEngine;
using UnityEngine.EventSystems;

public class StrapSlot : SlotUI
{

    public override void OnDrop(PointerEventData eventData)
    {
        InsertData(eventData.pointerDrag);
    }

    public override void InsertData(GameObject data)
    {
        if (data.GetComponent<StrapItem>())
        {
            base.InsertData(data);
        }
    }

    public override T ReadData<T>()
    {
        return base.ReadData<T>();
    }
}
