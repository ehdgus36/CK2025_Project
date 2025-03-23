
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    Transform FromUnitPos;
    Transform ToUnitPos;

    [SerializeField] int TotalDamage;
    [SerializeField] Sprite AttackSprite;
    [SerializeField] int AttackCount = 1;
    [SerializeField] int CurrentAttackCount = 0;
    [SerializeField] AttackEffectObj attackObj;
    [SerializeField] AttackType attackType;
    public void Initialize()
    {
        TotalDamage = 0;
        AttackSprite = null;
        AttackCount = 1;
        CurrentAttackCount = 0;
        attackType = AttackType.None;
    }

    public void Attack(Unit from, Unit to, List<Card> attackCard)
    {
        FromUnitPos = from.transform;
        ToUnitPos = to.transform;

        for (int i = 0; i < attackCard.Count; i++)
        {
            switch (attackCard[i].cardType)
            {
                case CardType.Attack:
                    TotalDamage += attackCard[i].Data.Damage;
                    if (attackType == AttackType.None)
                    {
                        attackType = attackCard[i].Data.Type;
                    }
                    attackType = attackCard[i].Data.Type;
                    break;
                case CardType.AttackBuff:
                    if (attackType == AttackType.None)
                    {
                        attackType = attackCard[i].Data.Type;
                    }

                    if (attackCard[i].AttackImage != null)
                    {
                        AttackSprite = attackCard[i].AttackImage;
                    }

                    TotalDamage += attackCard[i].PlusDamage;
                    break;
                case CardType.Buff:
                    AttackCount *= attackCard[i].AttackCount;
                    break;
            }
        }

        StartCoroutine("AttackUpdate");
    }

    IEnumerator AttackUpdate()
    {
        if (attackType == AttackType.Single)
        {
            for (int i = 0; i < AttackCount; i++)
            {
                AttackEffectObj obj = Instantiate<AttackEffectObj>(attackObj, FromUnitPos.position, FromUnitPos.rotation);
                obj.SetData(ToUnitPos, AttackDamage, AttackSprite);
                yield return new WaitForSeconds(0.2f);
            }
        }
        if (attackType == AttackType.All)
        {
            for (int i = 0; i < AttackCount; i++)
            {
                AttackEffectObj obj = Instantiate<AttackEffectObj>(attackObj, FromUnitPos.position, FromUnitPos.rotation);
                obj.SetData(ToUnitPos, AttackAllDamage, AttackSprite);
                yield return new WaitForSeconds(0.2f);
            }
        }
        yield return null;
    }

    void AttackDamage()
    {
        GameManager.instance.AttackDamage(TotalDamage);
        CurrentAttackCount++;
        if (AttackCount == CurrentAttackCount)
        {
            Initialize();
        }
    }

    void AttackAllDamage()
    {
        GameManager.instance.GetEnemysGroup().TakeAllDamage(TotalDamage);
        CurrentAttackCount++;
        if (AttackCount == CurrentAttackCount)
        {
            Initialize();
        }
    }
}
