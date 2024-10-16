using System.Collections;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float tiltAmount = 6f; 
    public float smoothSpeed = 5f;

    private float targetTilt = 0f;      
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            targetTilt = -tiltAmount;
        }
        else if (horizontalInput < 0)
        {
            targetTilt = tiltAmount;
        }
        else
        {
            targetTilt = 0f;
        }

        // Interpole la rotation actuelle vers la rotation cible
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, 0, targetTilt);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}