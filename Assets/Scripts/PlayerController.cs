using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Camera myCamera;
    private Vector2 camDirection;
    public float speed = 1f;
    public float defaultFOV = 50f;
    public float zoomFOV = 30f;
    public bool isMoving => Input.GetAxis("Vertical") > 0f || Input.GetAxis("Horizontal") > 0f;
    public bool isZooming => Input.GetButton("Zoom");
    public bool isLooking => Input.GetAxis("Mouse X") > 0f || Input.GetAxis("Mouse Y") > 0f;
    public bool isMovingAll => isMoving || isLooking && (SystemInfo.supportsGyroscope && Input.gyro.userAcceleration.magnitude > 0.1f);
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        controller.Move(direction * Time.deltaTime * speed);
        if(!controller.isGrounded) controller.Move(Physics.gravity * Time.deltaTime);
        camDirection.x += Input.GetAxis("Mouse X");
        camDirection.y -= Input.GetAxis("Mouse Y");
        camDirection.y = Mathf.Clamp(camDirection.y, -90f, 90f);
        myCamera.transform.localEulerAngles = new Vector3(camDirection.y, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, camDirection.x, 0f);

        if(!isMoving)
        {
            if (Input.GetButton("Zoom")) myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, zoomFOV, Time.deltaTime * speed);
            else myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, defaultFOV, Time.deltaTime * speed);
        }
        else
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, defaultFOV, Time.deltaTime * speed);
        }
    }
}
