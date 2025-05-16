using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MetronomeSystem : MonoBehaviour
{


    [SerializeField] float BPM;
    double CurrentTime;
    UnityAction OnMetronomEventOnce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime >= 60d / BPM)
        { 
            CurrentTime -= 60d / BPM;
            OnMetronomEventOnce?.Invoke();
            OnMetronomEventOnce?.Clone(); //등록된 이벤트는 한번만 실행
        }
    }


    /// <summary>
    /// 등록된 이벤트는 한박자에 한번만 실행합니다. 다시 사용하려면 재등록 필요
    /// </summary>
    /// <param name="action"></param>
    public void AddOnceMetronomEvent(UnityAction action)
    {
        OnMetronomEventOnce += action;        
    }
    
}
