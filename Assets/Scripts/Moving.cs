using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : MonoBehaviour
{
    //////////////////////////////
    ///Position var section
    [SerializeField] private InputAction inputAction;

    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField ]private float _movingSpeed;

    private float _offsetXPos;
    private float _offsetYPos;

    private float _newXPos;
    private float _newYPos;

    private float _clampedXPos;
    private float _clampedYPos;


    [SerializeField] private float _xMax;
    [SerializeField] private float _xMin;

    [SerializeField] private float _yMax;
    [SerializeField] private float _yMin;

    //////////////////////////////
    ///Roatation var section

    private float _pitch;
    private float _yaw;
    private float _roll;

    private float _controlPitchFactor = -15f;
    private float _positionPitchFactor = -5f;

    private float _positionYawFactor = 2f;
    private float _controlRollFactor = -20f;

    private void OnEnable()
    {
        inputAction.Enable();
    }

    void Start()
    {
        _movingSpeed = 15f;

        _xMax = 7.60f;
        _xMin = -7.60f;
        _yMax = 6.9f;
        _yMin = -2.6f;


        _pitch = 0f;
        _yaw = 0f;
        _roll = 0f;
    }

    void Update()
    {
        ShipMoving();
        ShipRotating();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void ShipMoving()
    {
        _horizontalInput = inputAction.ReadValue<Vector2>().x;
        _verticalInput = inputAction.ReadValue<Vector2>().y;

        _offsetXPos = _horizontalInput * _movingSpeed * Time.deltaTime;
        _offsetYPos = _verticalInput * _movingSpeed * Time.deltaTime;

        _newXPos = transform.localPosition.x + _offsetXPos;
        _newYPos = transform.localPosition.y + _offsetYPos;

        _clampedXPos = Mathf.Clamp(_newXPos, _xMin, _xMax);
        _clampedYPos = Mathf.Clamp(_newYPos, _yMin, _yMax);

        transform.localPosition = new Vector3(_clampedXPos, _clampedYPos, 0);

        //Debug.Log($"Horizontal = {_horizontalMoving} and Vertical = {_verticalMoving}");
    }

    public void ShipRotating() 
    {
        _pitch = transform.localPosition.y * _positionPitchFactor 
        + _verticalInput * _controlPitchFactor;

        _yaw = transform.localPosition.x * _positionYawFactor;
                
        _roll = _horizontalInput * _controlRollFactor;

        transform.localRotation = Quaternion.Euler(_pitch, _yaw, _roll);
    }

}
