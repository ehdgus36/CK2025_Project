using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Spine;
using Spine.Unity;
using System.Collections;

public class CardDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CardName;
    [SerializeField] TextMeshProUGUI Desc;
    [SerializeField] TextMeshProUGUI DescSub;
    [SerializeField] TextMeshProUGUI Grade_Point;
    [SerializeField] Image DescImage;

    [SerializeField] Sprite Attack;
    [SerializeField] Sprite Debuff;
    [SerializeField] Sprite Buff;

    [SerializeField] SkeletonGraphic LD_Attack;
    [SerializeField] SkeletonGraphic LD_Buff;
    [SerializeField] SkeletonGraphic LD_Debuff;

    SkeletonGraphic selectLD = null;

    Coroutine coroutine;

    float delayTime = .2f;
    public void UpdateDescription(Vector3 pos, CardData cardData)
    {
        LD_Attack.gameObject.SetActive(false);
        LD_Buff.gameObject.SetActive(false);
        LD_Debuff.gameObject.SetActive(false);

        if (cardData.Target_Type == "1")
        {
           selectLD = LD_Buff;     
        }
        else
        {
            selectLD = LD_Attack;
        }

        selectLD.gameObject.SetActive(true);
        selectLD.AnimationState.SetAnimation(0, "idle", true);
        CardName.text = cardData.Card_Name_KR;
        Desc.text = cardData.Card_Des;
        DescImage.transform.position = pos;
    }

    public void ActiveCard()
    {
        if (selectLD != null)
        {
            selectLD.AnimationState.SetAnimation(0, "activate a card", false);
            StopCoroutine("DisableGameObjectDelay");
            delayTime = .5f;
            StartCoroutine("DisableGameObjectDelay");
        }
    }

    public void SetActive(bool isactive)
    {
        if (isactive == true)
        {
            CardName.gameObject.transform.parent.gameObject.SetActive(true);
            if (coroutine != null) 
                StopCoroutine("DisableGameObjectDelay");

            this.gameObject.SetActive(true);
        }
        if (isactive == false)
        {
            CardName.gameObject.transform.parent.gameObject.SetActive(false);
            delayTime = .05f;
            coroutine = StartCoroutine("DisableGameObjectDelay");
        }
    }

    IEnumerator DisableGameObjectDelay()
    {
        yield return new WaitForSeconds(delayTime);
        selectLD.gameObject.SetActive(false);
    }

}
