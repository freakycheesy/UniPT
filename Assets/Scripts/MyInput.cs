using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInput : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Action = Resources.Load<InputActionAsset>("Input").FindActionMap("Player");
        Action.Enable();
    }
    public static InputActionMap Action { get; private set; }
    public static Vector2 GetMove() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    public static Vector2 GetLook() => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    public static bool GetZoom() => Input.GetButton("Zoom");
    public static bool GetZoomDown() => Input.GetButtonDown("Zoom");
    public static bool GetZoomUp() => Input.GetButtonUp("Zoom");
    public static bool GetInteract() => Input.GetButton("Interact");
    public static bool GetInteractDown() => Input.GetButtonDown("Interact");
    public static bool GetInteractUp() => Input.GetButtonUp("Interact");
}
