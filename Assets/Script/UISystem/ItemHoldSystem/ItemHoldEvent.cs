using UnityEngine;
using UnityEngine.UI;

public class ItemHoldEvent : MonoBehaviour
{
    [SerializeField] Button ExitButton;
    [SerializeField] ItemInventorySystem InventorySystem;
    [SerializeField] ItemHoldSystem ItemHoldSystem;


    private void Start()
    {
        ExitButton.onClick.AddListener(ExitEvent);
    }

    void ExitEvent()
    {
        InventorySystem.SaveInventory();
        ItemHoldSystem.SaveData();
    }
}
