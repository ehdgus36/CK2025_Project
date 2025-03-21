using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int LastWave = 3;
    [SerializeField] private int CurrentWave;

    [SerializeField]List<EnemysGroup> Enemys;

    

    public void Initialize()
    {
        if (Enemys.Count == 0) return;
        GameManager.instance.SetEnemy(Enemys[0]);

        for (int i = 0; i < Enemys.Count; i++)
        {
            Enemys[i].gameObject.SetActive(false);
        }
        CurrentWave = 1;
       

        Enemys[0].gameObject.SetActive(true);

    }

    public void NextWave()
    {
        if (CurrentWave == LastWave)
        {
            EndWave();
            return;
        }
        Enemys[CurrentWave - 1].gameObject.SetActive(false);
        CurrentWave++;
        StartCoroutine("NextWaveEventSample");
    }

    
    IEnumerator NextWaveEventSample()
    {
        yield return new WaitForSeconds(0.5f);
        Enemys[CurrentWave - 1].gameObject.SetActive(true);


        yield return new WaitForSeconds(1f);
        GameManager.instance.SetEnemy(Enemys[CurrentWave - 1]);
        GameManager.instance.InitializeTurn();
        GameManager.instance.GetHpManager().Initialize();
    }
    public void GetCurrentWaveData()
    { 
    
    }

    public void EndWave()
    {
        GameManager.instance.GameOver.SetActive(false);
    }
}
