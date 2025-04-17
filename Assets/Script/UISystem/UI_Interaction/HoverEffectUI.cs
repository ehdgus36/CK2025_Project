using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    [Range(1,2)]float hoverScale = 1f;
    Vector3 StartScale;
    Vector3 StartPos;

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        StartScale = transform.localScale;
        StartPos = transform.position;
        transform.localScale = StartScale * hoverScale;

        transform.position += new Vector3(0f, 0f, -1f);

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = StartScale;
        transform.position = StartPos;
    }
}
