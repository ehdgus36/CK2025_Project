using UnityEngine;
using UnityEngine.EventSystems;


public class HoverEffectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    [Range(1,2)]float hoverScale = 1f;
    Vector3 StartScale;
    Vector3 StartPos;

    [SerializeField] CardDescription cardDescription;
    GameObject enemy;
    int layerind;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        StartScale = transform.localScale;
        StartPos = transform.position;
        transform.localScale = StartScale * hoverScale;

        transform.position += new Vector3(0f, 0f, -1f);

        Card card = GetComponent<SlotUI>().ReadData<Card>();
        cardDescription.gameObject.SetActive(true);
        cardDescription.transform.position = this.transform.position;

        if (card != null)
        {
            cardDescription.UpdateDescription(card.DescSprite);
        }

      

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = StartScale;
        transform.position = StartPos;
        cardDescription.gameObject.SetActive(false);
        Time.timeScale = 1f;

      
        ChangeLayerRecursively(enemy, layerind);
      
    }

    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        if (obj != null)
        {

            obj.layer = layer;

            foreach (Transform child in obj.transform)
            {
                ChangeLayerRecursively(child.gameObject, layer);
            }
        }
    }
}
