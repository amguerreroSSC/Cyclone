using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] int totalStars;
    [SerializeField] int parallaxLayers;
    
    [SerializeField] float maxWidth;
    [SerializeField] float maxHeight;
    [SerializeField] float translateSpeed;
    [SerializeField] float rollSpeed;

    Transform[] Stars;
    int[,] LayerRanges;
    GameObject starsParent;
    float parallaxLayersReciprocal;
    float starsPerLayer;
    Vector3 randomPosHolder;


    private void OnEnable()
    {
        CustomInputManager.OnPressedQ += RollRight;
        CustomInputManager.OnPressedE += RollLeft;
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
        CustomInputManager.OnPressedE -= RollLeft;
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
        // Calculate these variables ahead of time;
        parallaxLayersReciprocal = 1f / parallaxLayers;
        starsPerLayer = totalStars * parallaxLayersReciprocal;

        // Calculate parallax layer chunk mins and maxes ahead of time and put them in a multidimensional array
        LayerRanges = new int[parallaxLayers, 2];
        for(int i = 0; i < parallaxLayers; i++)
        {
            LayerRanges[i, 0] = Mathf.RoundToInt(starsPerLayer * i);
            LayerRanges[i, 1] = Mathf.RoundToInt(starsPerLayer * (i + 1));
        }

        // Create a new array of transforms and populate it with number of stars equal to totalStars
        Stars = new Transform[totalStars];
        starsParent = Instantiate(new GameObject("Stars"));
        for(int i = 0; i < totalStars; i++)
        {
            Stars[i] = Instantiate(starPrefab, RandomPos(1), Quaternion.identity, starsParent.transform).GetComponent<Transform>();
        }
        
    }


    //private void Rotate(Vector3 dir, float distance)
    //{
    //    
    //    for (int i = 0; i < 19; i++)
    //    {
    //        Stars[i].transform.position += -dir * (distance * 2) * Time.deltaTime;
    //    }
    //    
    //    for (int i = 19; i < 39; i++)
    //    {
    //        Stars[i].transform.position += -dir * (distance * 2 / 3) * Time.deltaTime;
    //    }
    //    
    //    for (int i = 39; i < totalStars; i++)
    //    {
    //        Stars[i].transform.position += dir * (distance * 2 / 5) * Time.deltaTime;
    //    }
    //}

    private void YawLeft()
    {
        for (int i = 0; i < parallaxLayers; i++)
        {
            float speed = translateSpeed / (i + 1);
            for (int j = LayerRanges[i, 0]; j < LayerRanges[i, 1]; j++)
            {
                Stars[j].position += Constants.Vector3Right * speed * Time.deltaTime;
                if (Stars[j].position.x > maxWidth)
                    Stars[j].position = RandomPosW();
            }
        }
    }

    private void YawRight()
    {
        for (int i = 0; i < parallaxLayers; i++)
        {
            float speed = translateSpeed / (i + 1);
            for (int j = LayerRanges[i, 0]; j < LayerRanges[i, 1]; j++)
            {
                Stars[j].position += Constants.Vector3Left * speed * Time.deltaTime;
                if (Stars[j].position.x < -maxWidth)
                    Stars[j].position = RandomPosE();
            }
        }
    }

    private void RollRight()
    {
        starsParent.transform.Rotate(Constants.Vector3Forward * rollSpeed * Time.deltaTime);
    }

    private void RollLeft()
    {
        starsParent.transform.Rotate(Constants.Vector3Back * rollSpeed * Time.deltaTime);
    }

    private void PitchDown()
    {
        for (int i = 0; i < parallaxLayers; i++)
        {
            float speed = translateSpeed / (i + 1);
            for (int j = LayerRanges[i, 0]; j < LayerRanges[i, 1]; j++)
            {
                Stars[j].position += Constants.Vector3Up * speed * Time.deltaTime;
                if (Stars[j].position.y > maxHeight)
                    Stars[j].position = RandomPosS();
            }
        }
    }

    private void PitchUp()
    {
        for (int i = 0; i < parallaxLayers; i++)
        {
            float speed = translateSpeed / (i + 1);
            for (int j = LayerRanges[i, 0]; j < LayerRanges[i, 1]; j++)
            {
                Stars[j].position += Constants.Vector3Down * speed * Time.deltaTime;
                if (Stars[j].position.y < -maxHeight)
                    Stars[j].position = RandomPosN();
            }
        }
    }

    private void TranslateAwayFromOrigin()
    {
        for (int i = 0; i < totalStars; i++)
        {
            Stars[i].position += (Stars[i].position - Vector3.zero) * Time.deltaTime;
            if (Stars[i].position.x < -maxWidth || Stars[i].position.x > maxWidth || Stars[i].position.y > maxHeight || Stars[i].position.y < -maxHeight)
                Stars[i].position = RandomPos(0.5f);
        }
    }

    private Vector3 RandomPosN()
    {
        randomPosHolder.x = Random.Range(-maxWidth, maxWidth);
        randomPosHolder.y = maxHeight;
        return randomPosHolder;
    }

    private Vector3 RandomPosS()
    {
        randomPosHolder.x = Random.Range(-maxWidth, maxWidth);
        randomPosHolder.y = -maxHeight;
        return randomPosHolder;
    }

    private Vector3 RandomPosE()
    {
        randomPosHolder.x = maxWidth;
        randomPosHolder.y = Random.Range(-maxHeight, maxHeight);
        return randomPosHolder;
    }

    private Vector3 RandomPosW()
    {
        randomPosHolder.x = -maxWidth;
        randomPosHolder.y = Random.Range(-maxHeight, maxHeight);
        return randomPosHolder;
    }

    private Vector3 RandomPos(float scale)
    {
        randomPosHolder.x = Random.Range(-maxWidth, maxWidth) * scale;
        randomPosHolder.y = Random.Range(-maxHeight, maxHeight) * scale;
        return randomPosHolder;
    }
}
