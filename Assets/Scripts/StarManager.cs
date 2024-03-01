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
    [SerializeField] float rollSpeed;

    GameObject[] Stars;
    GameObject starsParent;
    

    private void OnEnable()
    {
        CustomInputManager.OnPressedQ += RollRight;
        CustomInputManager.OnPressedD += YawRight;
        CustomInputManager.OnPressedA += YawLeft;
        CustomInputManager.OnPressedS += PitchDown;
        CustomInputManager.OnPressedW += PitchUp;
        //CustomInputManager.OnMouseMove += Rotate;
        CustomInputManager.OnPressedLShift += TranslateAwayFromOrigin;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedQ -= RollRight;
        CustomInputManager.OnPressedD -= YawRight;
        CustomInputManager.OnPressedA -= YawLeft;
        CustomInputManager.OnPressedS -= PitchDown;
        CustomInputManager.OnPressedW -= PitchUp;
        //CustomInputManager.OnMouseMove -= Rotate;
        CustomInputManager.OnPressedLShift -= TranslateAwayFromOrigin;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Stars = new GameObject[totalStars];
        starsParent = Instantiate(new GameObject("Stars"));
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
            Stars[i].transform.position += -dir * (distance * 2) * Time.deltaTime;
        }
        // Translate stars in 2nd layer
        for (int i = 19; i < 39; i++)
        {
            Stars[i].transform.position += -dir * (distance * 2 / 3) * Time.deltaTime;
        }
        // Translate stars in 3rd layer
        for (int i = 39; i < totalStars; i++)
        {
            Stars[i].transform.position += dir * (distance * 2 / 5) * Time.deltaTime;
        }
    }

    private void YawLeft()
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

    private void YawRight()
    {
        for (int i = 0; i < 19; i++)
        {
            Stars[i].transform.position += Vector3.left * translateSpeed * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth)
                Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
        for (int i = 19; i < 39; i++)
        {
            Stars[i].transform.position += Vector3.left * (translateSpeed / 3) * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth)
                Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
        for (int i = 39; i < totalStars; i++)
        {
            Stars[i].transform.position += Vector3.left * (translateSpeed / 5) * Time.deltaTime;
            if (Stars[i].transform.position.x < -maxWidth)
                Stars[i].transform.position = new Vector3(maxWidth, Random.Range(-maxHeight, maxHeight));
        }
    }

    private void RollRight()
    {
        starsParent.transform.Rotate(new Vector3(0, 0, 1) * rollSpeed * Time.deltaTime);
    }

    private void RollLeft()
    {
        starsParent.transform.Rotate(new Vector3(0, 0, -1) * rollSpeed * Time.deltaTime);
    }

    private void PitchDown()
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
            Stars[i].transform.position += Vector3.down * (translateSpeed / 5) * Time.deltaTime;
            if (Stars[i].transform.position.y < -maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), maxHeight);
        }
    }

    private void PitchUp()
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
            Stars[i].transform.position += Vector3.up * (translateSpeed / 5) * Time.deltaTime;
            if (Stars[i].transform.position.y > maxHeight)
                Stars[i].transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), -maxHeight);
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

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-maxWidth, maxWidth), Random.Range(-maxHeight, maxHeight));
    }
}
