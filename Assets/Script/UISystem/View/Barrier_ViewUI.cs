using UnityEngine;
using TMPro;
using System.Collections;

public class Barrier_ViewUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Barrier_Text;
    
    Animator animator;
    public void UpdateUI( int currentbarrier)
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (currentbarrier != 0)
        {
            this.gameObject.SetActive(true);
            animator.Play("GetBarrierAnimation");
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        Barrier_Text.text = currentbarrier.ToString();
       
    }
}
