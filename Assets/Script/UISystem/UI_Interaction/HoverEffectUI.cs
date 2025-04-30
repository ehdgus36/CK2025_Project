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
    [SerializeField] GameObject BG;

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
            cardDescription.UpdateDescription(card.CardName, card.Example, card.SubExample, card.Grade_Point, card.icon);
        }

        if (card.GetComponent<TargetCard>() != null)
        {
            enemy = GameManager.instance.EnemysGroup.Enemys[card.GetComponent<TargetCard>().GetTargetIndex()].gameObject;
            layerind = enemy.layer;
            enemy.layer = 7;

            ChangeLayerRecursively(enemy, 7);
            Time.timeScale = 0.2f;
            BG.SetActive(true);
        }

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = StartScale;
        transform.position = StartPos;
        cardDescription.gameObject.SetActive(false);
        Time.timeScale = 1f;

      
        ChangeLayerRecursively(enemy, layerind);
        BG.SetActive(false);
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
