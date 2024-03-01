using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float translateSpeed;
    [SerializeField] Vector3 scaleFactor;
    [SerializeField] float lifetime;
    EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = EnemyManager.Instance;
    }

    private void OnEnable()
    { 
        //StartCoroutine(Accelerate());
        transform.localScale = new Vector3(0.01f, 0.01f);
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyManager.Release(this);
    }

}
