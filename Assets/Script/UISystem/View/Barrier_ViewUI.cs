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
            transform.localScale = Vector3.one * 1.5f;
            animator.Play("GetBarrierAnimation");
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        Barrier_Text.text = currentbarrier.ToString();
       
    }
}
