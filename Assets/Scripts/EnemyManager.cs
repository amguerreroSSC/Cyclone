using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyPoolStartCap;
    [SerializeField] float initialSpawnRate;
    [SerializeField] float maxSpawnrate;
    [SerializeField] float spawnAreaWidth;
    [SerializeField] float spawnAreaHeight;
    [SerializeField] float rollSpeed;
    [SerializeField] float translateSpeed;

    List<Transform> Enemies;
    Transform enemiesParent;
    GameManager gameManager;

    private void Awake()
    {
        // Make sure this is the only instance of this class (Singleton)
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        Initialize();
    }

    private void Initialize()
    {   
        Enemies = new List<Transform>(enemyPoolStartCap);
        enemiesParent = Instantiate(new GameObject("Enemies")).GetComponent<Transform>();
        for (int i = 0; i < enemyPoolStartCap; i++)
        {
            GameObject enemyInstance = Instantiate(enemyPrefab, RandomPos(), Constants.QuaternionIdentity, enemiesParent);
            Enemies.Add(enemyInstance.GetComponent<Transform>());
            enemyInstance.SetActive(false);
        }
    }

    public void StartSimulation()
    {
        CustomInputManager.OnPressedQ += RollLeft;
        CustomInputManager.OnPressedE += RollRight;
        CustomInputManager.OnPressedD += TranslateLeft;
        CustomInputManager.OnPressedA += TranslateRight;
        CustomInputManager.OnPressedS += TranslateUp;
        CustomInputManager.OnPressedW += TranslateDown;
        StartCoroutine(StartSpawningEnemies());
    }

    public void StopSimulation()
    {
        CustomInputManager.OnPressedQ -= RollLeft;
        CustomInputManager.OnPressedE -= RollRight;
        CustomInputManager.OnPressedD -= TranslateLeft;
        CustomInputManager.OnPressedA -= TranslateRight;
        CustomInputManager.OnPressedS -= TranslateUp;
        CustomInputManager.OnPressedW -= TranslateDown;
    }

    private IEnumerator StartSpawningEnemies()
    {
        int spawned = 0;
        float spawnRate = initialSpawnRate;
        int spawnCount = 1;
        while(gameManager.isGameOver != true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnEnemy(spawnCount);
            spawned++;
            if (spawned % 5 == 0)
            {
                spawnCount++;
                if(spawnRate > maxSpawnrate)
                {
                    spawnRate *= 0.9f;
                    translateSpeed *= 1.1f;
                }
            }
        }
    }

    private void SpawnEnemy(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Transform enemy = GetEnemy();
            enemy.position = RandomPos();
            enemy.gameObject.SetActive(true);
        }
    }

    private Transform GetEnemy()
    {
        for(int i = 0; i < enemyPoolStartCap; i++)
        {
            if (Enemies[i].gameObject.activeSelf == false)
                return Enemies[i];
        }
        return CloneEnemy();
    }

    private Transform CloneEnemy()
    {
        GameObject newEnemyInstance = Instantiate(enemyPrefab, RandomPos(), Constants.QuaternionIdentity, enemiesParent);
        Enemies.Add(newEnemyInstance.GetComponent<Transform>());
        newEnemyInstance.SetActive(false);
        return newEnemyInstance.GetComponent<Transform>();
    }

    public void Release(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void TranslateUp()
    {
        for(int i = 0; i < enemyPoolStartCap; i++)
        {
            Enemies[i].position += Constants.Vector3Up * translateSpeed * Time.deltaTime;
        }
    }

    private void TranslateDown()
    {
        for (int i = 0; i < enemyPoolStartCap; i++)
        {
            Enemies[i].position += Constants.Vector3Down * translateSpeed * Time.deltaTime;
        }
    }

    private void TranslateRight()
    {
        for (int i = 0; i < enemyPoolStartCap; i++)
        {
            Enemies[i].position += Constants.Vector3Right * translateSpeed * Time.deltaTime;
        }
    }

    private void TranslateLeft()
    {
        for (int i = 0; i < enemyPoolStartCap; i++)
        {
            Enemies[i].position += Constants.Vector3Left * translateSpeed * Time.deltaTime;
        }
    }

    private void RollRight()
    {
        enemiesParent.Rotate(Constants.Vector3Forward * rollSpeed * Time.deltaTime);
    }

    private void RollLeft()
    {
        enemiesParent.Rotate(Constants.Vector3Back * rollSpeed * Time.deltaTime);
    }

    //private void Rotate(Vector3 dir, float distance)
    //{
    //    for (int i = 0; i < enemyPoolStartCap; i++)
    //    {
    //        Enemies[i].position += dir * distance * Time.deltaTime;
    //    }
        
    //}

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-spawnAreaHeight, spawnAreaWidth), Random.Range(-spawnAreaHeight, spawnAreaHeight));
    }

    public void Restart()
    {
        int listCount = Enemies.Count;
        for (int i = 0; i < listCount; i++)
        {
            Enemies[i].gameObject.SetActive(false);
        }
    }
}
