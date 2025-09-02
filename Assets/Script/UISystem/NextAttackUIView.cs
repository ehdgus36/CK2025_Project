using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NextAttackUIView : MonoBehaviour
{
    public enum AttackIconEnum
    {  
        Attack,RecverHP,Attack_Two
    }

    [System.Serializable]
    struct AttackIconData
    {
        public AttackIconEnum Key;
        public Sprite icon;
    }

    [SerializeField] Image AttackIcon;
    [SerializeField] TextMeshProUGUI NextStateText;
    
    [SerializeField] AttackIconData[] IconDatas;
    public void UpdateUI(string atkDamage, AttackIconEnum iconEnum)
    {
        for (int i = 0; i < IconDatas?.Length; i++)
        {
            if (IconDatas[i].Key == iconEnum)
            {
                AttackIcon.sprite = IconDatas[i].icon;
            }
        }

        NextStateText.text = atkDamage;
    }

    
}
