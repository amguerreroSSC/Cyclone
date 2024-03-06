using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float Size;
    [SerializeField] Vector3 Speed;
    [SerializeField] Vector3 RotationSpeed;
    [SerializeField] float TranslationSpeed;
    [SerializeField] List<Sprite> sprites;
    GameManager gameManager;
    SpriteRenderer spriteRenderer;
    int spriteListLength;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        transform.localScale = Constants.Vector3Zero;
        spriteRenderer.sprite = sprites[Random.Range(0, spriteListLength)];
        StartCoroutine(Move());
    }

    private void Start()
    { 
        spriteListLength = sprites.Count;
    }

    private IEnumerator Move()
    {
        while (transform.localScale.x < Size)
        {
            transform.position += (transform.position - Constants.Vector3Zero) * TranslationSpeed * Time.deltaTime;
            transform.localScale += Speed * Time.deltaTime;
            transform.Rotate(RotationSpeed);
            yield return null;
        }
        if (transform.position.x > -7.5f && transform.position.x < 7.5f && transform.position.y > -5f && transform.position.y < 5f)
            GameManager.Instance.GameOver();
        else
            gameObject.SetActive(false);
    }
    

}
