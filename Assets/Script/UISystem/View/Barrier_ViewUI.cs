using UnityEngine;
using TMPro;

public class Barrier_ViewUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Barrier_Text;
    public void UpdateUI( int currentbarrier)
    {
        Barrier_Text.text = currentbarrier.ToString();
    }
}
