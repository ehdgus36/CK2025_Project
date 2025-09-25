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
    //나가기 이벤트
    //나가면서 현재 인벤토리 상태, 장착한 아이템 상황을 다이나믹 데이터 베이스로 전달
    //아이템관련 다이나믹 시스템 업데이트


    }
}
