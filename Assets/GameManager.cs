using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GO_DeadScreen;
    public FirstPersonController FirstPersonController;
    public PlayerActionController PlayerActionController;

    public static GameManager Instance;
    public bool BOOL_PlayerDead;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GameOver()
    {
        if(BOOL_PlayerDead){return;}
        GO_DeadScreen.SetActive(true);
        FirstPersonController.playerCanMove = false;
        PlayerActionController.enabled = false;
    }
}
