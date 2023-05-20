using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBox : MonoBehaviour
{
    [Header("Box Fields")]
    [SerializeField] private TextMeshProUGUI date;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI enemies;
    [SerializeField] private TextMeshProUGUI completionTime;

   public void WriteInfo(string dateD, string timeD, string scoreD, string damageD, string enemyD, string completionTimeD)
    {
        date.text = dateD;
        time.text = timeD;
        score.text = scoreD;
        damage.text = damageD;
        enemies.text = enemyD;
        completionTime.text = completionTimeD;
    }
}
