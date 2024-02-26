using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager : MonoBehaviour
{
    public CustomInputManager Instance { get; private set; }

    // Actions
    public static Action OnPressedW;
    public static Action OnPressedA;
    public static Action OnPressedS;
    public static Action OnPressedD;
    public static Action OnPressedSpace;
    public static Action OnPressedEscape;


    private void Awake()
    {
        // Make sure this is the only instance of this class (Singleton)
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    void Update()
    {
        // Detect player inputs and invoke actions accordingly
        if (Input.GetKey(KeyCode.W))
        {
            OnPressedW.Invoke();
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnPressedA.Invoke();
        }
        if (Input.GetKey(KeyCode.S))
        {
            OnPressedS.Invoke();
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnPressedD.Invoke();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            OnPressedSpace.Invoke();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            OnPressedEscape.Invoke();
        }
    }
}
