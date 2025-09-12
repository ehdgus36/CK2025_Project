using UnityEngine;

public class ItemInventorySystem : MonoBehaviour
{
    [SerializeField] protected SlotGroup _ItemSlotGroup;

    public SlotGroup ItemSlotGroup { get { return _ItemSlotGroup; } }
    


    private void OnEnable()
    {
        
    }


    public void SetUpItem()
    { 
    
    }
}
