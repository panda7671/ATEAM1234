using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 플레이어
    public Vector3 offset = new Vector3(0, 2, -4);
    public float mouseSensitivity = 3f;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xRotation = angles.x;
        yRotation = angles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -20f, 60f); // 위아래 회전 제한

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position + Vector3.up * 1.5f); // 카메라가 살짝 위를 보게
    }
}
