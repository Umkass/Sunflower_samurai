using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class DeveloperLevelUnlocker : MonoBehaviour
{
    [SerializeField]
    public Button[] levelButtons;

    public void Reset()
    {
        SoundManager.instance.StatusButtonPlay();
        for (int i = 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UnlockAllLevels()
    {
        SoundManager.instance.StatusButtonPlay();
        for (int i = 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = true;
        }
        PlayerPrefs.SetInt("UnlockLevel", 7);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Awake()
    {
        for (int i = 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }
    void Start()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("UnlockLevel"); i++)
        {
            levelButtons[i].interactable = true;
        }
    }
}
