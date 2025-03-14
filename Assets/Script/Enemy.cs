using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    // Start is called before the first frame update

    int sampleDamage = 1;

    private void Start()
    {
        StartTurnEvent += () => { StartCoroutine("SampleAi"); };
        EndTurnEvent += () => { StopCoroutine("SampleAi"); };
    }
    IEnumerator SampleAi()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.instance.AttackDamage(1);

        yield return null;
    }

}