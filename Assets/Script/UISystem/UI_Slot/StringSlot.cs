using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class StringSlot : SlotUI
{
    [SerializeField] SlotGroup StringInventory;
    public override void OnDrop(PointerEventData eventData)
    {
        if (StringInventory == null) Debug.LogError(gameObject.name + "오브젝트에 StringInventory 설정이 안돼어 있습니다.");
        InsertData(eventData.pointerDrag);
    }

    public override void InsertData(GameObject data)
    {
        if (data.GetComponent<StringItem>())
        {
            RuntimeManager.PlayOneShot("event:/UI/Item_Stage/Item_Set");

            //만약 자리에 타입이 같은 새로운 아이템이 들어오면 
            //현재 아이템을 비어있는 인벤토리로 보내고 새로운 아이템을 넣음
            GameObject obj = ReadData<GameObject>();
            StringInventory.InsertData(obj);
            base.InsertData(data); 
        }
    }

    public override T ReadData<T>()
    {
        return base.ReadData<T>();
    }
}
