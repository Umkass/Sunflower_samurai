using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioMixerGroup Mixer;
    [SerializeField]
    AudioSource audioSourceStatusButton;
    [SerializeField]
    AudioSource audioSourceBackgroundMusic;
    [SerializeField]
    public AudioSource audioSourceCharacter;
    [SerializeField]
    AudioClip[] soundsCharacter;


    public AudioSource AudioSourceStatusButton { get => audioSourceStatusButton; set => audioSourceStatusButton = value; }
    public AudioSource AudioSourceCharacter { get => audioSourceCharacter; set => audioSourceCharacter = value; }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void StatusButtonPlay()
    {
        audioSourceStatusButton.PlayOneShot(AudioSourceStatusButton.clip);
    }

    public void JumpCharacterPlay()
    {
        audioSourceCharacter.PlayOneShot(soundsCharacter[0]);
    }
    public void FallCharacterPlay()
    {
        audioSourceCharacter.PlayOneShot(soundsCharacter[1]);
    }
    public void DieCharacterPlay()
    {
        audioSourceCharacter.PlayOneShot(soundsCharacter[2]);
    }
    public void LevelCompleteCharacterPlay()
    {
        audioSourceCharacter.PlayOneShot(soundsCharacter[3]);
    }
}
