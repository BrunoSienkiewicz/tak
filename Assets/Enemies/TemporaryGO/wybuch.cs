using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wybuch : MonoBehaviour
{
    public float t;

    void Start()
    {
        Destroy(gameObject, t);
    }
}
