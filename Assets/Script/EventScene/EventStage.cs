using UnityEngine;
using TMPro;

public class EventStage : MonoBehaviour
{
    [System.Serializable]
    private struct EventChoice
    {
        [SerializeField] public string Choice_Text;
        [SerializeField] public TextMeshProUGUI Choice_TMP;
    }
   
    [SerializeField]EventChoice[] Choices;
    

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
        // 클릭시 처리할 이벤트 적용
    }

    public void Choice2()
    {
        // 클릭시 처리할 이벤트 적용
    }

}
