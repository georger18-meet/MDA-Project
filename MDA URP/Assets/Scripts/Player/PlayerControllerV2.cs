using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    // player controller
    [Header("Camera")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _firstPersonCameraTransform;

    [Header("Animation")]
    [SerializeField] private Animator _playerAnimator;

    [Header("Momvement")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Vector2 _mouseSensitivity = new Vector2(1f, 1f);
    [SerializeField] private float _walkingSpeed = 6f, _runningSpeed = 11f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _maxFlyingHeight = 100f;
    [SerializeField] private float _turnSpeed = 90f;
    private Vector2 _input;

    [Header("Models")]
    [SerializeField] private GameObject _alternativeFlyingModel;

    [Header("Physics")]
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private float _groundCheckRadius = 0.5f;
    private float _gravity = Physics.gravity.y;
    private bool _isGrounded;


    // state machine
    private delegate void State();

    private State _stateAction;

    private void Start()
    {
        _stateAction = UseIdleState;
    }

    private void Update()
    {
        _stateAction.Invoke();
    }

    private void GetInputAxis()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void UseTankMovement()
    {
        Vector3 moveDirerction;
        moveDirerction = transform.forward * _input.y * (Input.GetKey(KeyCode.LeftShift) ? _runningSpeed : _walkingSpeed);

        // moves the character in horizontal direction
        _characterController.Move(moveDirerction * Time.deltaTime - Vector3.up * 0.1f);
    }

    private void UseTankRotate()
    {
        transform.Rotate(0, _input.x * _turnSpeed * Time.deltaTime, 0);
    }

    private void UseFirstPersonMovement()
    {
        _characterController.Move(_input.x * transform.right * Time.deltaTime + _input.y * transform.forward * Time.deltaTime);
    }

    private void UseFirstPersonRotate()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));

        transform.Rotate(Vector3.up * mouseInput.x * _mouseSensitivity.x * Time.deltaTime);
        _playerCamera.transform.Rotate(Vector3.right * mouseInput.y * _mouseSensitivity.y * Time.deltaTime);
    }

    private void SetFirstPersonCamera(bool value)
    {
        _playerCamera.transform.position = value ? _firstPersonCameraTransform.position : new Vector3(0f, 3.25f, -4.5f);
        _playerCamera.transform.rotation = value ? _firstPersonCameraTransform.rotation : Quaternion.Euler(new Vector3(15f, 0f, 0f));
    }


    #region States
    // states:
    // idle, walking, running, flying, firstPerson, thirdPerson, driving, treating

    private void UseIdleState()
    {
        Debug.Log("Current State: Idle");

        GetInputAxis();

        if (_input != Vector2.zero)
        {
            _stateAction = UseTankWalkingState;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SetFirstPersonCamera(true);
            _stateAction = UseFirstPersonWalkingState;
        }
    }

    private void UseTankWalkingState()
    {
        Debug.Log("Current State: Walking");

        GetInputAxis();

        if (_input == Vector2.zero)
        {
            _stateAction = UseIdleState;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SetFirstPersonCamera(true);
            _stateAction = UseFirstPersonWalkingState;
        }

        UseTankRotate();
        UseTankMovement();
    }

    private void UseFirstPersonWalkingState()
    {
        Debug.Log("Current State: First Person Walking");

        GetInputAxis();

        if (Input.GetKeyDown(KeyCode.V))
        {
            SetFirstPersonCamera(false);

            if (_input == Vector2.zero)
            {
                _stateAction = UseIdleState;
            }
            else
            {
                _stateAction = UseTankWalkingState;
            }
        }

        UseFirstPersonRotate();
        UseFirstPersonMovement();
    }

    private void UseFlyingState()
    {

    }

    private void UseFirstPersonState()
    {

    }

    private void UseThirdPersonState()
    {
        // set camera original pos
        // position: Vector3(0,3.25,-4.5)
        // rotation: Vector3(15,0,0)
    }

    private void UseDrivingState()
    {

    }

    private void UseTreatingState()
    {

    }
    #endregion
}
