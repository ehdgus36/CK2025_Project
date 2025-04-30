
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.Audio;

public class AttackManager : MonoBehaviour
{
    public GameObject AttackEffect;
    [SerializeField] GameObject GradeAttackEffect;
    [SerializeField] SkeletonMecanim PlayerAnime;
    [SerializeField] GameObject AllEffect;
    AttackData MainData;
    int Maintarget_index;

    [SerializeField]AudioSource audioSource;
    public void Initialize()
    {
       
    }

    void PlayerAttackEffect(Vector3 pos)
    {
        AttackEffect.SetActive(false);
        AttackEffect.SetActive(true);
        AttackEffect.transform.position = pos;
    }


    public void Attack(AttackData data , int target_index)
    {
        StartCoroutine(AttackDelay(data, target_index));
    }


    IEnumerator AttackDelay(AttackData data, int target_index)
    {

        MainData = data;
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.Player.PlayerAttackAnime();
        yield return new WaitForSeconds(0.5f);

        EnemysGroup enemysGroup = GameManager.instance.EnemysGroup;


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


        if (data.Attack_Effect_Code == "Y")
        {
            if (target_index == 1000)
            {
                audioSource.Play();
                GradeAttackEffect.SetActive(false);
                GradeAttackEffect.SetActive(true);

                yield return new WaitForSeconds(.9f);



                AllEffect.SetActive(false);
                AllEffect.SetActive(true);

                yield return new WaitForSeconds(0.3f);
            }
            if (target_index != 1000)
            {
                PlayerAttackEffect(enemysGroup.Enemys[target_index].transform.position);

                yield return new WaitForSeconds(0.5f);
            }
        }


        if (data.Attack_Effect_Code == "")
        {
            PlayerAttackEffect(enemysGroup.Enemys[target_index].transform.position);
            GameManager.instance.Shake.PlayShake();
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
        

        yield return new WaitForSeconds(0.3f);

        //단일공격  // 전체 공격

        if (target_index != 1000)
        {
            //단일
            enemysGroup.Enemys[target_index].TakeDamage(data.Base_Damage_1, TargetEnemyBuff);

            // 2~3타 공격
            if (data.Card_Code_1 == "C31")
            {

                int attackCount = Random.Range(1, 3);
                for (int i = 0; i < attackCount; i++)
                {
                    yield return new WaitForSeconds(.3f);
                    enemysGroup.Enemys[target_index].TakeDamage(data.Base_Damage_1, null);
                }

            }

            //전체
            for (int i = 0; i < enemysGroup.Enemys.Count; i++)
            {
                if (i == target_index)
                {
                    continue;
                }
                enemysGroup.Enemys[i].TakeDamage(data.Base_Damage_2, AllEnemyBuff);
            }
        }

        if (target_index == 1000)
        {
            List<Enemy>Targets = new List<Enemy>();
           

            for (int i = 0; i < enemysGroup.Enemys.Count; i++)
            {
                Targets.Add(enemysGroup.Enemys[i]);
            }
            for (int i = 0; i < Targets.Count; i++)
            {
                Targets[i].TakeDamage(data.Base_Damage_1, AllEnemyBuff);
            }
        }


        //체력 회복

        GameManager.instance.Player.addHP(data.Recover_HP);

        yield return new WaitForSeconds(1.5f);
        //공격 끝
        GameManager.instance.TurnSwap();
    }
}
