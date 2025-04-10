using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class ItemMover : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] float FrequencyRate;
    [SerializeField] float MoveSpeed;
    [SerializeField] float SpinSpeed;
    [SerializeField]Transform TargetPos;
    [SerializeField] PlayerCDSlotGroup aaa;
    Transform ItemPos;
    SlotUI ItemSlotUI;
    Animator Animator;
   

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ItemSlotUI == null)
        {
            ItemSlotUI = GetComponent<SlotUI>();
            ItemPos = ItemSlotUI.ReadData<Transform>();
            Animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
            if (Animator != null)
            {
                Animator.enabled = false;
            }
        }


        if (TargetPos != null && ItemPos != null)
        {
            Debug.Log("Move");
            StartCoroutine("StartMoveUpdate");
        }
    }

    IEnumerator StartMoveUpdate()
    {
        //Animator.Play("CD_Spin");

        ItemPos.transform.SetParent(TargetPos.transform);
        bool isPlay = true;
        while (isPlay)
        {
            ItemPos.position = Vector3.MoveTowards(ItemPos.position, TargetPos.position, MoveSpeed/100);
            ItemPos.Rotate(SpinSpeed, 0, 0);
            if (ItemPos.position == TargetPos.position)
            {
                isPlay = false;

                Animator.enabled = true;
                Animator.Play("CD_Insert");

                aaa.aaaa();
                break;
            }

            yield return new WaitForSeconds(FrequencyRate);
        }
    }

}
