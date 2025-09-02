using UnityEngine;
using UnityEngine.EventSystems;


public class HoverEffectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    [Range(1,2)]float hoverScale = 1f;
    [SerializeField] Vector3 StartScale = new Vector3(.8f, .8f, 1f);
    Vector3 StartPos;

    [SerializeField] CardDescription cardDescription;
    GameObject enemy;
    int layerind;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardDescription == null)
        {
            cardDescription = GameManager.instance.UIManager.CardDescription;
        }
       
        ///StartScale = transform.localScale;
        StartPos = transform.position;
        transform.localScale = transform.localScale * hoverScale;

        transform.position += new Vector3(0f, 0f, -1f);

        Card card = GetComponent<SlotUI>().ReadData<Card>();
      

        if (card != null)
        {
            //cardDescription.UpdateDescription(card.DescSprite); ±¸¹öÀü
            cardDescription.UpdateDescription(card.cardData.Card_Name_KR, card.cardData.Card_Des, this.transform.position);
            cardDescription.gameObject.SetActive(true);
            
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Card_UI/Card_Mouse_UP");
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
