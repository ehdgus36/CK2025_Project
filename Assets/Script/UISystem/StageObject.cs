using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageObject : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] String Scene_Name;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("button");
        SceneManager.LoadScene(Scene_Name);
    }
}
