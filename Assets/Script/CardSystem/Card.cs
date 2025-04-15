using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class Card : MonoBehaviour
{
    // [SerializeField] int Damage = 1;
    //public bool isHold = false;

    [SerializeField] String CardID;
    [SerializeField] int ManaCost = 1;
    [SerializeField] 
    [TextArea]string Example;





    public String GetID() { return CardID; }
    public int GetManCost() { return ManaCost; }
    public string GetExample() { return Example; }
    public abstract int GetDamage();


   
}
