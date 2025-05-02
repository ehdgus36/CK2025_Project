using UnityEngine;
using UnityEngine.Events;

public class MoverGroup : MonoBehaviour
{
    [SerializeField]ItemMover[] Mover;

    private void OnEnable()   
    {
        Initialize();
    }


    public void Initialize()
    {
        Mover= GetComponentsInChildren<ItemMover>();
        if (Mover.Length == 0)
        {
            Debug.LogError("Name:" + this.gameObject.name + "SlotGroup에 Slot이 없습니다 생성해 주세요");
            return;
        }
        for (int i = 0; i < Mover.Length; i++)
        {
            Mover[i].AddMoveEvent(funtion);
        }

        for (int i = 0; i < Mover.Length; i++)
        {
            Mover[i].IsMove = true;
        }
    }

    public void funtion()
    {
        for (int i = 0; i < Mover.Length; i++)
        {
            Mover[i].IsMove = false;
        }
    }
}
