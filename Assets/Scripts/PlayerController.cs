using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Camera myCamera;
    private Vector2 camDirection;
    private RaycastHit[] results = new RaycastHit[5];
    public float speed = 1f;
    public float defaultFOV = 50f;
    public float zoomFOV = 30f;
    public bool isMoving => MyInput.GetMove().magnitude > 0;
    public bool isZooming => myCamera.fieldOfView < defaultFOV;
    public bool isLooking => MyInput.GetLook().magnitude > 0;
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
        Vector3 direction = transform.forward * MyInput.GetMove().y + transform.right * MyInput.GetMove().x;
        controller.Move(direction * Time.deltaTime * speed);
        if(!controller.isGrounded) controller.Move(Physics.gravity * Time.deltaTime);
        camDirection.x += MyInput.GetLook().x;
        camDirection.y -= MyInput.GetLook().y;
        camDirection.y = Mathf.Clamp(camDirection.y, -90f, 90f);
        myCamera.transform.localEulerAngles = new Vector3(camDirection.y, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, camDirection.x, 0f);

        if(!isMoving)
        {
            if (MyInput.GetZoom().IsPressed()) { 
                myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, zoomFOV, Time.deltaTime * speed); 
                if(myCamera.fieldOfView < zoomFOV + 10f) Interact(Interactable.InteractionType.Zoom, Mathf.Infinity);
            }
            else myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, defaultFOV, Time.deltaTime * speed);
        }
        else
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, defaultFOV, Time.deltaTime * speed);
            Interact(Interactable.InteractionType.Look, Mathf.Infinity);
        }

        if (MyInput.GetInteract().WasPressedThisFrame())
        {
            Interact(Interactable.InteractionType.Click);
        }
    }

    public void Interact(Interactable.InteractionType type, float maxDistance = 5f)
    {
        for(int i = 0; i< Physics.RaycastNonAlloc(myCamera.transform.position, myCamera.transform.forward, results, maxDistance); i++)
        {
            if(results[i].transform.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact(type);
            }
        }
    }
}
