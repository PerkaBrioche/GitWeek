using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHimself : MonoBehaviour
{
    public float DestroyAfter;
    void Start()
    {
        Destroy(gameObject, DestroyAfter);
    }
}
