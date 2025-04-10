using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    [Range(1,2)]float hoverScale = 1f;
    Vector3 StartScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
       StartScale = transform.localScale;
       transform.localScale = StartScale * hoverScale;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = StartScale;
    }
}
