using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buff_Icon_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] BuffIcon;

    public void UpdateBuffIcon(List<Buff> buffs)
    {
        // Debug.Log(buffs.GetType());

        if (buffs == null) return;
        // Debug.Log("UI갱신 활성화");

        for (int i = 0; i < BuffIcon.Length; i++)
        {
            BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false);
            BuffIcon[i].text = "";
        }



        for (int i = 0; i < buffs.Count; i++)
        {

            string buffTurn = buffs[i].GetBuffDurationTurn().ToString();

            if (buffs[i].GetBuffDurationTurn() <= 0)
            {
                buffTurn = "";

                continue;

            }



            switch (buffs[i])
            {
                case FireBuffBrunOut F: // 빨

                    BuffIcon[0].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[0].text = buffTurn;
                    if (buffTurn == "") BuffIcon[0].gameObject.transform.parent.gameObject.SetActive(false);
                    break;

                case FireBuff F: // 빨

                    BuffIcon[1].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[1].text = buffTurn;
                    if (buffTurn == "") BuffIcon[1].gameObject.transform.parent.gameObject.SetActive(false);
                    break;

                case AttackDamageDownBuff_Mute F: // 초
                    BuffIcon[2].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[2].text = buffTurn;

                    break;

                case AttackDamageDownBuff F: // 초
                    BuffIcon[3].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[3].text = buffTurn;
                    break;

                case BarbedArmorBuff F: // 초
                    BuffIcon[4].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[4].text = buffTurn;
                    break;

                case VolumeUPBuff F: // 초
                    BuffIcon[5].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[5].text = buffTurn;
                    break;

                case RhythmDebuff F: // 초
                    BuffIcon[6].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[6].text = buffTurn;
                    break;

                case PoisonBuff F: // 초
                    BuffIcon[7].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[7].text = buffTurn;
                    break;

            }

            
           
        }
    }

}
