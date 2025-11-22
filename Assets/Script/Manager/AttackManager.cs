
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;




public class AttackManager : MonoBehaviour
{
    [System.Serializable]
    struct AttakEffectData
    {
        [SerializeField] public string code;
        [SerializeField] public GameObject Effect;
    }

    SkeletonAnimation anime;

    [SerializeField] AttakEffectData[] AttackEffectDatas;


    Dictionary<string,GameObject> AttackEffects = new Dictionary<string,GameObject>();
  
 

    [SerializeField]AudioSource audioSource;
    public void Initialize()
    {
        
        for (int i = 0; i < AttackEffectDatas.Length; i++)
        {
            AttackEffects.Add(AttackEffectDatas[i].code , AttackEffectDatas[i].Effect);
        }
    }

    void PlayerAttackEffect(string code)
    {
        AttackEffects[code]?.SetActive(false);
        AttackEffects[code]?.SetActive(true);
    }

    public void Attack(RecipeData data)
    {
        StartCoroutine(AttackDelay(data));
    }


    IEnumerator AttackDelay(RecipeData data)
    {
        string code = data.Card_Code_1;

        if (AttackEffects.ContainsKey(code))
        {
            PlayerAttackEffect(code);
        }

        yield return null;
    }
}
