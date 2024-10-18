using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    public int SceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SceneIndex == 2)
            {
                Application.Quit();
                return;
            }
            SceneManager.LoadScene(SceneIndex);
        }
    }
}
