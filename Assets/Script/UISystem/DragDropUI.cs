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

    bool onPointer = false;


    void Start()
    {
        startScale = transform.localScale; // 일단 야매
        startParent = transform.parent;
    }

    // 인터페이스 IBeginDragHandler를 상속 받았을 때 구현해야하는 콜백함수
    public void OnBeginDrag(PointerEventData eventData)
    {

        onDragParent = GameObject.Find("Fild").gameObject.transform;
        
        // 백업용 포지션과 부모 트랜스폼을 백업 해둔다.

       // startParent = transform.parent;
       
    
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
    //    if (onPointer) return;

    //    if (PointerEnterParent == null)
    //    {
    //        PointerEnterParent = GameObject.Find("PointerEnterFild").gameObject.transform;  마우스 올라갈때 커지는거 구현중 그리고 앞으로 나오는거 
                                                                                           // 현재 버그는 위에 행동 작동하면 드래그앤 드롭안됨 엉뚱한 곳으로 돌아가기(start부모가 어디서 바뀌나봄)
    //    }

    //    transform.localScale = startScale * 1.2f;
    //    startParent = transform.parent;
    //    transform.SetParent(PointerEnterParent);

    //    Debug.Log("마우스 인식");
    //    onPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.localScale = startScale ;
        //transform.position = startParent.position ;
        //transform.rotation = startParent.rotation;
        //transform.SetParent(startParent);

        //onPointer = true;

    }
}
