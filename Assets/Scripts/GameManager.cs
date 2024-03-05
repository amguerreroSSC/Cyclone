using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] StarManager starManager;
    public static GameManager Instance { get; private set; }

    // gameplay fields
    public bool isGameOver { get; private set; } = false;
    public int Score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void OnEnable()
    {
        CustomInputManager.OnPressedSpace += StartSimulation;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedSpace -= StartSimulation;
    }

    private void StartSimulation()
    {
        starManager.StartSimulation();
        enemyManager.StartSimulation();
    }


}
