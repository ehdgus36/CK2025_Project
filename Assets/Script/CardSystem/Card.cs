using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Card : MonoBehaviour
{
    // [SerializeField] int Damage = 1;
    //public bool isHold = false;

    [SerializeField] String CardID;
    int CardLevel;
    int GradePoint;

    string Example;
    int Grade_Point;





    public String GetID() { return CardID; }
    public int GetGradePoint() { return Grade_Point; }

    public string GetExample() { return Example; }

    public virtual int GetDamage() { return 0; }


   
}
