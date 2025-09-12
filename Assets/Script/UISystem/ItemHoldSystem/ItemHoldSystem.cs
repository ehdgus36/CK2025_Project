using UnityEngine;

public class ItemHoldSystem : MonoBehaviour
{
    [SerializeField] SlotGroup HoldSlotGroup;
    [SerializeField] ItemInventorySystem[] InventorySystem;


    public void Start()
    {
        
    }


    void HoldSlotInsertEvent()
    {
        for (int i = 0; i < InventorySystem.Length; i++)
        {
            InventorySystem[i].SetUpItem();
        }
    }
}
