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

    private CarDoorCollision DoorCollider;

    private void Update()
    {
        PlayerInteractWithCar();
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


    public void CollisionDetected(CarDoorCollision childScript)
    {
        if (childScript == null)
        {
            DoorCollider = null;
        }
        else if (childScript.SeatNum == 1)
        {
            DoorCollider = childScript;
        }
    }

    private void PlayerInteractWithCar()
    {
        if (DoorCollider != null && _isDrivable)
        {
            DoorCollider.CollidedPlayer.transform.transform.position = DoorCollider.gameObject.transform.position;
            DoorCollider.CollidedPlayer.transform.transform.position += new Vector3(0, 2, 0);
        }

        if (DoorCollider != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && !_isDrivable)
            {
                _isDrivable = true;
                DoorCollider.CollidedPlayer.GetComponent<ThirdPersonMovement>().InControl = false;
            }
            else if (Input.GetKeyDown(KeyCode.F) && _isDrivable)
            {
                _isDrivable = false;
                DoorCollider.CollidedPlayer.GetComponent<ThirdPersonMovement>().InControl = true;
            }
        }
    }
}
