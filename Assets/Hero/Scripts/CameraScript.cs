using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    float xRotation = 90f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localPosition += new Vector3(0, 0.7f, 0);
    }

    
    void Update()
    {
        float mouseX = GI.mouseDelta.x * Const.MouseSensitivity * Time.deltaTime;
        float mouseY = GI.mouseDelta.y * Const.MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Hero.ME.transform.Rotate(Vector3.up * mouseX);
    }
}