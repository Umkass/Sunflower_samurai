using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    [SerializeField]
    GameObject[] destroyGameObjects;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("PressurePlate");
            if (PlayerPrefs.HasKey("Master Sound"))
            {
                if (PlayerPrefs.GetInt("Master Sound") == 1)
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                else
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 0f);
                StartCoroutine(corountineDestroyObjects());
            }
            else
            {
                AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 1f);
                StartCoroutine(corountineDestroyObjects());
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().ResetTrigger("PressurePlate");
        }
    }

    IEnumerator corountineDestroyObjects()
    {
        for (int i = 0; i < destroyGameObjects.Length; i++)
        {
            destroyGameObjects[i].SetActive(false);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
