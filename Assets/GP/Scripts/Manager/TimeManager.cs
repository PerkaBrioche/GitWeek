using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            GameObject.DontDestroyOnLoad(gameObject);

            instance = this;
        }
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void SlowMotion(float targetTimeScale, float duration)
    {
        StartCoroutine(SlowMotionCoroutine(targetTimeScale, duration));
    }

    public void ResetTimeScale(float duration)
    {
        StartCoroutine(ResetTimeScaleCoroutine(duration));
    }

    public void SetTime(float Scale, float Duration)
    {
        StartCoroutine(SetTimeCoroutine(Scale, Duration));
    }

    private IEnumerator SetTimeCoroutine(float targetTimeScale, float transitionDuration)
    {
        Time.timeScale = targetTimeScale;
        yield return new WaitForSecondsRealtime(transitionDuration);
        Time.timeScale =1f;

    }

    private IEnumerator SlowMotionCoroutine(float targetTimeScale, float transitionDuration)
    {
        float currentScale = Time.timeScale;
        float t = 0f;

        while (t < transitionDuration)
        {
            t += Time.unscaledDeltaTime;
            float newScale = Mathf.Lerp(currentScale, targetTimeScale, t / transitionDuration);
            SetTimeScale(newScale);
            yield return null;
        }

        SetTimeScale(targetTimeScale);
    }

    private IEnumerator ResetTimeScaleCoroutine(float transitionDuration)
    {
        float currentScale = Time.timeScale;
        float t = 0f;

        while (t < transitionDuration)
        {
            t += Time.unscaledDeltaTime;
            float newScale = Mathf.Lerp(currentScale, 1.0f, t / transitionDuration);
            SetTimeScale(newScale);
            yield return null;
        }

        SetTimeScale(1.0f);
    }
}