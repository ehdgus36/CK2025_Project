using System.Collections.Generic;
using UnityEngine;

public class ManaSystem 
{
    int CurrentMana;
    int MaxMana = 5;
    ManaUIView ManaUIView = null;
    ManaBankSystem ManaBank = new ManaBankSystem();

    Stack<int> UseManaSave = new Stack<int>(); //카드를 사용하지 못했을때 마나 반환을 위해 사용마나 정보 저장

    public ManaSystem(int maxMana)
    {
        MaxMana = maxMana;
        Initialize();
    }


    public void Initialize()
    {
        CurrentMana = MaxMana;
        ManaUIView = GameManager.instance?.UIManager?.ManaUI;
        ManaUIView?.UpdateUI(ManaTextData());
    }

    public void EndTurnReset() //턴종료후 다시 실행해야 할때마다 리셋 해야 할거
    {
        //ManaBank.SaveMana(MaxMana - CurrentMana); // 남은 마나를 저장함
       
        CurrentMana = MaxMana;// 마나 초기화

        ManaUIView.UpdateUI(ManaTextData());
        UseManaSave.Clear(); // 회복 기록 초기화
    }


    /// <summary>
    /// 마나가 사용가능하면 True 사용불가하면 False 반환
    /// </summary>
    /// <param name="useMana"></param>
    /// <returns></returns>
    public bool UseMana(int costType)
    {
        //if (CurrentMana <= 0) return false;


        int useMana = 1;

        switch (costType)
        {
            case 0:
                useMana = 0;
                break;
            case 1:
                useMana = 1;
                break;
            case 2:
                useMana = CurrentMana;
                break;
        }

        if (useMana > CurrentMana) return false;


        UseManaSave.Push(useMana);
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

    public void RecoveryMana()
    {
        CurrentMana += UseManaSave.Pop();
        ManaUIView.UpdateUI(ManaTextData());
    }
    public int UseManaCount() { return MaxMana - CurrentMana; }
}
