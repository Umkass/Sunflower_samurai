using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalInfinityController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ParticleController.instance.PetalInfinityEffectStart(transform);
            if (PlayerPrefs.HasKey("Master Sound"))
            {
                if (PlayerPrefs.GetInt("Master Sound") == 1)
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                else
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 0f);
                if (SunflowerSamurai.instance.extraJumpCount > 3)
                {
                    SunflowerSamurai.instance.UnTouchingPetalInfinity();
                }
                else
                {
                    SunflowerSamurai.instance.TouchingPetalInfinity();
                }
            }
            else
            {
                AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                if (SunflowerSamurai.instance.extraJumpCount > 3)
                {
                    SunflowerSamurai.instance.UnTouchingPetalInfinity();
                }
                else
                {
                    SunflowerSamurai.instance.TouchingPetalInfinity();
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
