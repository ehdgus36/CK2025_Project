using UnityEngine;
using FMODUnity;

public class UIFMODSSoundEvent : MonoBehaviour
{
    [SerializeField] EventReference soundData;
    public void PlayerSound()
    {
        RuntimeManager.PlayOneShot(soundData);
    }
}
