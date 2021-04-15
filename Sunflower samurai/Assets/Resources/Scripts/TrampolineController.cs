using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Trampoline");
            ParticleController.instance.SmokeEffectPlay();
            if (PlayerPrefs.HasKey("Master Sound"))
            {
                if (PlayerPrefs.GetInt("Master Sound") == 1)
                {
                    SoundManager.instance.JumpCharacterPlay();
                    //AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                }
                else
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 0f);
                SunflowerSamurai.instance.TouchingTrampoline();
            }
            else
            {
                SoundManager.instance.JumpCharacterPlay();
                //AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                SunflowerSamurai.instance.TouchingTrampoline();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().ResetTrigger("Trampoline");
            SunflowerSamurai.instance.UnTouchingTrampoline();
        }
    }
}
