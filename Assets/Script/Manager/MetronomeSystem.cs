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
    UnityAction OnMetronomEventOnceX4;
    [SerializeField]TextMeshProUGUI Text;
    [SerializeField]int bpmCount = 0;

    int BpmX4 = 3;
    private void OnEnable()
    {
        bpmCount = 0;
    }
    void FixedUpdate()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime >= 60d / (BPM*4))
        {
            BpmX4++;
            if (BpmX4 == 4) // 정박 실행
            {
                if (Text != null)
                { 
                    Text.text = bpmCount.ToString();      
                    Text.color = Color.white;             
                }

                BpmX4 = 0;
                bpmCount++;

                //var soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Effect/Defense/Defense_Fail");
                //soundInstance.setVolume(1f); // 볼륨 0.0 ~ 1.0
                //soundInstance.start();
                //soundInstance.release(); // 

                OnMetronomEventOnceX4?.Invoke();

                OnMetronomEventOnceX4 = null;

                Debug.Log("박자");

            }

            //1/4 박실행
            CurrentTime -= 60d / (BPM * 4);
            OnMetronomEventOnce?.Invoke(); //등록된 이벤트 실행
            OnMetronomEventOnce = null; //등록된 이벤트는 한번만 실행해야 함으로 실행한후 Null

            OnMetronomEventRecurring?.Invoke();//등록된 이벤트 실행 , 리듬게임에 사용
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

    public void AddOnceMetronomX4Event(UnityAction action)
    {
        OnMetronomEventOnceX4 += action;
    }


    public void RemoveOnceMetronomEvent(UnityAction action)
    {
        OnMetronomEventOnce -= action;
    }

    

    public void RemoveRecurringMetronomEvent(UnityAction action)
    {
        OnMetronomEventRecurring -= action;
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
