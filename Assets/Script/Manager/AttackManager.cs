
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
        StartCoroutine(AttackDelay(data, target_index));
    }


    IEnumerator AttackDelay(AttackData data, int target_index)
    {

        yield return new WaitForSeconds(1.0f);

        EnemysGroup enemysGroup = GameManager.instance.GetEnemysGroup();


        Debug.Log("Player공격");
        //버프 제작
        Buff TargetEnemyBuff = null;
        Buff AllEnemyBuff = null;

        if (data.Fire_Effect_1 != 0) // 화염 도트뎀
        {
            TargetEnemyBuff = new FireBuff(BuffType.End, data.Fire_Effect_1, 2);
            AllEnemyBuff = new FireBuff(BuffType.End, data.Fire_Effect_2, 2);
        }

        if (data.Elec_Effect_1 != 0) // 전기 방어력 감소
        {
            TargetEnemyBuff = new ElecBuff(BuffType.End, data.Elec_Effect_1, 1);
            AllEnemyBuff = new ElecBuff(BuffType.End, data.Elec_Effect_2, 1);
        }

        if (data.Captiv_Effect_1 != 0) //혼란이상 상태
        {
            TargetEnemyBuff = new CaptivBuff(BuffType.End, data.Captiv_Effect_1, 50);
            AllEnemyBuff = new CaptivBuff(BuffType.End, data.Captiv_Effect_2, 50);
        }

        if (data.Curse_Effect_1 != 0) //몬스터 공격력 감소
        {
            TargetEnemyBuff = new CurseBuff(BuffType.End, data.Curse_Effect_1, 1);
            AllEnemyBuff = new CurseBuff(BuffType.End, data.Curse_Effect_1, 1);
        }


        //단일공격  // 전체 공격


        enemysGroup.GetEnemy(target_index).TakeDamage(data.Base_Damage_1, TargetEnemyBuff);


        for (int i = 0; i < enemysGroup.GetEnemyCount(); i++)
        {
            if (i == target_index)
            {
                continue;
            }
            enemysGroup.GetEnemy(i).TakeDamage(data.Base_Damage_2, AllEnemyBuff);
        }






        //체력 회복



        yield return new WaitForSeconds(1.0f);
        //공격 끝
        GameManager.instance.TurnSwap();
    }
}
