using UnityEngine;
using UnityEngine.EventSystems;

enum CardType
{ 
NOMAL ,SPECIAL , UP_GRADE
}
public class MixtureCardSlotUI : SlotUI
{
    [SerializeField]CardType CardType;

}
