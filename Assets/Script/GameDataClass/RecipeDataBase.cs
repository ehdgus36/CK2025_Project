using System.Collections.Generic;
using System;
using UnityEngine;



public struct RecipeData
{
    public RecipeData(Dictionary<string, object> data)
    {
        Card_Code_1 = data["Card_Code_1"].ToString();
        Card_Code_2 = data["Card_Code_2"].ToString();
        Card_Code_3 = data["Card_Code_3"].ToString();
        Add_Code = data["Add_Code"].ToString();

        Target_Monster = (int)data["Target_Monster"];
        Attack_Count = (int)data["Attack_Count"];

        Base_Damage_1 = (int)data["Base_Damage_1"];
        Base_Damage_2 = (int)data["Base_Damage_2"];


        Status_Type_1 = (int)data["Status_Type_1"];
        Status_Turn_1 = (int)data["Status_Turn_1"];


        Status_Type_2 = (int)data["Status_Type_2"];
        Status_Turn_2 = (int)data["Status_Turn_2"];


        Recover_HP = (int)data["Recover_HP"];
        
        Explain_Up = data["Explain_Up"].ToString();
        Explain_Down = data["Explain_Down"].ToString();


        View_Card_Code_1 = Card_Code_1;
        View_Card_Code_2 = Card_Code_2;
        View_Card_Code_3 = Card_Code_3;
        View_Add_Code = Add_Code;

        View_Target_Monster = Target_Monster;
        View_Attack_Count = Attack_Count;

        View_Base_Damage_1 = Base_Damage_1;
        View_Base_Damage_2 = Base_Damage_2;

        View_Status_Type_1 = Status_Type_1;
        View_Status_Turn_1 = Status_Turn_1;

        View_Status_Type_2 = Status_Type_1;
        View_Status_Turn_2 = Status_Type_2;

        View_Recover_HP = Recover_HP;
        
        View_Explain_Up = Explain_Up;
        View_Explain_Down = Explain_Down;
    }

    public readonly String Card_Code_1;
    public readonly String Card_Code_2;
    public readonly String Card_Code_3;
    public readonly String Add_Code;

    public readonly int Target_Monster;
    public readonly int Attack_Count;

    public readonly int Base_Damage_1;
    public readonly int Base_Damage_2;

    public readonly int Status_Type_1;
    public readonly int Status_Turn_1;

    public readonly int Status_Type_2;
    public readonly int Status_Turn_2;


    public readonly int Recover_HP;

    public readonly string Explain_Up;
    public readonly string Explain_Down;



    [SerializeField] String View_Card_Code_1;
    [SerializeField] String View_Card_Code_2;
    [SerializeField] String View_Card_Code_3;
    [SerializeField] String View_Add_Code;

    [SerializeField] int View_Target_Monster;
    [SerializeField] int View_Attack_Count;

    [SerializeField] int View_Base_Damage_1;
    [SerializeField] int View_Base_Damage_2;

    [SerializeField] int View_Status_Type_1;
    [SerializeField] int View_Status_Turn_1;

    [SerializeField] int View_Status_Type_2;
    [SerializeField] int View_Status_Turn_2;

    [SerializeField] int View_Recover_HP;



    [SerializeField] String View_Explain_Up;
    [SerializeField] String View_Explain_Down;


}


public class RecipeDataBase
{
    RecipeData[] Recipe_Data;

    public RecipeDataBase(TextAsset RecipeDataTable)
    {
       
        List<Dictionary<string, object>> recipe = CSVReader.Read(RecipeDataTable);

        Recipe_Data = new RecipeData[recipe.Count];


        for (int i = 0; i < recipe.Count; i++)
        {
            Recipe_Data[i] = new RecipeData(recipe[i]);
        }
    }
    public bool SearchData(string recipeCode , ref RecipeData get_recipeData)
    {
        for (int i = 0; i < Recipe_Data.Length; i++)
        {
            if (Recipe_Data[i].Add_Code == recipeCode )
            {
                get_recipeData = Recipe_Data[i];
                return true;
            }
        }

        return false;
    }


    public bool SearchData(string recipeCode)
    {
        for (int i = 0; i < Recipe_Data.Length; i++)
        {
            if (Recipe_Data[i].Add_Code == recipeCode)
            {
                return true;
            }
        }

        return false;
    }

}
