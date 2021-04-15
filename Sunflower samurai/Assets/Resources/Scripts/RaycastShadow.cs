using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShadow : MonoBehaviour
{
    RaycastHit2D[] hits;
    [SerializeField]
    GameObject shadow;
    Transform character;

    void Awake()
    {
        character = gameObject.GetComponentInParent<Transform>();
    }
    void Update()
    {
        Vector3 origin = new Vector3(character.transform.position.x, character.transform.position.y - 1.1f, 0);
        if (SunflowerSamurai.instance.Gravity)
        {
            Ray2D ray = new Ray2D(origin, -transform.up);
            hits = Physics2D.RaycastAll(origin, -transform.up, 10f);
            Debug.DrawRay(origin, -transform.up * 10, Color.blue);
        }
        else
        {
            Ray2D ray = new Ray2D(origin, transform.up);
            hits = Physics2D.RaycastAll(origin, transform.up, 10f);
            Debug.DrawRay(origin, transform.up * 10, Color.blue);
        }
        foreach (RaycastHit2D item in hits)
        {
            if (item.collider != null && shadow != null)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.CompareTag("Ground"))
                    {
                        shadow.SetActive(true);
                        if (PlayerPrefs.GetInt("currentCharacter") == 0)
                            shadow.transform.position = new Vector3(character.transform.position.x + 0.15f, hits[i].point.y, 0);
                        if (PlayerPrefs.GetInt("currentCharacter") == 1 || PlayerPrefs.GetInt("currentCharacter") == 2)
                            shadow.transform.position = new Vector3(character.transform.position.x, hits[i].point.y, 0);
                        break;
                    }
                    else
                    {
                        shadow.SetActive(false);
                        break;
                    }
                }
            }
        }
    }
}
