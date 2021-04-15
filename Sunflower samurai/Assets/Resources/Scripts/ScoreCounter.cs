using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    Text score;
    [SerializeField]
    Transform Character;
    int scoreValue;
    void Start()
    {
        score.text = "Score: " + 0;
    }
    void Update()
    {
        if (SunflowerSamurai.instance != null)
        {
            if (SunflowerSamurai.instance.state == GameState.Playing)
            {
                if (Character.transform.position.x >= 0.0f)
                {
                    scoreValue = (int)Character.transform.position.x;
                    score.text = "Score: " + scoreValue;
                    UIManager.instance.scoreDeadCanvas.text = score.text;
                    if (PlayerPrefs.HasKey("BestScore"))
                    {
                        if (scoreValue > PlayerPrefs.GetInt("BestScore"))
                        {
                            PlayerPrefs.SetInt("BestScore", scoreValue);
                            UIManager.instance.bestScoreDeadCanvas.text = "Best score: " + scoreValue;
                        }
                        else
                        {
                            UIManager.instance.bestScoreDeadCanvas.text = "Best score: " + PlayerPrefs.GetInt("BestScore");
                        }
                    }
                    else
                    {
                        UIManager.instance.bestScoreDeadCanvas.text = "Best score: " + scoreValue;
                        PlayerPrefs.SetInt("BestScore", scoreValue);
                    }
                }
            }
        }
    }
}
