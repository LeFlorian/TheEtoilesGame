using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [HideInInspector]
    private Controls c;

    [HideInInspector]
    public InputAction move;
    [HideInInspector]
    public InputAction jump;

    private void Awake()
    {
        instance = this;

        c = new Controls();
    }

    private void OnEnable()
    {
        move = c.Game.Move;
        move.Enable();

        jump = c.Game.Jump;
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }
}
