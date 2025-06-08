using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class MetronomeSystem : MonoBehaviour
{


    [SerializeField] float BPM;
    double CurrentTime;
    UnityAction OnMetronomEventOnce;
    UnityAction OnMetronomEventRecurring;
    [SerializeField]TextMeshProUGUI Text;
    int bpmCount = 0;

    void FixedUpdate()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime >= 60d / BPM)
        { 
            bpmCount++; 
            CurrentTime -= 60d / BPM;
            OnMetronomEventOnce?.Invoke(); //등록된 이벤트 실행
            OnMetronomEventOnce = null; //등록된 이벤트는 한번만 실행해야 함으로 실행한후 Null

            OnMetronomEventRecurring?.Invoke();//등록된 이벤트 실행 , 리듬게임에 사용


            if (Text != null)
            {
                Text.text = bpmCount.ToString();
            }
        }
    }


    /// <summary> 등록된 이벤트는 한박자에 한번만 실행합니다. 다시 사용하려면 재등록 필요 </summary>
    /// <param name="action"></param>
    public void AddOnceMetronomEvent(UnityAction action)
    {
        OnMetronomEventOnce += action;        
    }

    /// <summary> 등록된 이벤트는 매박자에 한번씩 실행합니다. 다시 사용하려면 재등록 필요 </summary>
    /// <param name="action"></param>

    public void AddRecurringMetronomEvent(UnityAction action)
    {
        OnMetronomEventRecurring += action;
    }


    /// <summary> 등록된 모든 이벤트 삭제</summary>
    public void RecurringMetronomEventClear()
    {
        OnMetronomEventRecurring = null;
    }

    /// <summary> 등록된 모든 이벤트 삭제</summary>
    public void OnceMetronomEventClear()
    {
        OnMetronomEventOnce = null;  
    }
}
