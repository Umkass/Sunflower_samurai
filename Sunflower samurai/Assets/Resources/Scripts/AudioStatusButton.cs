using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStatusButton : MonoBehaviour
{
    public static AudioStatusButton instance = null;
    AudioSource audioSourceStatusButton;

    public AudioSource AudioSourceStatusButton { get => audioSourceStatusButton; set => audioSourceStatusButton = value; }
}
