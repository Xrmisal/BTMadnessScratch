using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    [SerializeField]
    GameObject mainCamera;

    public float mouseSensitivity = 500.0f;

    float xRotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Make cursor invisible and locked into position once in the game.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // set mouse axis' to a value based on moue movement.
        float mouseX = Input.GetAxis(xAxis) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(yAxis) * mouseSensitivity * Time.deltaTime;
        // If xRotation is above 90 or below -90, return nothing
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
        // Rotate player left and right with horizontal movement.
        transform.Rotate(Vector3.up * mouseX);
        // Local rotation means to rotate based on the parent's position. Rotate camera on x Axis up to 90 and down to -90.
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
    }

}
