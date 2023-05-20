using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("Score Variables")]

    public int score;
    private int currentScore;
    public int enemyKilled;
    public int completionTime;
    public int damageDealt;

    [SerializeField] private TextMeshProUGUI ScoreText;

    [SerializeField] private ScoreSave scoreSave;

    private void Start()
    {
        InitVariables();
        scoreSave = gameObject.GetComponent<ScoreSave>();
    }
    private void InitVariables()
    {
        currentScore = 0;
        completionTime = 0;
        enemyKilled = 0;
        damageDealt = 0;
    }
    public void AddToDamage(int addDamage)
    {
        damageDealt += addDamage;
    }
    public void AddToScore(int scorepoints)
    {
        currentScore += scorepoints;
        ScoreText.text = currentScore.ToString();
    }
    public void Enemykilled()
    {
        enemyKilled += 1;
    }
    public void SaveTime()
    {
        completionTime = (int)Mathf.Round(Time.time);
        Save(currentScore, damageDealt, enemyKilled, completionTime);
    }
    public void Save(int currentScore, int damageDealt, int enemyKilled, int completionTime)
    {
        scoreSave.SaveScoreToFile(currentScore, damageDealt, enemyKilled, completionTime);
    }
}
