using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    private float _currentSteerAngle;
    private float _currentbreakForce;
    private bool _isBreaking;
    [SerializeField] private bool _isDrivable;

    [SerializeField] private float _motorForce;
    [SerializeField] private float _breakForce;
    [SerializeField] private float _maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;

    public GameObject CarHeadLights;
    private bool _carHeadLightsOn = true;

    public GameObject CarSiren;
    private bool _carSirenOn = true;

    public List<CarDoorCollision> CarDoorCollisions;

    private void Update()
    {
        CheckIfDriveable();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        if (_isDrivable)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            _isBreaking = Input.GetKey(KeyCode.Space);
        }
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = _verticalInput * _motorForce;
        frontRightWheelCollider.motorTorque = _verticalInput * _motorForce;
        _currentbreakForce = _isBreaking ? _breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = _currentbreakForce;
        frontLeftWheelCollider.brakeTorque = _currentbreakForce;
        rearLeftWheelCollider.brakeTorque = _currentbreakForce;
        rearRightWheelCollider.brakeTorque = _currentbreakForce;
    }

    private void HandleSteering()
    {
        _currentSteerAngle = _maxSteerAngle * _horizontalInput;
        frontLeftWheelCollider.steerAngle = _currentSteerAngle;
        frontRightWheelCollider.steerAngle = _currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }


    public void ToggleHeadlights()
    {
        if (_carHeadLightsOn)
        {
            _carHeadLightsOn = false;
            CarHeadLights.SetActive(false);
        }
        else
        {
            _carHeadLightsOn = true;
            CarHeadLights.SetActive(true);
        }
    }

    public void ToggleSiren()
    {
        if (_carSirenOn)
        {
            _carSirenOn = false;
            CarSiren.GetComponent<Animator>().enabled = false;
        }
        else
        {
            _carSirenOn = true;
            CarSiren.GetComponent<Animator>().enabled = true;
        }
    }


    private void CheckIfDriveable()
    {
        foreach (var item in CarDoorCollisions)
        {
            if (item.SeatNum == 1 && item.SeatOccupied)
            {
                _isDrivable = true;
            }
            else if (item.SeatNum == 1 && !item.SeatOccupied)
            {
                _isDrivable = false;
            }
        }
    }
}
