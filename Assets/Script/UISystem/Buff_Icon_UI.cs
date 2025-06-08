using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buff_Icon_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] BuffIcon;

    public void UpdateBuffIcon(List<Buff> buffs)
    {
        if (buffs == null) return;


        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].GetBuffDurationTurn() != 0)
            {

                switch (buffs[i])
                {
                    case FireBuff F: // 빨
                        BuffIcon[0].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[0].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;

                    case ElecBuff F:// 파
                        BuffIcon[1].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[1].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;


                    case CaptivBuff F:// 보
                        BuffIcon[2].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[2].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;

                    case CurseBuff F: // 초
                        BuffIcon[3].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[3].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;

                }
            }
            else // 남은 턴수가 0이면 지워주기
            {
                BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false); 
                BuffIcon[i].text = buffs[i].GetBuffDurationTurn().ToString();
            }
        }
    }

}
