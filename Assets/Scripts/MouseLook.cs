using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform PlayerBody;

    float xRotatation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotatation -= mouseY;
        xRotatation = Mathf.Clamp(xRotatation, -90f, 90);

        transform.localRotation = Quaternion.Euler(xRotatation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
