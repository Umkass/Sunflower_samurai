using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject MenuCanvas;
    [SerializeField]
    GameObject SettingCanvas;
    [SerializeField]
    GameObject LevelsCanvas;
    [SerializeField]
    GameObject CharacterCanvas;
    [SerializeField]
    GameObject QuestionCanvas;
    [SerializeField]
    Image spriteRendererDzin;
    [SerializeField]
    Image spriteRendererMugen;
    [SerializeField]
    Image spriteRendererFuu;
    [SerializeField]
    Image spriteRendererSound;
    [SerializeField]
    Image spriteRendererMusic;
    [SerializeField]
    Image[] spriteRenderlevels;
    [SerializeField]
    Image spriteRendererMenuCanvasCharacterImage;
    [SerializeField]
    GameObject SelectButton;
    [SerializeField]
    GameObject BuyButton;
    Sprite soundOff;
    Sprite soundOn;
    Sprite musicOff;
    Sprite musicOn;
    Sprite DzinOff;
    Sprite DzinOn;
    Sprite MugenOff;
    Sprite MugenOn;
    Sprite FuuOff;
    Sprite FuuOn;
    bool МugenBought;
    bool FuuBought;
    public Character characterPressed = Character.Dzin;
    public Character currentCharacter = Character.Dzin;
    int priceMugen = 15;
    int priceFuu = 20;
    Sprite levelGold;
    Sprite levelCopper;
    Sprite levelSilver;
    [SerializeField]
    GameObject noAds;
    // Start is called before the first frame update
    public void Play()
    {
        SoundManager.instance.StatusButtonPlay();
        MenuCanvas.SetActive(false);
        LevelsCanvas.SetActive(true);
        for (int i = 0; i < UIManager.instance.progressLvls.Length; i++)
        {
            if ((Progress)PlayerPrefs.GetInt("proggressLvls" + i) == Progress.Copper)
                spriteRenderlevels[i].sprite = levelCopper;
            if ((Progress)PlayerPrefs.GetInt("proggressLvls" + i) == Progress.Silver)
                spriteRenderlevels[i].sprite = levelSilver;
            if ((Progress)PlayerPrefs.GetInt("proggressLvls" + i) == Progress.Gold)
                spriteRenderlevels[i].sprite = levelGold;
        }
    }
    public void ChoiceLVL(int caption)
    {
        PlayerPrefs.SetInt("TryValue", 0);
        SoundManager.instance.StatusButtonPlay();
        int i = caption;
        if (i > 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + i);
        else
            Debug.Log("Error_of_scene");
    }
    public void Settings()
    {
        SoundManager.instance.StatusButtonPlay();
        MenuCanvas.SetActive(false);
        SettingCanvas.SetActive(true);
        if (PlayerPrefs.GetInt("noAds") == 1)
            noAds.SetActive(false);
    }

    public void Question()
    {
        SoundManager.instance.StatusButtonPlay();
        SettingCanvas.SetActive(false);
        QuestionCanvas.SetActive(true);
    }
    public void Characters()
    {
        SoundManager.instance.StatusButtonPlay();
        MenuCanvas.SetActive(false);
        CharacterCanvas.SetActive(true);
    }
    public void SelectDzin()
    {
        SoundManager.instance.StatusButtonPlay();
        spriteRendererDzin.sprite = DzinOn;
        spriteRendererMugen.sprite = MugenOff;
        spriteRendererFuu.sprite = FuuOff;
        characterPressed = Character.Dzin;
        if (currentCharacter == characterPressed)
        {
            BuyButton.SetActive(false);
            SelectButton.SetActive(true);
            Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
            SelectButtonText.text = "Selected";
        }
        else
        {
            BuyButton.SetActive(false);
            SelectButton.SetActive(true);
            Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
            SelectButtonText.text = "Select";
        }
    }
    public void SelectMugen()
    {
        SoundManager.instance.StatusButtonPlay();
        spriteRendererMugen.sprite = MugenOn;
        spriteRendererFuu.sprite = FuuOff;
        spriteRendererDzin.sprite = DzinOff;
        characterPressed = Character.Mugen;
        if (currentCharacter == characterPressed)
        {
            BuyButton.SetActive(false);
            SelectButton.SetActive(true);
            Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
            SelectButtonText.text = "Selected";
        }
        else
        {
            if (МugenBought)
            {
                BuyButton.SetActive(false);
                SelectButton.SetActive(true);
                Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
                SelectButtonText.text = "Select";
            }
            else
            {
                SelectButton.SetActive(false);
                BuyButton.SetActive(true);
                Text BuyButtonText = BuyButton.GetComponentInChildren<Text>();
                BuyButtonText.text = "Buy " + priceMugen;
            }
        }
    }
    public void SelectFuu()
    {
        SoundManager.instance.StatusButtonPlay();
        spriteRendererFuu.sprite = FuuOn;
        spriteRendererDzin.sprite = DzinOff;
        spriteRendererMugen.sprite = MugenOff;
        characterPressed = Character.Fuu;
        if (currentCharacter == characterPressed)
        {
            BuyButton.SetActive(false);
            SelectButton.SetActive(true);
            Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
            SelectButtonText.text = "Selected";
        }
        else
        {
            if (FuuBought)
            {
                BuyButton.SetActive(false);
                SelectButton.SetActive(true);
                Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
                SelectButtonText.text = "Select";
            }
            else
            {
                SelectButton.SetActive(false);
                BuyButton.SetActive(true);
                Text BuyButtonText = BuyButton.GetComponentInChildren<Text>();
                BuyButtonText.text = "Buy " + priceFuu;
            }
        }
    }
    public void PressSelectButton()
    {
        SoundManager.instance.StatusButtonPlay();
        Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
        if (SelectButtonText.text == "Select")
        {
            SelectButtonText.text = "Selected";
            if (characterPressed == Character.Dzin)
            {
                currentCharacter = Character.Dzin;
                spriteRendererMenuCanvasCharacterImage.sprite = DzinOff;
                PlayerPrefs.SetInt("currentCharacter", (int)currentCharacter);
            }
            else if (characterPressed == Character.Mugen)
            {
                currentCharacter = Character.Mugen;
                spriteRendererMenuCanvasCharacterImage.sprite = MugenOff;
                PlayerPrefs.SetInt("currentCharacter", (int)currentCharacter);
            }
            else
            {
                currentCharacter = Character.Fuu;
                spriteRendererMenuCanvasCharacterImage.sprite = FuuOff;
                PlayerPrefs.SetInt("currentCharacter", (int)currentCharacter);
            }
        }
    }
    public void PressBuyButton()
    {
        SoundManager.instance.StatusButtonPlay();
        if (characterPressed == Character.Mugen)
        {
            if (UIManager.instance.allCoins >= priceMugen)
            {
                UIManager.instance.allCoins -= priceMugen;
                PlayerPrefs.SetInt("Coins", UIManager.instance.allCoins);
                МugenBought = true;
                PlayerPrefs.SetInt("МugenBought", МugenBought ? 1 : 0);
                BuyButton.SetActive(false);
                SelectButton.SetActive(true);
                Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
                SelectButtonText.text = "Select";
            }
        }
        else
        {
            if (UIManager.instance.allCoins >= priceFuu)
            {
                UIManager.instance.allCoins -= priceFuu;
                PlayerPrefs.SetInt("Coins", UIManager.instance.allCoins);
                FuuBought = true;
                PlayerPrefs.SetInt("FuuBought", FuuBought ? 1 : 0);
                BuyButton.SetActive(false);
                SelectButton.SetActive(true);
                Text SelectButtonText = SelectButton.GetComponentInChildren<Text>();
                SelectButtonText.text = "Select";
            }
        }
    }
    public void OnOffSound()
    {
        SoundManager.instance.StatusButtonPlay();
        UIManager.instance.isSound = !UIManager.instance.isSound;
        if (UIManager.instance.isSound)
        {
            spriteRendererSound.sprite = soundOn;

            SoundManager.instance.Mixer.audioMixer.SetFloat("Master Sound", 0);
        }
        else
        {
            spriteRendererSound.sprite = soundOff;
            SoundManager.instance.Mixer.audioMixer.SetFloat("Master Sound", -80);
        }
        PlayerPrefs.SetInt("Master Sound", UIManager.instance.isSound ? 1 : 0);
    }
    public void OnOffMusic()
    {
        SoundManager.instance.StatusButtonPlay();
        UIManager.instance.isMusic = !UIManager.instance.isMusic;
        if (UIManager.instance.isMusic)
        {
            spriteRendererMusic.sprite = musicOn;
            SoundManager.instance.Mixer.audioMixer.SetFloat("Music", 0);
        }
        else
        {
            spriteRendererMusic.sprite = musicOff;
            SoundManager.instance.Mixer.audioMixer.SetFloat("Music", -80);
        }
        PlayerPrefs.SetInt("Music", UIManager.instance.isMusic ? 1 : 0);
    }
    public void QuitGame()
    {
        SoundManager.instance.StatusButtonPlay();
        PlayerPrefs.Save();
        Debug.Log("QuitGame");
        Application.Quit();
    }
    public void Menu()
    {
        SoundManager.instance.StatusButtonPlay();
        MenuCanvas.SetActive(true);
        SettingCanvas.SetActive(false);
        LevelsCanvas.SetActive(false);
        CharacterCanvas.SetActive(false);
        QuestionCanvas.SetActive(false);
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            soundOn = Resources.Load<Sprite>("UI/soundon");
            soundOff = Resources.Load<Sprite>("UI/soundoff");
            musicOff = Resources.Load<Sprite>("UI/musicoff");
            musicOn = Resources.Load<Sprite>("UI/musicon");
            DzinOn = Resources.Load<Sprite>("UI/DzinCharacterOutline");
            DzinOff = Resources.Load<Sprite>("UI/DzinCharacter");
            MugenOn = Resources.Load<Sprite>("UI/MugenCharacterOutline");
            MugenOff = Resources.Load<Sprite>("UI/MugenCharacter");
            FuuOn = Resources.Load<Sprite>("UI/FuuCharacterOutline");
            FuuOff = Resources.Load<Sprite>("UI/FuuCharacter");
            levelGold = Resources.Load<Sprite>("UI/goldbutton");
            levelCopper = Resources.Load<Sprite>("UI/copperbutton");
            levelSilver = Resources.Load<Sprite>("UI/silverbutton");
        }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.HasKey("Music"))
            {
                UIManager.instance.isMusic = PlayerPrefs.GetInt("Music") == 1;
                if (UIManager.instance.isMusic)
                {
                    spriteRendererMusic.sprite = musicOn;
                    SoundManager.instance.Mixer.audioMixer.SetFloat("Music", 0);
                }
                else
                {
                    spriteRendererMusic.sprite = musicOff;
                    SoundManager.instance.Mixer.audioMixer.SetFloat("Music", -80);
                }
            }
            else
            {
                UIManager.instance.isMusic = true;
                spriteRendererMusic.sprite = musicOn;
                SoundManager.instance.Mixer.audioMixer.SetFloat("Music", 0);
            }
            if (PlayerPrefs.HasKey("Master Sound"))
            {
                UIManager.instance.isSound = PlayerPrefs.GetInt("Master Sound") == 1;
                if (UIManager.instance.isSound)
                {
                    spriteRendererSound.sprite = soundOn;
                    SoundManager.instance.Mixer.audioMixer.SetFloat("Master Sound", 0);
                }
                else
                {
                    spriteRendererSound.sprite = soundOff;
                    SoundManager.instance.Mixer.audioMixer.SetFloat("Master Sound", -80);
                }
            }
            else
            {
                UIManager.instance.isSound = true;
                spriteRendererSound.sprite = soundOn;
                SoundManager.instance.Mixer.audioMixer.SetFloat("Master Sound", 0);
            }
            if (PlayerPrefs.HasKey("currentCharacter"))
            {
                currentCharacter = (Character)PlayerPrefs.GetInt("currentCharacter");
                characterPressed = (Character)PlayerPrefs.GetInt("currentCharacter");
                if (PlayerPrefs.GetInt("currentCharacter") == 0)
                {
                    spriteRendererMenuCanvasCharacterImage.sprite = DzinOff;
                    spriteRendererMugen.sprite = MugenOff;
                    spriteRendererFuu.sprite = FuuOff;
                    spriteRendererDzin.sprite = DzinOn;
                }
                if (PlayerPrefs.GetInt("currentCharacter") == 1)
                {
                    spriteRendererMenuCanvasCharacterImage.sprite = MugenOff;
                    spriteRendererMugen.sprite = MugenOn;
                    spriteRendererFuu.sprite = FuuOff;
                    spriteRendererDzin.sprite = DzinOff;
                }
                if (PlayerPrefs.GetInt("currentCharacter") == 2)
                {
                    spriteRendererMenuCanvasCharacterImage.sprite = FuuOff;
                    spriteRendererMugen.sprite = MugenOff;
                    spriteRendererFuu.sprite = FuuOn;
                    spriteRendererDzin.sprite = DzinOff;
                }
            }
            else
            {
                currentCharacter = Character.Dzin;
                spriteRendererMenuCanvasCharacterImage.sprite = DzinOff;
                characterPressed = Character.Dzin;
                spriteRendererMugen.sprite = MugenOff;
                spriteRendererFuu.sprite = FuuOff;
                spriteRendererDzin.sprite = DzinOn;
            }
            if (PlayerPrefs.HasKey("МugenBought"))
            {
                МugenBought = PlayerPrefs.GetInt("МugenBought") == 1;
            }
            else
            {
                МugenBought = false;
            }
            if (PlayerPrefs.HasKey("FuuBought"))
            {
                FuuBought = PlayerPrefs.GetInt("FuuBought") == 1;
            }
            else
            {
                FuuBought = false;
            }
        }
    }
}
