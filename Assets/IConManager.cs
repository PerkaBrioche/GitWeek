using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IConManager : MonoBehaviour
{
    public void CheckIncon(int Index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == Index)
            {
                transform.GetChild(i).GetComponent<IconController>().Surbrillance();
            }
            else
            {
                transform.GetChild(i).GetComponent<IconController>().StopSurbrillance();
            }
        }
    }
}
