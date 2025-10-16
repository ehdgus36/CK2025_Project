using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buff_Icon_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] BuffIcon;

    public void UpdateBuffIcon(List<Buff> buffs)
    {
        Debug.Log(buffs.GetType());

        if (buffs == null) return;
        Debug.Log("UI갱신 활성화");

        for (int i = 0; i < BuffIcon.Length; i++)
        {
            BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false);
            BuffIcon[i].text = "";
        }



        for (int i = 0; i < buffs.Count; i++)
        {
            //if (buffs[i].GetState == BuffState.Enable)
            //{

            string buffTurn = buffs[i].GetBuffDurationTurn().ToString();

            if (buffs[i].GetBuffDurationTurn() == 0)
            {
                buffTurn = "";
            }

            

            switch (buffs[i])
            {
                case FireBuff F: // 빨
                    Debug.Log("UI갱신");
                    BuffIcon[0].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[0].text = buffTurn;
                    if (buffTurn == "") BuffIcon[0].gameObject.transform.parent.gameObject.SetActive(false);
                    break;

                case AttackDamageDownBuff F: // 초
                    BuffIcon[3].gameObject.transform.parent.gameObject.SetActive(true);
                    BuffIcon[3].text = buffTurn;
                    break;

            }
            //}
            //else // 남은 턴수가 0이면 지워주기
            // {
            //Debug.Log("UI갱신");

            //BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false); 
            //BuffIcon[i].text = buffs[i].GetBuffDurationTurn().ToString();
            //}
        }
    }

}
