using FMODUnity;
using UnityEngine;

public class FMODTEST : MonoBehaviour
{
    [SerializeField] StudioEventEmitter FMOD;


    private void Start()
    {
        FMOD.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            FMOD.EventInstance.setParameterByNameWithLabel("Parameter 1", "Common");
            Debug.Log("FMOD C");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            FMOD.EventInstance.setParameterByNameWithLabel("Parameter 1", "Upgrade");
            Debug.Log("FMOD U");
        }
    }
}
