using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ParticleController.instance.PetalEffectStart(transform);
            if (PlayerPrefs.HasKey("Master Sound"))
            {
                if (PlayerPrefs.GetInt("Master Sound") == 1)
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                else
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 0f);
                SunflowerSamurai.instance.TouchingPetal();
            }
            else
            {
                AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                SunflowerSamurai.instance.TouchingPetal();
            }
            }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SunflowerSamurai.instance.UnTouchingPetal();
            Destroy(gameObject);
        }
    }
}
