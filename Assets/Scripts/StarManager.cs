using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] int totalStars;
    [SerializeField] float maxWidth;
    [SerializeField] float maxHeight;
    [SerializeField] float translateSpeed;

    GameObject[] Stars;

    private void OnEnable()
    {
        CustomInputManager.OnPressedD += TranslateLeft;
        CustomInputManager.OnPressedA += TranslateRight;
        CustomInputManager.OnPressedW += TranslateDown;
        CustomInputManager.OnPressedS += TranslateUp;
        CustomInputManager.OnPressedLShift += TranslateAwayFromOrigin;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedD -= TranslateLeft;
        CustomInputManager.OnPressedA -= TranslateRight;
        CustomInputManager.OnPressedW -= TranslateDown;
        CustomInputManager.OnPressedS -= TranslateUp;
        CustomInputManager.OnPressedLShift -= TranslateAwayFromOrigin;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
        Stars = new GameObject[totalStars];
        GameObject starsParent = Instantiate(new GameObject("Stars"));
        for(int i = 0; i < totalStars; i++)
        {
            Stars[i] = Instantiate(starPrefab, RandomPos(), Quaternion.identity, starsParent.transform);
        }
    }

    private void TranslateLeft()
    {
        for(int i = 0; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.left * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth)
                Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
    }

    private void TranslateRight()
    {
        for (int i = 0; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.right * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.x > maxWidth)
                Stars[i].transform.position = new Vector3(-maxWidth, Random.Range(-maxHeight, maxHeight));
        }
    }

    private void TranslateUp()
    {
        for (int i = 0; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.up * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.y > maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), -maxHeight);
        }
    }

    private void TranslateDown()
    {
        for (int i = 0; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.down * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.y < -maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), maxHeight);
        }
    }

    private void TranslateAwayFromOrigin()
    {
        for (int i = 0; i < totalStars; i++)
        {
            Vector3 moveDir = Stars[i].transform.position - Vector3.zero;
            Stars[i].transform.position += moveDir * translateSpeed/2 * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth || Stars[i].transform.position.x > maxWidth || Stars[i].transform.position.y > maxHeight || Stars[i].transform.position.y < -maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), Random.Range(-maxHeight, maxHeight));
        }
    }


    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-maxWidth, maxWidth), Random.Range(-maxHeight, maxHeight));
    }
}
