
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
  
    public void Initialize()
    {
       
    }

    public void Attack(AttackData data , int target_index)
    {     
        EnemysGroup enemysGroup = GameManager.instance.GetEnemysGroup();



        //단일공격
        enemysGroup.GetEnemy(target_index).TakeDamage(data.Base_Damage_1);

        //전체 공격
        for (int i = 0; i < enemysGroup.GetEnemyCount(); i++)
        {
            if (i == target_index)
            {
                continue;
            }
            enemysGroup.GetEnemy(i).TakeDamage(data.Base_Damage_2);
        }

        //버프 턴공격

        //전체 버프 턴

        //체력 회복
    } 
}
