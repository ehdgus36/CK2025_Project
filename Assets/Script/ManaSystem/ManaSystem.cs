using UnityEngine;

public class ManaSystem 
{
    int CurrentMana;
    const int MaxMana = 5;
    ManaUIView ManaUIView = null;
    ManaBankSystem ManaBank = new ManaBankSystem();

    public ManaSystem()
    {
        Initialize();
    }


    public void Initialize()
    {
        CurrentMana = MaxMana;
        ManaUIView = GameManager.instance?.UIManager?.ManaUI;
    }

    public void EndTurnReset() //턴종료후 다시 실행해야 할때마다 리셋 해야 할거
    {
        ManaBank.SaveMana(MaxMana - CurrentMana); // 남은 마나를 저장함
       
        CurrentMana = MaxMana;// 마나 초기화
    }


    /// <summary>
    /// 마나가 사용가능하면 True 사용불가하면 False 반환
    /// </summary>
    /// <param name="useMana"></param>
    /// <returns></returns>
    public bool UseMana(int useMana)
    {
        if (useMana > CurrentMana) return false;

        CurrentMana -= useMana;

        if (ManaUIView != null)
        {
            ManaUIView.UpdateUI(ManaTextData());
        }
        else
        {
            ManaUIView = GameManager.instance.UIManager?.ManaUI;
        }
       
        return true;
    }


    /// <summary>
    /// 마나의 정보를 "currentMana/MaxMana" 의 스트링형태로 반환
    /// </summary>
    /// <returns></returns>
    string ManaTextData()
    {
        return CurrentMana.ToString() + "/" + MaxMana.ToString();
    }

}
