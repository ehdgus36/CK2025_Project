using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    int MaxHp = 0;
    int CurrentHp = 10;
    public GameManager gameManager;

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
    }
    public void ThisTurn()
    {
    
    }
}
