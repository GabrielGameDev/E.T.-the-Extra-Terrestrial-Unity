using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Range(0f,1f)]
    public float chance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > chance)
            gameObject.SetActive(false);
    }

   
}
