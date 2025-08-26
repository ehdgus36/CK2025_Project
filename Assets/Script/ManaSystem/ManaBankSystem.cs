using UnityEngine;

public class ManaBankSystem
{
    public static readonly int MAX_BANK_MANA = 10;
    
   

    public void SaveMana(int mana)
    {
        //항상 가져오기
        int CurrentMana;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, out CurrentMana);

        if (CurrentMana >= MAX_BANK_MANA) return;

        CurrentMana += mana;
        CurrentMana = Mathf.Clamp(CurrentMana, 0, MAX_BANK_MANA);


        //데이터 시스템에 데이터 갱신
        //GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, CurrentMana);
    }



   
   
}
