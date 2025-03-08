using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] int damage = 1;
    public bool isHold = false;
    int GetDamage() { return damage; }
    void SetHold(bool hold) { isHold = hold; }
}
