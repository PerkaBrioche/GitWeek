using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{

    private RawImage RAW_Icon;

    private void Awake()
    {
        RAW_Icon = transform.GetComponent<RawImage>();
        StopSurbrillance();
    }

    public void SetIcon(Texture texture)
    {
        RAW_Icon.texture = texture;
    }
    public void Surbrillance()
    {
        RAW_Icon.color = new Color(255/255, 255/255, 255/255, 0.7f);
    }
    public void StopSurbrillance()
    {
        RAW_Icon.color = new Color(255/255, 255/255, 255/255, 0.2f);
    }
}
