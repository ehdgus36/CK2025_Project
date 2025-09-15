using UnityEngine;

public class ItemHoldSystem : MonoBehaviour
{
    [SerializeField] SlotGroup HoldSlotGroup;
    [SerializeField] ItemInventorySystem[] InventorySystem;


    public void Start()
    {
        for (int i = 0; i < HoldSlotGroup.Getsloat().Length; i++)
        {
            HoldSlotGroup.Getsloat()[i].AddInsertEvent(HoldSlotInsertEvent);   
        }
    }


    void HoldSlotInsertEvent()
    {
        for (int i = 0; i < InventorySystem.Length; i++)
        {
            InventorySystem[i].SetUpItem();
        }
    }
}
