using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetctileController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TimerManager.Instance.SoustractTimer(2);
        }
    }
}
