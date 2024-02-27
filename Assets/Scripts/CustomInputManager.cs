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
    public static Action OnPressedLShift;
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
            OnPressedW?.Invoke();
            Debug.Log("W");
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnPressedA?.Invoke();
            Debug.Log("A");
        }
        if (Input.GetKey(KeyCode.S))
        {
            OnPressedS?.Invoke();
            Debug.Log("S");
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnPressedD?.Invoke();
            Debug.Log("D");
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnPressedLShift?.Invoke();
            Debug.Log("LShift");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            OnPressedSpace?.Invoke();
            Debug.Log("Space");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            OnPressedEscape?.Invoke();
            Debug.Log("Escape");
        }
    }
}
