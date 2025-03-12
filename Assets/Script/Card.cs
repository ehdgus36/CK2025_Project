using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] int Damage = 1;
    public bool isHold = false;
    [SerializeField] int ManaCost = 1;
    public int GetDamage() { return Damage; }
    public int GetManCost() { return ManaCost; }
    void SetHold(bool hold) { isHold = hold; }
   
}
