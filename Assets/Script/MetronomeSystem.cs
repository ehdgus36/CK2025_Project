using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class MetronomeSystem : MonoBehaviour
{


    [SerializeField] float BPM;
    double CurrentTime;
    UnityAction OnMetronomEventOnce;
    [SerializeField]TextMeshProUGUI Text;
    int bpmCount = 0;

  


    void FixedUpdate()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime >= 60d / BPM)
        { 
            bpmCount++;
            Text.text = bpmCount.ToString();
            CurrentTime -= 60d / BPM;

            OnMetronomEventOnce?.Invoke(); //등록된 이벤트 실행

            OnMetronomEventOnce = null; //등록된 이벤트는 한번만 실행해야 함으로 실행한후 Null
        }
    }


    /// <summary> 등록된 이벤트는 한박자에 한번만 실행합니다. 다시 사용하려면 재등록 필요 </summary>
    /// <param name="action"></param>
    public void AddOnceMetronomEvent(UnityAction action)
    {
        OnMetronomEventOnce += action;        
    }
    
}
