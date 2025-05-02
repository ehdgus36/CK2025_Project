using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public enum AttackType
{
    Single, All, None
}

public enum AttackOrderType
{
    First, Last
}

[System.Serializable]
public class AttackData
{
    public AttackData(List<Dictionary<string, object>> data, int row)
    {
        Card_Code_1 = data[row]["Card_Code_1"].ToString();
        Card_Code_2 = data[row]["Card_Code_2"].ToString();
        Add_Code = data[row]["Add_Code"].ToString();
        Base_Damage_1 = (int)data[row]["Base_Damage_1"];
        Base_Damage_2 = (int)data[row]["Base_Damage_2"];
        Fire_Effect_1 = (int)data[row]["Fire_Effect_1"];
        Fire_Effect_2 = (int)data[row]["Fire_Effect_2"];
        Elec_Effect_1 = (int)data[row]["Elec_Effect_1"];
        Elec_Effect_2 = (int)data[row]["Elec_Effect_2"];
        Captiv_Effect_1 = (int)data[row]["Captiv_Effect_1"];
        Captiv_Effect_2 = (int)data[row]["Captiv_Effect_2"];
        Curse_Effect_1 = (int)data[row]["Curse_Effect_1"];
        Curse_Effect_2 = (int)data[row]["Curse_Effect_2"];
        Recover_HP = (int)data[row]["Recover_HP"];
        Attack_Effect_Code = data[row]["Attack_Effect_Code"].ToString();


        Explain_Up_1 = data[row]["Explain_Up_1"].ToString();
        Explain_Up_2 = data[row]["Explain_Up_2"].ToString();
        Explain_Down = data[row]["Explain_Down"].ToString();


        View_Card_Code_1 = Card_Code_1;
        View_Card_Code_2 = Card_Code_2;
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


    public readonly String Card_Code_1;
    public readonly String Card_Code_2;
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



    [SerializeField] String View_Card_Code_1;
    [SerializeField] String View_Card_Code_2;
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