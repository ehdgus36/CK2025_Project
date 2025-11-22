using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EventStage : MonoBehaviour
{
    [System.Serializable]
    private struct EventChoice
    {
        [SerializeField] public string Choice_Text;
        [SerializeField] public TextMeshProUGUI Choice_TMP;
    }
   
    [SerializeField]EventChoice[] Choices;
    [SerializeField] Animator Animator;

    [SerializeField] GameObject EXitButton;
    [SerializeField] TextMeshProUGUI Result_TMP;

    private void Awake()
    {
        for(int i=0;i < Choices.Length ; i++)
            Choices[i].Choice_TMP.text = string.Format("<#4F4F4F>{0}</color>", Choices[i].Choice_Text);
       
    }


    public void PointerEnter(int index)
    {
        Choices[index].Choice_TMP.text = string.Format("<color=white>{0}</color>", Choices[index].Choice_Text);
    }

    
    public void PointerExit(int index)
    {
        Choices[index].Choice_TMP.text = string.Format("<#4F4F4F>{0}</color>", Choices[index].Choice_Text);
    }

    public void Choice1()
    {

        Animator.Play("TakeItem");
        Choices[0].Choice_TMP.gameObject.transform.parent.gameObject.SetActive(false);
        Choices[1].Choice_TMP.gameObject.transform.parent.gameObject.SetActive(false);
        EXitButton.SetActive(true);



        List<string> playerItemData = new List<string>();


        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA, out playerItemData);

        playerItemData.Add("IT03");

        //정보 갱신

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA, playerItemData);
        // 클릭시 처리할 이벤트 적용
    }

    public void Choice2()
    {
        Exit();
        // 클릭시 처리할 이벤트 적용
    }

    public void Exit()
    {
        SceneManager.LoadScene("GameMap");

    }
}
