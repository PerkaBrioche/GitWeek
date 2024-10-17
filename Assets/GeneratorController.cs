using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public int INT_Life;

    private bool AlreadyDead;

    public void LoseLife(int LifeToSub)
    {
        INT_Life -= LifeToSub;
        if (INT_Life <= 0 && !AlreadyDead)
        {
            AlreadyDead = true;
            DestroyGenerator();
        }
    }

    public void DestroyGenerator()
    {
        transform.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject,2);
        transform.GetChild(0).gameObject.SetActive(true);
        GameManager.Instance.IncremantGenerator();
    }
}
