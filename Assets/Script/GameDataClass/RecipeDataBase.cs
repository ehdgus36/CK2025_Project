using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using static UnityEditor.U2D.ScriptablePacker;


public struct RecipeData
{
    public RecipeData(Dictionary<string, object> data)
    {
        Add_Code = data["Add_Code"].ToString();
        Base_Damage_1 = (int)data["Base_Damage_1"];
        Base_Damage_2 = (int)data["Base_Damage_2"];
        Fire_Effect_1 = (int)data["Fire_Effect_1"];
        Fire_Effect_2 = (int)data["Fire_Effect_2"];
        Elec_Effect_1 = (int)data["Elec_Effect_1"];
        Elec_Effect_2 = (int)data["Elec_Effect_2"];
        Captiv_Effect_1 = (int)data["Captiv_Effect_1"];
        Captiv_Effect_2 = (int)data["Captiv_Effect_2"];
        Curse_Effect_1 = (int)data["Curse_Effect_1"];
        Curse_Effect_2 = (int)data["Curse_Effect_2"];
        Recover_HP = (int)data["Recover_HP"];
        Attack_Effect_Code = data["Attack_Effect_Code"].ToString();


        Explain_Up_1 = data["Explain_Up_1"].ToString();
        Explain_Up_2 = data["Explain_Up_2"].ToString();
        Explain_Down = data["Explain_Down"].ToString();



        View_Add_Code = Add_Code;
        View_Base_Damage_1 = Base_Damage_1;
        View_Base_Damage_2 = Base_Damage_2;
        View_Fire_Effect_1 = Fire_Effect_1;
        View_Fire_Effect_2 = Fire_Effect_2;
        View_Elec_Effect_1 = Elec_Effect_1;
        View_Elec_Effect_2 = Elec_Effect_2;
        View_Captiv_Effect_1 = Captiv_Effect_1;
        View_Captiv_Effect_2 = Captiv_Effect_2;
        View_Curse_Effect_1 = Curse_Effect_1;
        View_Curse_Effect_2 = Curse_Effect_2;
        View_Recover_HP = Recover_HP;
        View_Attack_Effect_Code = Attack_Effect_Code;
        View_Explain_Up_1 = Explain_Up_1;
        View_Explain_Up_2 = Explain_Up_2;
        View_Explain_Down = Explain_Down;
    }

    public readonly String Add_Code;
    public readonly int Base_Damage_1;
    public readonly int Base_Damage_2;
    public readonly int Fire_Effect_1;
    public readonly int Fire_Effect_2;
    public readonly int Elec_Effect_1;
    public readonly int Elec_Effect_2;
    public readonly int Captiv_Effect_1;
    public readonly int Captiv_Effect_2;
    public readonly int Curse_Effect_1;
    public readonly int Curse_Effect_2;
    public readonly int Recover_HP;
    public readonly string Attack_Effect_Code;
    public readonly string Explain_Up_1;
    public readonly string Explain_Up_2;
    public readonly string Explain_Down;




    [SerializeField] String View_Add_Code;
    [SerializeField] int View_Base_Damage_1;
    [SerializeField] int View_Base_Damage_2;
    [SerializeField] int View_Fire_Effect_1;
    [SerializeField] int View_Fire_Effect_2;
    [SerializeField] int View_Elec_Effect_1;
    [SerializeField] int View_Elec_Effect_2;
    [SerializeField] int View_Captiv_Effect_1;
    [SerializeField] int View_Captiv_Effect_2;
    [SerializeField] int View_Curse_Effect_1;
    [SerializeField] int View_Curse_Effect_2;
    [SerializeField] int View_Recover_HP;
    [SerializeField] String View_Attack_Effect_Code;
    [SerializeField] String View_Explain_Up_1;
    [SerializeField] String View_Explain_Up_2;
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
