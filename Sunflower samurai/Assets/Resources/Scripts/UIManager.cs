using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using GoogleMobileAds.Api;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public int allCoins;
    public int artifacts;
    public Progress[] progressLvls;
    public bool isMusic = true;
    public bool isSound = true;
    [SerializeField]
    Text coinCounter;
    [SerializeField]
    Text ArtifactCounter;
    int tryValue;
    [SerializeField]
    Text tryCounter;
    [SerializeField]
    public Text scoreDeadCanvas;
    [SerializeField]
    public Text bestScoreDeadCanvas;
    [SerializeField]
    GameObject LevelCompleteCanvas;
    [SerializeField]
    GameObject DeadCanvas;
    [SerializeField]
    Transform StarsBackgroundLevel3;
    [SerializeField]
    InterAd adTransition;
    public bool IsSound { get => isSound; set => isSound = value; }
    public void GoToMainMenu()
    {
        PlayerPrefs.SetInt("TryValue", 0);
        if (isMusic)
        {
            SoundManager.instance.Mixer.audioMixer.SetFloat("Music", 0);
        }
        SoundManager.instance.StatusButtonPlay();
        SceneManager.LoadScene(1);
    }
    public void Restart()
    {
        if (isMusic)
        {
            SoundManager.instance.Mixer.audioMixer.SetFloat("Music", 0);
        }
        SoundManager.instance.StatusButtonPlay();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("TryValue", 0);
        if (isMusic)
        {
            SoundManager.instance.Mixer.audioMixer.SetFloat("Music", 0);
        }
        SoundManager.instance.StatusButtonPlay();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    public void Die()
    {
        if (isMusic)
        {
            SoundManager.instance.Mixer.audioMixer.SetFloat("Music", -10);
        }
        if (SceneManager.GetActiveScene().buildIndex != 8)
        {
            tryValue = PlayerPrefs.GetInt("TryValue") + 1;
            PlayerPrefs.SetInt("TryValue", tryValue);
            if (tryValue % 5 == 0)
            {
                if (PlayerPrefs.GetInt("noAds") != 1)
                    adTransition.ShowAd();
            }
            tryCounter.text = "Try: " + PlayerPrefs.GetInt("TryValue");
            tryCounter.rectTransform.anchoredPosition = new Vector2(0, -144f);
        }
        else
        {
            tryValue = PlayerPrefs.GetInt("TryValue") + 1;
            PlayerPrefs.SetInt("TryValue", tryValue);
            if (tryValue % 5 == 0)
            {
                if (PlayerPrefs.GetInt("noAds") != 1)
                    adTransition.ShowAd();
            }
        }
        DeadCanvas.SetActive(true);
    }
    public void LevelComplete()
    {
        tryValue = PlayerPrefs.GetInt("TryValue") + 1;
        PlayerPrefs.SetInt("TryValue", tryValue);
        if (tryValue % 5 == 0)
        {
            if (PlayerPrefs.GetInt("noAds") != 1)
                adTransition.ShowAd();
        }
        if (isMusic)
        {
            SoundManager.instance.Mixer.audioMixer.SetFloat("Music", -10);
        }
        SoundManager.instance.LevelCompleteCharacterPlay();
        tryCounter.text = "Try: " + PlayerPrefs.GetInt("TryValue");
        tryCounter.rectTransform.anchoredPosition = new Vector2(0, -144f);
        LevelCompleteCanvas.SetActive(true);
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        if (GameObject.Find("Coin") == null && ArtifactCounter.text == "3/3")
        {
            progressLvls[currentLevel - 1] = Progress.Gold;
        }
        else if (GameObject.Find("Coin") == null || ArtifactCounter.text == "3/3")
        {
            progressLvls[currentLevel - 1] = Progress.Silver;
        }
        else
        {
            progressLvls[currentLevel - 1] = Progress.Copper;
        }
        if (PlayerPrefs.HasKey("proggressLvls" + (currentLevel - 1)))
        {
            if (PlayerPrefs.GetInt("proggressLvls" + (currentLevel - 1)) < (int)progressLvls[currentLevel - 1])
                PlayerPrefs.SetInt("proggressLvls" + (currentLevel - 1), (int)progressLvls[currentLevel - 1]);
        }
        else
        {
            PlayerPrefs.SetInt("proggressLvls" + (currentLevel - 1), (int)progressLvls[currentLevel - 1]);
        }
        if (currentLevel >= PlayerPrefs.GetInt("UnlockLevel"))
        {
            if (currentLevel != 6)
            {
                PlayerPrefs.SetInt("UnlockLevel", currentLevel + 1);
            }
            else
            {
                bool infinityLVL = true;
                for (int i = 0; i < progressLvls.Length && infinityLVL; i++)
                {
                    if ((Progress)PlayerPrefs.GetInt("proggressLvls" + i) >= Progress.Silver)
                    {
                        infinityLVL = true;
                    }
                    else
                    {
                        infinityLVL = false;
                    }
                }
                if (infinityLVL)
                {
                    PlayerPrefs.SetInt("UnlockLevel", currentLevel + 1);
                }
            }
        }
    }

    public void HideLevelCompleteCanvas()
    {
        if (LevelCompleteCanvas != null)
            LevelCompleteCanvas.SetActive(false);
    }
    public void HideDeadCanvas()
    {
        if (DeadCanvas != null)
            DeadCanvas.SetActive(false);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            if (SceneManager.GetActiveScene().buildIndex != 8)
            {
                HideLevelCompleteCanvas();
            }
            HideDeadCanvas();
        }
        gameObject.GetComponent<UIManager>().enabled = true;
    }
    void Start()
    {
        progressLvls = new Progress[6];
        //PlayerPrefs.SetInt("Coins", 150);
        if (PlayerPrefs.HasKey("Coins"))
        {
            allCoins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            allCoins = 0;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

        }
        else
        {
            artifacts = 0;
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 8)
        {
            ArtifactCounter.text = artifacts + "/3";
        }
        coinCounter.text = " " + allCoins;
        if (PlayerPrefs.HasKey("Music"))
            isMusic = PlayerPrefs.GetInt("Music") == 1;
        else
            isMusic = true;
        if (PlayerPrefs.HasKey("Master Sound"))
            isSound = PlayerPrefs.GetInt("Master Sound") == 1;
        else
            isSound = true;

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            StarsBackgroundLevel3.position = new Vector3(SunflowerSamurai.instance.transform.position.x + 2.3f, StarsBackgroundLevel3.position.y, StarsBackgroundLevel3.position.z);
        }
#if UNITY_STANDALONE
        PlayerPrefs.SetInt("noAds", 1);
        if (Input.GetKeyDown(KeyCode.R) && SunflowerSamurai.instance.state == GameState.Dead)
        {
            Restart();
        }
#elif UNITY_ANDROID || UNITY_IOS
PlayerPrefs.SetInt("noAds", 0);
#endif
    }
}