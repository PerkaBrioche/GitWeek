using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GO_DeadScreen;
    public FirstPersonController FirstPersonController;
    public PlayerActionController PlayerActionController;

    public static GameManager Instance;
    public bool BOOL_PlayerDead;

    public TextMeshProUGUI TMP_TextGenerator;

    public int INT_GeneratorToGet;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GameObject.DontDestroyOnLoad(gameObject);
        TMP_TextGenerator.text = "";
        FirstPersonController = FindObjectOfType<FirstPersonController>();
        PlayerActionController = FindObjectOfType<PlayerActionController>();
    }

    public void GameOver()
    {
        if(BOOL_PlayerDead){return;}
        GO_DeadScreen.SetActive(true);
        FirstPersonController.playerCanMove = false;
        PlayerActionController.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        FirstPersonController.transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void IncremantGenerator()
    {
        INT_GeneratorToGet++;
        TMP_TextGenerator.text = INT_GeneratorToGet + "/3";

        if (INT_GeneratorToGet >= 3)
        {
            GameObject.Find("Grid_Generator").gameObject.SetActive(false);
        }
    }

    public void ActivateGenerator()
    {
        TMP_TextGenerator.text = INT_GeneratorToGet + "/3";
    }
}
