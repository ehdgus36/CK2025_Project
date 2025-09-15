using UnityEngine;
using UnityEngine.EventSystems;

public class StringSlot : SlotUI
{
    public override void OnDrop(PointerEventData eventData)
    {
       
    }

    public override void InsertData(GameObject data)
    {
        if (data.GetComponent<StringItem>())
        { 
        base.InsertData(data); 
        }
    }

    public override T ReadData<T>()
    {
        return base.ReadData<T>();
    }
}
