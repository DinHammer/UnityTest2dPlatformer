using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private GameInputs _inputs;

    public bool IsJump { get; private set; }
    public float MoveLeftRight { get; private set; }

    private void Awake()
    {
        _inputs = new GameInputs();
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Update()
    {
        MoveLeftRight = _inputs.Player.LeftRight.ReadValue<float>();

        if (_inputs.Player.Jump.triggered)
        {
            IsJump = true;
        }
        else
        {
            IsJump = false;
        }

    }
}
