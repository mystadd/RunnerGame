using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSeconds;

    public bool scoreIncreasing;    

    private void Start()
    {        
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    private void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSeconds * Time.deltaTime;
        }
        
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = Mathf.Round(scoreCount).ToString();
        highScoreText.text = Mathf.Round(highScoreCount).ToString();
    }
}
