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
    [HideInInspector]
    public bool jumpPerformed;

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

        jump.performed += delegate { StartCoroutine(PerformedJump(true)); };
        
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    private IEnumerator PerformedJump(bool asJump)
    {
        jumpPerformed = true;
        yield return new WaitForEndOfFrame();
        jumpPerformed = false;
    }
}
