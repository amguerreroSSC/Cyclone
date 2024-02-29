using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float lifetime;
    [SerializeField] Vector3 scaleFactor;

    // Update is called once per frame

    private void OnEnable()
    {
        StartCoroutine(Accelerate());
    }

    private IEnumerator Accelerate()
    {
        float elapsedTime = 0;
        while(elapsedTime < lifetime)
        {
            transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
            transform.localScale += scaleFactor * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    
}
