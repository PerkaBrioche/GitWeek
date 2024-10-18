using System.Collections;
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

    public void SetTimeFor(float CoolDown, float Scale)
    {
        StartCoroutine(StopTime(Scale, CoolDown));
    }

    private IEnumerator StopTime(float targetTimeScale, float Duration)
    {
        Time.timeScale = targetTimeScale;
        yield return new WaitForSecondsRealtime(Duration);
        Time.timeScale = 1;

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