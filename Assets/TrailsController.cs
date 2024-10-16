using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailsController : MonoBehaviour
{
    public List<TrailRenderer> List_Trails;
    private bool IsEmit;

    public void ChangeEmission()
    {
        IsEmit =! IsEmit;
        foreach (var Trail in List_Trails)
        {
            Trail.emitting = IsEmit;
        }
    }
}
