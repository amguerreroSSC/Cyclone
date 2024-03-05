using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float size;
    [SerializeField] Vector3 speed;
    EnemyManager enemyManager;    

    private void OnEnable()
    {
        transform.localScale = new Vector3(0.01f, 0.01f);
        StartCoroutine(Move());
    }

    // Gets called when game is started
    public void Initialize(EnemyManager enemyManagerInstance)
    {
        enemyManager = enemyManagerInstance;
    }

    private IEnumerator Move()
    {
        while (transform.localScale.x < size)
        {
            transform.localScale += speed * Time.deltaTime;
            yield return null;
        }
        if(transform.position.x < 3 && transform.position.x > -3 && transform.position.y < 5.5 && transform.position.y > -5.5)
        {
            Debug.Log("game over");
        }
        else
        {
            enemyManager.Release(this);
        }
    }
}
