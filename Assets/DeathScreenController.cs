using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public void LoadScene(int Index)
    {
        SceneManager.LoadScene(0);
    }
    public void Reload()
    {
        int ActiveScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(ActiveScene);
    }
}
