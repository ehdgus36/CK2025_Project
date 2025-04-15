using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandle
{
    public void InputEvent(string eventKey);
}

[Serializable]
struct SerializebleObject<T1 , T2>
{
    public T1 key;
    public T2 value;
}


public class InputManager : MonoBehaviour
{
    [SerializeField] Dictionary<string, IInputHandle> RegistInputHandles = new Dictionary<string, IInputHandle>();


    [SerializeField]List<SerializebleObject<string , IInputHandle>> RegistInputHandle_Inspector;
    // Start is called before the first frame update
    public void Initialize()
    {

        for (int i = 0; i < RegistInputHandle_Inspector.Count; i++)
        {
            RegistInputHandles.Add(RegistInputHandle_Inspector[i].key, RegistInputHandle_Inspector[i].value);
        }
    }
}
