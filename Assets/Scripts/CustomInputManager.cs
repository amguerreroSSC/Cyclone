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
    public static Action OnPressedQ;
    public static Action OnPressedE;
    public static Action OnPressedLShift;
    public static Action OnPressedSpace;
    public static Action OnPressedEscape;
    public static Action OnPressedLMouse;
    public static Action OnReleasedLMouse;
    public static Action<Vector3, float> OnMouseMove;

    Vector3 currentMousePos;
    Vector3 mouseOrigin = new Vector3(Screen.width / 2, Screen.height / 2);


    private void Awake()
    {
        // Make sure this is the only instance of this class (Singleton)
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        currentMousePos = Input.mousePosition;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
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
        if (Input.GetKey(KeyCode.Q))
        {
            OnPressedQ?.Invoke();
        }
        if (Input.GetKey(KeyCode.E))
        {
            OnPressedE?.Invoke();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnPressedLShift?.Invoke();
            Debug.Log("LShift");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPressedSpace?.Invoke();
            Debug.Log("Space");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            OnPressedEscape?.Invoke();
            Debug.Log("Escape");
        }
        if (currentMousePos != Input.mousePosition)
        {
            OnMouseMove?.Invoke((Input.mousePosition - currentMousePos), Vector3.Distance(currentMousePos, Input.mousePosition));
            Debug.Log("OnMouseMove");
            currentMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnPressedLMouse?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnReleasedLMouse?.Invoke();
        }
    }
}
