using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float translateSpeed;
    [SerializeField] Vector3 scaleFactor;
    [SerializeField] float lifetime;

    private void OnEnable()
    {
        CustomInputManager.OnPressedD += TranslateLeft;
        CustomInputManager.OnPressedA += TranslateRight;
        CustomInputManager.OnPressedS += TranslateTowardsOrigin;
        CustomInputManager.OnPressedW += TranslateAwayFromOrigin;
        CustomInputManager.OnMouseMove += Rotate;

        StartCoroutine(Accelerate());
        transform.localScale = new Vector3(0.01f, 0.01f);
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedD -= TranslateLeft;
        CustomInputManager.OnPressedA -= TranslateRight;
        CustomInputManager.OnPressedS -= TranslateTowardsOrigin;
        CustomInputManager.OnPressedW -= TranslateAwayFromOrigin;
        CustomInputManager.OnMouseMove -= Rotate;
    }

    private IEnumerator Accelerate()
    {
        float elapsedTime = 0;
        while (elapsedTime < lifetime)
        {
            transform.Translate((transform.position - Vector3.zero) * translateSpeed * Time.deltaTime);
            transform.localScale += scaleFactor * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void Rotate(Vector3 dir, float distance)
    {

        transform.position += dir * distance * Time.deltaTime;
    }

    private void TranslateLeft()
    {
        transform.position += Vector3.left * translateSpeed*2 * Time.deltaTime;
    }

    private void TranslateRight()
    {
        transform.position += Vector3.right * translateSpeed*2 * Time.deltaTime;
    }

    private void TranslateTowardsOrigin()
    {
        Vector3 moveDir = Vector3.zero - transform.position;
        transform.position += moveDir * Time.deltaTime;
        transform.localScale += -scaleFactor * Time.deltaTime;
    }

    private void TranslateAwayFromOrigin()
    {
        Vector3 moveDir = transform.position - Vector3.zero;
        transform.position += moveDir * Time.deltaTime;
        transform.localScale += scaleFactor * Time.deltaTime;
    }
}
