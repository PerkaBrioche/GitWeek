using System.Collections;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            GameObject.DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void ShakeCamera(float intensity, float duration)
    {
        StartCoroutine(Shake(intensity, duration));
    }

    private IEnumerator Shake(float intensity, float duration)
    {
        float elapsed = 0.0f;
        Transform cameraTransform = Camera.main.transform;

        // On utilise la position locale de la caméra au lieu de la position globale
        Vector3 originalLocalPos = cameraTransform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float offsetX = (Mathf.PerlinNoise(Time.time * 10f, 0f) - 0.5f) * intensity;
            float offsetY = (Mathf.PerlinNoise(0f, Time.time * 10f) - 0.5f) * intensity;

            cameraTransform.localPosition = new Vector3(
                originalLocalPos.x + offsetX, 
                originalLocalPos.y + offsetY, 
                originalLocalPos.z
            );

            yield return null;
        }

        // Retour à la position locale d'origine
        cameraTransform.localPosition = originalLocalPos;
    }
}