using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;

public class DragDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{




    Transform onDragParent;

    [SerializeField] public Transform startParent { get; private set; }

    public Vector3 startScale; //임시

    int index;

    private Canvas canvas;

    void Start()
    {
        startScale = transform.localScale; // 일단 야매
        canvas = GetComponentInParent<Canvas>();
    }

    // 인터페이스 IBeginDragHandler를 상속 받았을 때 구현해야하는 콜백함수
    public void OnBeginDrag(PointerEventData eventData)
    {

        transform.parent.transform.SetSiblingIndex(index); // 0407 수정 드래그 시작하면 부모 원상복구
        onDragParent = GameObject.Find("Filds").gameObject.transform;

        // 백업용 포지션과 부모 트랜스폼을 백업 해둔다.

        startParent = transform.parent;


        // 드래그 시작할때 부모transform을 변경
        this.transform.SetParent(onDragParent);


        GetComponent<CanvasGroup>().blocksRaycasts = false;


        Card card = GetComponent<Card>();

        if (card != null)
        {
            if (card.cardData.Target_Type == "2")
            {
                card.transform.localScale = new Vector3(1.44f, 1.44f, 1.44f);
            }
        }
    }



    // 인터페이스 IDragHandler 상속 받았을 때 구현 해야하는 콜백함수
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint))
        {
            transform.localPosition = localPoint;
        }
        //transform.localScale = startScale;

    }

    // 인터페이스 IEndDragHandler 상속 받았을 때 구현 해야하는 콜백함수
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("카드의 좌표 :" + this.transform.position.y);
        if (GetComponent<Card>() != null && this.transform.position.y > Card.UsePos)
        {

        }
        else if (transform.parent == onDragParent)
        {
            transform.position = startParent.position;
            transform.SetParent(startParent);
            transform.rotation = startParent.rotation;
            transform.localScale = new Vector3(1, 1, 1);
        }

        //CardDropArea?.SetActive(false);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
