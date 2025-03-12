using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    int MaxHp = 10;
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