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
    [SerializeField] public String CardName;
    [SerializeField] public String Example;
    [SerializeField] public String SubExample;
    public int Grade_Point = 1;
    [SerializeField] public Sprite icon;

    [SerializeField] Card UpGradeCard;
    public virtual void Initialized() { }

    public String GetID() { return CardID; }

    public String GetName() { return CardName; }
    public int GetGradePoint() { return Grade_Point; }

    public string GetExample() { return Example; }

    public virtual int GetDamage() { return 0; }

    public virtual Card GetUpGradeCard() { return UpGradeCard; }

}
