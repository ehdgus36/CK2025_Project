using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{
    // Start is called before the first frame update

    int sampleDamage = 1;
    protected UnityAction DieEvent;

    private void Awake()
    {
        StartTurnEvent += () => { StartCoroutine("SampleAi"); };
        EndTurnEvent += () => { StopCoroutine("SampleAi"); };
    }

    public void SetDieEvent(UnityAction dieEvent)
    {
        DieEvent += dieEvent;
    }

    IEnumerator SampleAi()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.AttackDamage(1);

        yield return new WaitForSeconds(1.0f);
        
        yield return null;
    }

    protected override void Die()
    {
        Destroy(this.gameObject);
        DieEvent?.Invoke();
        
    }

}