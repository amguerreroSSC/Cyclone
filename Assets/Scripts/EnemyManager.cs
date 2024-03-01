using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyPoolCap;
    [SerializeField] float initialSpawnRate;
    [SerializeField] float spawnAreaWidth;
    [SerializeField] float spawnAreaHeight;
    [SerializeField] float rollSpeed;
    [SerializeField] float translateSpeed;
    [SerializeField] Vector3 scaleSpeed;


    List<GameObject> Enemies;
    GameObject enemiesParent;
    float spawnRate;
    bool gameOver = false;

    private void Awake()
    {
        // Make sure this is the only instance of this class (Singleton)
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void OnEnable()
    {
        CustomInputManager.OnPressedQ += RollLeft;
        CustomInputManager.OnPressedD += TranslateLeft;
        CustomInputManager.OnPressedA += TranslateRight;
        CustomInputManager.OnPressedS += TranslateUp;
        CustomInputManager.OnPressedW += TranslateDown;
        CustomInputManager.OnPressedLShift += MoveAwayFromOrigin;
        //CustomInputManager.OnMouseMove += Rotate;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedQ -= RollLeft;
        CustomInputManager.OnPressedD -= TranslateLeft;
        CustomInputManager.OnPressedA -= TranslateRight;
        CustomInputManager.OnPressedS -= TranslateUp;
        CustomInputManager.OnPressedW -= TranslateDown;
        CustomInputManager.OnPressedLShift -= MoveAwayFromOrigin;
        //CustomInputManager.OnMouseMove -= Rotate;
    }

    void Start()
    {
        Initialize();
        StartCoroutine(StartSpawningEnemies());
    }

    private void Initialize()
    {
        Enemies = new List<GameObject>(enemyPoolCap);
        enemiesParent = Instantiate(new GameObject("Enemies"));
        for (int i = 0; i < enemyPoolCap; i++)
        {
            GameObject enemyInstance = Instantiate(enemyPrefab, RandomPos(), Quaternion.identity, enemiesParent.transform);
            enemyInstance.SetActive(false);
            Enemies.Add(enemyInstance);
        }
    }

    private IEnumerator StartSpawningEnemies()
    {
        float elapsedTime = 0;
        float lastSpawnTime = 0;
        while(gameOver != true)
        {
            if(elapsedTime > lastSpawnTime + initialSpawnRate)
            {
                SpawnEnemy();
                lastSpawnTime = elapsedTime;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = GetEnemy();
        enemy.transform.position = RandomPos();
        enemy.SetActive(true);
    }

    private GameObject GetEnemy()
    {
        for(int i = 0; i < enemyPoolCap; i++)
        {
            if (Enemies[i].activeSelf == false)
                return Enemies[i];
        }
        return CloneEnemy();
    }

    private GameObject CloneEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, RandomPos(), Quaternion.identity, enemiesParent.transform);
        Enemies.Add(newEnemy);
        return newEnemy;
    }

    public void Release(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void TranslateUp()
    {
        for(int i = 0; i < enemyPoolCap; i++)
        {
            Enemies[i].transform.position += Vector3.up * translateSpeed * Time.deltaTime;
        }
    }

    private void TranslateDown()
    {
        for (int i = 0; i < enemyPoolCap; i++)
        {
            Enemies[i].transform.position += Vector3.down * translateSpeed * Time.deltaTime;
        }
    }

    private void TranslateRight()
    {
        for (int i = 0; i < enemyPoolCap; i++)
        {
            Enemies[i].transform.position += Vector3.right * translateSpeed * Time.deltaTime;
        }
    }

    private void TranslateLeft()
    {
        for (int i = 0; i < enemyPoolCap; i++)
        {
            Enemies[i].transform.position += Vector3.left * translateSpeed * Time.deltaTime;
        }
    }

    private void RollRight()
    {
        enemiesParent.transform.Rotate(new Vector3(0, 0, 1) * rollSpeed * Time.deltaTime);
    }

    private void RollLeft()
    {
        enemiesParent.transform.Rotate(new Vector3(0, 0, -1) * rollSpeed * Time.deltaTime);
    }

    private void MoveAwayFromOrigin()
    {
        for (int i = 0; i < enemyPoolCap; i++)
        {
            Vector3 moveDir = Enemies[i].transform.position - Vector3.zero;
            Enemies[i].transform.position += moveDir * Time.deltaTime;
            Enemies[i].transform.localScale += scaleSpeed * Time.deltaTime;
        }
    }

    private void Rotate(Vector3 dir, float distance)
    {
        for (int i = 0; i < enemyPoolCap; i++)
        {
            Enemies[i].transform.position += dir * distance * Time.deltaTime;
        }
        
    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-spawnAreaHeight, spawnAreaWidth), Random.Range(-spawnAreaHeight, spawnAreaHeight));
    }
}
