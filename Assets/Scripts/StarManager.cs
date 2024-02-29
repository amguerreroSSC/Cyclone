using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] int totalStars; //60
    [SerializeField] int denomOfFractionInBackgroundLayer;
    [SerializeField] float maxWidth;
    [SerializeField] float maxHeight;
    [SerializeField] float translateSpeed;

    GameObject[] Stars;
    

    private void OnEnable()
    {
        CustomInputManager.OnPressedD += TranslateLeft;
        CustomInputManager.OnPressedA += TranslateRight;
        CustomInputManager.OnPressedS += TranslateTowardsOrigin;
        CustomInputManager.OnPressedW += TranslateAwayFromOrigin;
        CustomInputManager.OnMouseMove += Rotate;
        //CustomInputManager.OnPressedLShift += TranslateTowardsOrigin;
        //CustomInputManager.OnPressedSpace += TranslateAwayFromOrigin;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedD -= TranslateLeft;
        CustomInputManager.OnPressedA -= TranslateRight;
        CustomInputManager.OnPressedS -= TranslateTowardsOrigin;
        CustomInputManager.OnPressedW -= TranslateAwayFromOrigin;
        CustomInputManager.OnMouseMove -= Rotate;
        //CustomInputManager.OnPressedLShift -= TranslateTowardsOrigin;
        //CustomInputManager.OnPressedSpace -= TranslateAwayFromOrigin;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
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


    private void Rotate(Vector3 dir, float distance)
    {
        // Translate stars in 1st layer
        for (int i = 0; i < 19; i++)
        {
            Stars[i].transform.position += dir * distance * Time.deltaTime;
            //if (Stars[i].transform.position.x < -maxWidth)
            //    Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
        // Translate stars in 2nd layer
        for (int i = 19; i < 39; i++)
        {
            Stars[i].transform.position += dir * (distance / 3) * Time.deltaTime;
            //if (Stars[i].transform.position.x < -maxWidth)
            //    Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
        // Translate stars in 3rd layer
        for (int i = 39; i < totalStars; i++)
        {
            Stars[i].transform.position += -dir * (distance / 5) * Time.deltaTime;
            //if (Stars[i].transform.position.x > maxWidth)
            //    Stars[i].transform.position = new Vector3(-maxWidth, Random.Range(-maxHeight, maxHeight));
        }
    }

    private void TranslateLeft()
    {
        // Translate stars in 1st layer
        for(int i = 0; i < 19; i++)
        {
            Stars[i].transform.position += Vector3.left * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth)
                Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
        // Translate stars in 2nd layer
        for (int i = 19; i < 39; i++)
        {
            Stars[i].transform.position += Vector3.left * (translateSpeed / 3) * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth)
                Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
        // Translate stars in 3rd layer
        for (int i = 39; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.left * (translateSpeed / 5) * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth)
                Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
    }

    private void TranslateRight()
    {
        for (int i = 0; i < 19; i++)
        {
            Stars[i].transform.position += Vector3.right * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.x > maxWidth)
                Stars[i].transform.position = new Vector3(-maxWidth, Random.Range(-maxHeight, maxHeight));
        }
        for (int i = 19; i < 39; i++)
        {
            Stars[i].transform.position += Vector3.right * (translateSpeed / 3) * Time.deltaTime;
            if (Stars[i].transform.position.x > maxWidth)
                Stars[i].transform.position = new Vector3(-maxWidth, Random.Range(-maxHeight, maxHeight));
        }
        for (int i = 39; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.right * (translateSpeed / 5) * Time.deltaTime;
            if (Stars[i].transform.position.x > maxWidth)
                Stars[i].transform.position = new Vector3(-maxWidth, Random.Range(-maxHeight, maxHeight));
        }
    }

    private void TranslateUp()
    {
        for (int i = 0; i < 19; i++)
        {
            Stars[i].transform.position += Vector3.up * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.y > maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), -maxHeight);
        }
        for (int i = 19; i < 39; i++)
        {
            Stars[i].transform.position += Vector3.up * (translateSpeed / 3) * Time.deltaTime;
            if (Stars[i].transform.position.y > maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), -maxHeight);
        }
        for (int i = 39; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.up * (translateSpeed / 5) * Time.deltaTime;
            if (Stars[i].transform.position.y > maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), -maxHeight);
        }
    }

    private void TranslateDown()
    {
        for (int i = 0; i < 19; i++)
        {
            Stars[i].transform.position += Vector3.down * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.y < -maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), maxHeight);
        }
        for (int i = 19; i < 39; i++)
        {
            Stars[i].transform.position += Vector3.down * (translateSpeed / 3) * Time.deltaTime;
            if (Stars[i].transform.position.y < -maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), maxHeight);
        }
        for (int i = 39; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.down * (translateSpeed / 5) * Time.deltaTime;
            if (Stars[i].transform.position.y < -maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), maxHeight);
        }
    }

    private void TranslateAwayFromOrigin()
    {
        float halfMaxWidth = maxWidth / 2;
        float halfMaxHeight = maxHeight / 2;
        for (int i = 0; i < totalStars; i++)
        {
            Vector3 moveDir = Stars[i].transform.position - Vector3.zero;
            Stars[i].transform.position += moveDir * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth || Stars[i].transform.position.x > maxWidth || Stars[i].transform.position.y > maxHeight || Stars[i].transform.position.y < -maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-halfMaxWidth, halfMaxWidth), Random.Range(-halfMaxHeight, halfMaxHeight));
        }
    }

    private void TranslateTowardsOrigin()
    {
        for (int i = 0; i < totalStars; i++)
        {
            Vector3 moveDir = Vector3.zero - Stars[i].transform.position;
            Stars[i].transform.position += moveDir * (translateSpeed / 2) * Time.deltaTime;
            if (Stars[i].transform.position.x > (-maxWidth / Random.Range(1, 100)) && Stars[i].transform.position.x < (maxWidth / Random.Range(1, 100)) && Stars[i].transform.position.y > (-maxHeight / Random.Range(1, 100)) && Stars[i].transform.position.y < (maxHeight / Random.Range(1, 100)))
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), Random.Range(-maxHeight, maxHeight));
        }
    }


    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-maxWidth, maxWidth), Random.Range(-maxHeight, maxHeight));
    }
}
