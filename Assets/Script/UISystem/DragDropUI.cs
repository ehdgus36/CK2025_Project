using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler , IPointerEnterHandler , IPointerExitHandler
{




    [SerializeField] Transform onDragParent;


    public Transform startParent;

    [SerializeField] Vector3 startScale;

    [SerializeField] Transform PointerEnterParent;

    int index;
    bool onDrage = false;

    void Start()
    {
        startScale = transform.localScale; // 일단 야매
       
    }

    // 인터페이스 IBeginDragHandler를 상속 받았을 때 구현해야하는 콜백함수
    public void OnBeginDrag(PointerEventData eventData)
    {

        transform.parent.transform.SetSiblingIndex(index); // 0407 수정 드래그 시작하면 부모 원상복구
        //transform.SetSiblingIndex(index);
        onDragParent = GameObject.Find("Filds").gameObject.transform;

        // 백업용 포지션과 부모 트랜스폼을 백업 해둔다.
        


        startParent = transform.parent;
       
    
        // 드래그 시작할때 부모transform을 변경
        transform.SetParent(onDragParent);

       
    }

    // 인터페이스 IDragHandler 상속 받았을 때 구현 해야하는 콜백함수
    public void OnDrag(PointerEventData eventData)
    {
        //드래그중에는 Icon을 마우스나 터치된 포인트의 위치로 이동시킨다.
        transform.position = Input.mousePosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        transform.localScale = startScale;
    
    }

    // 인터페이스 IEndDragHandler 상속 받았을 때 구현 해야하는 콜백함수
    public void OnEndDrag(PointerEventData eventData)
    {

        if (transform.parent == onDragParent)
        {
            transform.position = startParent.position;
            transform.SetParent(startParent);
            transform.rotation = startParent.rotation;
        }

    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onDrage == true) return;

        if (PointerEnterParent == null)
        {
            PointerEnterParent = GameObject.Find("PointerEnterFild").gameObject.transform; 
        }

        transform.localScale = startScale * 1.2f;


       

        index = transform.parent.GetSiblingIndex();
        transform.parent.transform.SetAsLastSibling();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = startScale;

        transform.parent.transform.SetSiblingIndex(index);

    }
}
