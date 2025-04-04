using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Card : MonoBehaviour
{
   // [SerializeField] int Damage = 1;
    //public bool isHold = false;
   
    [SerializeField] int ManaCost = 1;
    [SerializeField] string Example;

    [SerializeField] public Sprite AttackImage;


   
   
    public int GetManCost() { return ManaCost; }
    public abstract int GetDamage();


   
}
