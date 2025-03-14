using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    private void Start()
    {
        StartTurnEvent += PlayableSystemOn;
        StartTurnEvent += DackDrow;
        EndTurnEvent += PlayableSystemOff;
    }

    void DackDrow()
    { 
    
    }

    void PlayableSystemOn()
    { 
    
    }

    void PlayableSystemOff()
    { 
    
    }
}
