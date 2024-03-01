using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        CustomInputManager.OnPressedLMouse += Activate;
        CustomInputManager.OnReleasedLMouse += Deactivate;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedLMouse -= Activate;
        CustomInputManager.OnReleasedLMouse -= Deactivate;
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
