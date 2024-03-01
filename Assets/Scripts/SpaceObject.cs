using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] GameObject origin;
    [SerializeField] float rollSpeed;
    [SerializeField] float translateSpeed;
    [SerializeField] Vector3 scaleSpeed;
    private void OnEnable()
    {
        CustomInputManager.OnPressedD += RollLeft;
        CustomInputManager.OnPressedA += RollRight;
        CustomInputManager.OnPressedS += TranslateUp;
        CustomInputManager.OnPressedW += TranslateDown;
        CustomInputManager.OnPressedLShift += TranslateAwayFromOrigin;
        //CustomInputManager.OnMouseMove += Rotate;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedD -= RollLeft;
        CustomInputManager.OnPressedA -= RollRight;
        CustomInputManager.OnPressedS -= TranslateUp;
        CustomInputManager.OnPressedW -= TranslateDown;
        CustomInputManager.OnPressedLShift -= TranslateAwayFromOrigin;
        //CustomInputManager.OnMouseMove -= Rotate;
    }

    private void Start()
    {
        transform.parent = origin.transform;
    }

    private void RollRight()
    {
        origin.transform.Rotate(new Vector3(0, 0, 1) * rollSpeed * Time.deltaTime);
    }

    private void RollLeft()
    {
        origin.transform.Rotate(new Vector3(0, 0, -1) * rollSpeed * Time.deltaTime);
    }

    private void TranslateUp()
    {
        transform.position += Vector3.up * translateSpeed * Time.deltaTime;
    }

    private void TranslateDown()
    {
        transform.position += Vector3.down * translateSpeed * Time.deltaTime;  
    }

    private void TranslateAwayFromOrigin()
    {
        Vector3 moveDir = transform.position - Vector3.zero;
        transform.position += moveDir * Time.deltaTime;
        transform.localScale += scaleSpeed * Time.deltaTime;
    }
}
