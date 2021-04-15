using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PercentageCounter : MonoBehaviour
{
    [SerializeField]
    Text percentageDead;
    [SerializeField]
    Text percentageComplete;
    [SerializeField]
    Transform EndPosition;
    [SerializeField]
    Transform Character;
    void Start()
    {
        percentageDead.text = 0 + "%";
        percentageComplete.text = 0 + "%";
    }
    void Update()
    {
        if (SunflowerSamurai.instance != null)
        {
            if (SunflowerSamurai.instance.state == GameState.Playing)
            {
                if (Character.transform.position.x >= 0.0f)
                {
                    float percentageValue = Character.transform.position.x * 100 / (EndPosition.position.x - 2.45f);
                    if (percentageValue > 100)
                    {
                        percentageDead.text = 100 + "%";
                        percentageComplete.text = 100 + "%";
                    }
                    else
                    {
                        percentageDead.text = (int)percentageValue + "%";
                        percentageComplete.text = (int)percentageValue + "%";
                    }
                }
            }
        }
    }
}
