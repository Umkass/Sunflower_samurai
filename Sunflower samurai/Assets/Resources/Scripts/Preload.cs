using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preload : MonoBehaviour
{
    void Awake()
    {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}