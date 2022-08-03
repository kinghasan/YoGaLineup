using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;

public class PlayerControlOmnidirectional : PlayerControl
{
    private float _checkDistance = 100f;

    private Vector3 _lastMousePosition;
    private bool _isMouseDown;

    public override void InitComponent()
    {
        base.InitComponent();
        _isMouseDown = false;
    }

    public override void Update()
    {
        if (Game.GamePhase != GamePhaseType.Gaming) return;
        if (!State.EnableInput) return;

        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
            _lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
        }
    }

    public virtual void FixedUpdate()
    {
        var deltaTime = FixedDeltaTime;
        if (Game.GamePhase != GamePhaseType.Gaming) return;
        if (!State.EnableInput) return;
        UpdateImpl(deltaTime);
    }

    public override void UpdateImpl(float deltaTime)
    {
        if (!State.EnableInput) return;

        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
            _lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
        }

        if (_isMouseDown)
        {
            var currentPosition = Input.mousePosition;
            var direction = currentPosition - _lastMousePosition;
            if (direction.magnitude > _checkDistance)
            {
                _lastMousePosition = currentPosition + -direction.normalized * _checkDistance;
            }

            direction.z = direction.y;
            direction.y = 0;
            direction = direction.normalized;

            if (direction == Vector3.zero) return;

            var distance = Move.MoveSpeed * deltaTime;
            var position = Move.MoveDirection(direction, distance);
        }
        else
        {
            Rigidbody.ClearMomentum();
        }
    }
}
