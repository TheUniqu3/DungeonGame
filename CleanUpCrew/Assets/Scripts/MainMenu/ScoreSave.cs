using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.IO;
public class ScoreSave : MonoBehaviour
{
    private string txtDocumentName;

    [Header("Scroll")]
    public Transform contentWindow;
    private ScoreBox scoreBox;
    public ScoreBox box;
    void Start()
    {
        InitialiseScoreFile();
    }

    public void InitialiseScoreFile()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/HighScore/");
        txtDocumentName = Application.streamingAssetsPath + "/HighScore/" + "HighScoreFile" + ".txt";
        if (!File.Exists(txtDocumentName))
        {
        }
    }
    public void SaveScoreToFile(int Score,int damage, int enemy,float time)
    {
        string todayDate = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
        File.AppendAllText(txtDocumentName, todayDate + " " + Score + " " + damage + " " + enemy + " " + time + Environment.NewLine);
    }

    public void GetScoreFromFile()
    {
        List<String> fileLines = File.ReadLines(txtDocumentName).ToList();
        foreach(string line in fileLines) 
        {
            ScoreBox scoreBox;
            string[] fields = line.Split(' ');
            scoreBox = Instantiate(box, contentWindow);
            scoreBox.WriteInfo(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);
        }
    }
}
