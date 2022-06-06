using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    // player controller
    [Header("Camera")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _firstPersonCameraTransform, _thirdPersonCameraTransform;

    [Header("Animation")]
    [SerializeField] private Animator _playerAnimator;

    [Header("Momvement")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Vector2 _mouseSensitivity = new Vector2(60f, 40f);
    [SerializeField] private float _turnSpeed = 90f, _walkingSpeed = 6f, _runningSpeed = 11f;
    [SerializeField] private float _jumpForce = 3f, _maxFlyingHeight = 100f;
    private Vector2 _input;

    [Header("Models")]
    [SerializeField] private GameObject _originalModel;
    [SerializeField] private GameObject _alternativeFlyingModel;

    [Header("Physics")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private float _groundCheckRadius = 0.5f;
    private float _gravity = Physics.gravity.y;
    private bool _isGrounded;

    // state machine
    private delegate void State();

    private State _stateAction;
    // -------------

    private void Start()
    {
        FreeMouse(true);
        _stateAction = UseTankIdleState;
    }

    private void Update()
    {
        _stateAction.Invoke();
    }

    #region Private Methods
    private void GetInputAxis()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void UseTankMovement()
    {
        Vector3 moveDirerction;
        moveDirerction = transform.forward * _input.y * (Input.GetKey(KeyCode.LeftShift) ? _runningSpeed : _walkingSpeed);

        // moves the character in diagonal direction
        _characterController.Move(moveDirerction * Time.deltaTime - Vector3.up * 0.1f);
    }

    private void UseTankRotate()
    {
        transform.Rotate(0, _input.x * _turnSpeed * Time.deltaTime, 0);
    }

    private void UseFirstPersonMovement()
    {
        _characterController.Move( transform.right * _input.x * (Input.GetKey(KeyCode.LeftShift) ? _runningSpeed : _walkingSpeed) * Time.deltaTime +  transform.forward * _input.y * (Input.GetKey(KeyCode.LeftShift) ? _runningSpeed : _walkingSpeed) * Time.deltaTime);
    }

    private void UseFirstPersonRotate()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));

        transform.Rotate(Vector3.up * mouseInput.x * _mouseSensitivity.x * Time.deltaTime);
        _playerCamera.transform.Rotate(Vector3.right * mouseInput.y * _mouseSensitivity.y * Time.deltaTime);
    }

    private void SetFirstPersonCamera(bool value)
    {
        _playerCamera.transform.position = value ? _firstPersonCameraTransform.position : _thirdPersonCameraTransform.position;
        _playerCamera.transform.rotation = value ? _firstPersonCameraTransform.rotation : _thirdPersonCameraTransform.rotation;
    }

    private void FreeMouse(bool value)
    {
        // Input.GetMouseButtonDown(1)
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void RotateBodyWithMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FreeMouse(true);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));

            transform.Rotate(Vector3.up * mouseInput.x * _mouseSensitivity.x * Time.deltaTime);
            _playerCamera.transform.Rotate(Vector3.right * mouseInput.y * _mouseSensitivity.y * Time.deltaTime);
        }
        else
        {
            FreeMouse(false);
        }
    }
    #endregion

    #region States

    private void UseTankIdleState()
    {
        Debug.Log("Current State: Idle");

        GetInputAxis();

        if (_input != Vector2.zero)
        {
            _stateAction = UseTankWalkingState;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            FreeMouse(false);
            SetFirstPersonCamera(true);
            _stateAction = UseFirstPersonIdleState;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _stateAction = UseFlyingState;
        }

        RotateBodyWithMouse();
    }

    private void UseFirstPersonIdleState()
    {
        Debug.Log("Current State: First Person Idle");

        GetInputAxis();

        if (_input != Vector2.zero)
        {
            FreeMouse(false);
            _stateAction = UseFirstPersonWalkingState;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            FreeMouse(true);
            SetFirstPersonCamera(false);
            _stateAction = UseTankIdleState;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _stateAction = UseFlyingState;
        }

        UseFirstPersonRotate();
    }

    private void UseTankWalkingState()
    {
        Debug.Log("Current State: Walking");

        GetInputAxis();

        if (_input == Vector2.zero)
        {
            FreeMouse(true);
            _stateAction = UseTankIdleState;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            FreeMouse(false);
            SetFirstPersonCamera(true);
            _stateAction = UseFirstPersonWalkingState;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _stateAction = UseFlyingState;
        }

        RotateBodyWithMouse();
        UseTankRotate();
        UseTankMovement();
    }

    private void UseFirstPersonWalkingState()
    {
        Debug.Log("Current State: First Person Walking");

        GetInputAxis();

        if (_input == Vector2.zero)
        {
            _stateAction = UseFirstPersonIdleState;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SetFirstPersonCamera(false);

            if (_input == Vector2.zero)
            {
                FreeMouse(true);
                _stateAction = UseTankIdleState;
            }
            else
            {
                FreeMouse(true);
                _stateAction = UseTankWalkingState;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _stateAction = UseFlyingState;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Cursor.visible)
            {
                FreeMouse(false);
            }
            else
            {
                FreeMouse(true);
            }
        }

        UseFirstPersonRotate();
        UseFirstPersonMovement();
    }

    private void UseFlyingState()
    {
        Debug.Log("Current State: Flying");

        if (Input.GetKeyDown(KeyCode.G))
        {
            _stateAction = UseTankIdleState;
        }
    }

    private void UseDrivingState()
    {

    }

    private void UseTreatingState()
    {

    }
    #endregion

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (_isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(_groundCheckTransform.position, _groundCheckRadius);
    }
    #endregion
}
