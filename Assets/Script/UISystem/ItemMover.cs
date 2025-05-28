using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemMover : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] float FrequencyRate;
    [SerializeField] float MoveSpeed;
    [SerializeField] float SpinSpeed;
    [SerializeField] SlotUI TargetSlot;
    [SerializeField] PlayerCDSlotGroup PlayerCardSlotManager;

    public bool IsMove = true;
   [SerializeField] Transform ItemPos;
    SlotUI ItemSlotUI;
   // Animator Animator;

    UnityAction MoveEvent;
   [SerializeField] ImageSwap ImageSwap;

    public void AddMoveEvent(UnityAction moveEvent)
    { 
    MoveEvent += moveEvent; 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 1f;

        if (IsMove == false) return;

        if (ItemSlotUI == null)
        {
            ItemSlotUI = GetComponent<SlotUI>();
           
        }

        ItemPos = ItemSlotUI.ReadData<Transform>();
       // Animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        ImageSwap = ItemSlotUI.ReadData<ImageSwap>();
        //if (Animator != null)
        //{
        //    Animator.enabled = false;
        //}


        if (TargetSlot != null && ItemPos != null)
        {
            Debug.Log("Move");
            
            StartCoroutine("StartMoveUpdate");
        }
    }

    IEnumerator StartMoveUpdate()
    {
        MoveEvent?.Invoke();
        
       
        bool isPlay = true;
        while (isPlay)
        {
            ItemPos.position = Vector3.MoveTowards(ItemPos.position, TargetSlot.transform.position, MoveSpeed/100);
            ItemPos.Rotate(SpinSpeed, 0, 0);
            if (ItemPos.position == TargetSlot.transform.position)
            {
                isPlay = false;

                // Animator.enabled = true;

                ImageSwap.StartSwapImage();

                TargetSlot.InsertData(ItemPos.gameObject);
                PlayerCardSlotManager.SwapCardSlots();
                break;
            }

            yield return new WaitForSeconds(FrequencyRate);
        }

        yield return null;
    }

}
