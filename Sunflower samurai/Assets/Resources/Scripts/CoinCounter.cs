using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.allCoins += 1;
            if (PlayerPrefs.HasKey("Master Sound"))
            {
                if (PlayerPrefs.GetInt("Master Sound") == 1)
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                else
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 0f);
            }
            else
            {
                AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
            }
                Destroy(gameObject);
        }
    }
}
