using UnityEngine;

public class WoldMapSystem : MonoBehaviour
{
    [SerializeField] MapSystem HIP_POP;
    [SerializeField] MapSystem JAZZ;
    [SerializeField] MapSystem KPOP;

    private void Start()
    {
        HIP_POP.gameObject.SetActive(true);
        KPOP.gameObject.SetActive(true);
        JAZZ.gameObject.SetActive(true);
    }

}
