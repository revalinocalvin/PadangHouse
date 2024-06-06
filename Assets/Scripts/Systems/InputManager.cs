using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public bool menuOpenCloseInput {  get; private set; }

    private PlayerInput playerInput;
    private InputAction menuOpenClose;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();
        menuOpenClose = playerInput.actions["MenuOpenClose"];
    }

    private void Update()
    {
        menuOpenCloseInput = menuOpenClose.WasPressedThisFrame();
    }
}
