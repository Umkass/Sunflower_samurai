using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithChance : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField]
    float ChanceOfDestruction;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.value < ChanceOfDestruction)
            DestroyImmediate(gameObject);
    }
}
