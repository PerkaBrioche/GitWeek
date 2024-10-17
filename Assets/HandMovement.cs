using UnityEngine;
using UnityEngine.UI;

public class HandMovement : MonoBehaviour
{
    public RectTransform leftHand;
    public RectTransform rightHand; 
    public float smoothness = 5.0f; 
    public float movementAmount = 30.0f;

    private Vector3 initialLeftHandPosition;
    private Vector3 initialRightHandPosition;

    private void Start()
    {
        initialLeftHandPosition = leftHand.anchoredPosition;
        initialRightHandPosition = rightHand.anchoredPosition;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        
        Vector3 newLeftHandPosition = initialLeftHandPosition + new Vector3(-mouseX + -Horizontal, -mouseY +- Vertical, 0) * movementAmount;
        Vector3 newRightHandPosition = initialRightHandPosition + new Vector3(-mouseX +  -Horizontal, -mouseY +-Vertical, 0) * movementAmount;

        leftHand.anchoredPosition = Vector3.Lerp(leftHand.anchoredPosition, newLeftHandPosition, Time.deltaTime * smoothness);
        rightHand.anchoredPosition = Vector3.Lerp(rightHand.anchoredPosition, newRightHandPosition, Time.deltaTime * smoothness);
    }
}