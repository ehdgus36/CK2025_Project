using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int LastWave = 3;
    private int CurrentWave;

    List<Unit> Enemys;

    

    public void Initialize()
    {
        CurrentWave = 1;
    }

    public void NextWave()
    {
        if (CurrentWave == LastWave)
        {
            EndWave();
            return;
        }

        CurrentWave++;
        StartCoroutine("NextWaveEventSample");
    }

    IEnumerator NextWaveEventSample()
    {
        yield return new WaitForSeconds(0.5f);
        Enemys[CurrentWave - 1].gameObject.SetActive(true);


        yield return new WaitForSeconds(1f);
        GameManager.instance.InitializeTurn();

    }
    public void GetCurrentWaveData()
    { 
    
    }

    public void EndWave()
    { 
    
    }
}
