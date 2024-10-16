using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Slider SLID_Timer;
    private bool BOOL_TimerRunning;

    public float FLO_MaxTimer;
    public float FLO_Multiplier;

    public static TimerManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            GameObject.DontDestroyOnLoad(gameObject);

            Instance = this;
        }
    }

    private void Start()
    {
        FLO_Multiplier = 1;
        LaunchTimer();
    }

    private void Update()
    {
        if (BOOL_TimerRunning)
        {
            ReduceTimer();
        }

        if (SLID_Timer.value <= 0)
        {
            StopTimer();
            GameManager.Instance.GameOver();
        }
    }

    private void ReduceTimer()
    {
        SLID_Timer.value -= Time.deltaTime * FLO_Multiplier;
    }

    public void LaunchTimer()
    {
        SLID_Timer.maxValue = FLO_MaxTimer;
        SLID_Timer.value = FLO_MaxTimer;
        BOOL_TimerRunning = true;
    }

    public void StopTimer()
    {
        BOOL_TimerRunning = false;
    }

    public void AddToTimer(float TimeToAdd)
    {
        SLID_Timer.value += TimeToAdd;
    }
    
    public void SoustractTimer(float TimeToSub)
    {
        SLID_Timer.value -= TimeToSub;
    }}
