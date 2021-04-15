using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCoins : MonoBehaviour
{
    static public int levelCoins;
    Text coinCounter;
    void Start()
    {
        coinCounter = GetComponent<Text>();
        levelCoins = 0;
    }

    void Update()
    {
        coinCounter.text = " " + levelCoins;
    }
}
