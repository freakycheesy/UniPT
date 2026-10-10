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
    public static Vector2 GetMove() => Action["Move"].ReadValue<Vector2>();
    public static Vector2 GetLook() => Action["Look"].ReadValue<Vector2>();
    public static InputAction GetZoom() => Action["Zoom"];
    public static InputAction GetInteract() => Action["Interact"];
}
