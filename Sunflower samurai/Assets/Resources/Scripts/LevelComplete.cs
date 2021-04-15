using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    Animator levelComplete;

    private void Awake()
    {
        levelComplete = gameObject.GetComponent<Animator>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && (SunflowerSamurai.instance.state == GameState.LevelComplete))
        {
            levelComplete.SetTrigger("LevelComplete");
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && (SunflowerSamurai.instance.state == GameState.LevelComplete))
        {
            levelComplete.SetTrigger("LevelComplete");
        }
    }
}