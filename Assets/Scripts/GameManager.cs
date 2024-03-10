using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] StarManager starManager;

    [SerializeField] GameObject titleText;
    [SerializeField] GameObject loseText;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] TextMeshProUGUI scoreText;

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
        CustomInputManager.OnPressedR += Restart;
        CustomInputManager.OnPressedSpace += StartSimulation;
        CustomInputManager.OnPressedEscape += Application.Quit;
    }

    private void OnDisable()
    {
        CustomInputManager.OnPressedR -= Restart;
        CustomInputManager.OnPressedSpace -= StartSimulation;
        CustomInputManager.OnPressedEscape -= Application.Quit;
    }

    private void StartSimulation()
    {
        CustomInputManager.OnPressedSpace -= StartSimulation;
        isGameOver = false;
        Score = 0;
        titleText.SetActive(false);
        startText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        starManager.StartSimulation();
        enemyManager.StartSimulation();
        StartCoroutine(StartScoring());
    }

    private IEnumerator StartScoring()
    {
        while(isGameOver != true)
        {
            yield return new WaitForSeconds(0.5f);
            Score++;
            scoreText.text = Score.ToString();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        starManager.StopSimulation();
        enemyManager.StopSimulation();
        StopCoroutine(StartScoring());
        loseText.gameObject.SetActive(true);
        startText.gameObject.SetActive(true);
        startText.text = "Press R to Restart";
        Score -= 1;// Score doesn't go up after game ends
    }

    private void Restart()
    {
        if (isGameOver == true)
        {
            loseText.gameObject.SetActive(false);
            startText.gameObject.SetActive(false);
            enemyManager.Restart();
            Score = 0;
            scoreText.text = Score.ToString(); //Updates score back to 0 when game restarts
            StartSimulation();
        }
    }
}
