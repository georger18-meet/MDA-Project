using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource _indicatorSound;
    [SerializeField] private GameObject _indicatorIcon;

    [Header("Cameras")]
    [SerializeField] private Transform _mainPlayerCamTransform;
    public bool UseFirstPersonCam = false;

    [Header("Momvement")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed = 4f, _sprintMultiplier = 2f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _maxFlyingHeight = 100f;
    public float RotationSmootingTime = 0.1f, RotationSmoothVelocity;
    private float _finalSpeed;

    [Header("Animation")]
    [SerializeField] private Animator _playerAnimator;

    [Header("Models")]
    [SerializeField] private GameObject _alternativeFlyingModel;

    [Header("Physics")]
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private float _groundCheckRadius = 0.5f;
    private float _gravity = Physics.gravity.y;
    private bool _isGrounded;

    [Header("States")]
    [SerializeField] private bool _isRunning = true;
    [SerializeField] private bool _isFlying = false;

    public bool UseOldControls = true;
    public bool IsOnFoot = true;

    private Vector3 _velocity;
    private bool _isMoving;
    private bool _isSprinting;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (IsOnFoot)
        {
            _characterController.enabled = true;
            MovePlayer();
            ApplyGravity();
            Fly();
            if (!_isFlying)
            {
                AnimationController();
            }
        }
        else
        {
            _characterController.enabled = false;
            AnimationController();
        }
    }

    public void MovePlayer()
    {
        // get input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // calculate Direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // If There's Input
        if (direction.sqrMagnitude >= 0.1f)
        {
            if (UseFirstPersonCam)
            {
                _isMoving = true;
                Sprint();
                Vector3 moveDirectionFirstPesonCam = transform.right * horizontal + transform.forward * vertical;
                _characterController.Move(moveDirectionFirstPesonCam * _finalSpeed * Time.deltaTime);
            }
            else
            {
                _isMoving = true;
                // Rotate Player to Direction of Movement
                // *(The "+ Cam.eulerAngles.y" Makes the Player Face Look Direction)
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainPlayerCamTransform.eulerAngles.y;
                float angle;
                if (UseOldControls)
                {
                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref RotationSmoothVelocity, RotationSmootingTime);
                }
                else
                {
                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref RotationSmoothVelocity, RotationSmootingTime * 4);
                }
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Move Towards Look Direction
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                Sprint();
                _characterController.Move(moveDir.normalized * _finalSpeed * Time.deltaTime);
            }
        }
        else
        {
            _isMoving = false;
        }
    }

    public void ApplyGravity()
    {
        if (!_isFlying)
        {
            _isGrounded = Physics.CheckSphere(_groundCheckTransform.position, _groundCheckRadius, _groundLayer);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2;
            }

            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
    }

    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _isRunning)
        {
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }

        if (_isSprinting)
        {
            _finalSpeed = _movementSpeed * _sprintMultiplier;
        }
        else
        {
            _finalSpeed = _movementSpeed;
        }
    }

    public void Fly()
    {
        if (Input.GetKeyDown(KeyCode.G) && _isFlying)
        {
            _isFlying = false;
            _playerAnimator.gameObject.SetActive(true);
            _alternativeFlyingModel.gameObject.SetActive(false);
            _movementSpeed /= 8;
        }
        else if (Input.GetKeyDown(KeyCode.G) && !_isFlying)
        {
            _isFlying = true;
            _playerAnimator.gameObject.SetActive(false);
            _alternativeFlyingModel.gameObject.SetActive(true);
            _movementSpeed *= 8;
        }
        if (_isFlying)
        {
            Sprint();

            Vector3 moveDir = Vector3.zero;
            if (Input.GetKey(KeyCode.E) && !(_characterController.transform.position.y >= _maxFlyingHeight))
            {
                moveDir.y = 1f;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                moveDir.y = -1f;
            }
            else
            {
                moveDir.y = 0f;
            }
            _characterController.Move(moveDir.normalized * _finalSpeed * Time.deltaTime);
        }
    }

    public void AnimationController()
    {
        if (_isMoving && !_isSprinting)
        {
            _playerAnimator.SetBool("IsWalking", true);
            _playerAnimator.SetBool("IsRunning", false);
        }
        else if (_isMoving && _isSprinting)
        {
            _playerAnimator.SetBool("IsWalking", true);
            _playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            _playerAnimator.SetBool("IsWalking", false);
            _playerAnimator.SetBool("IsRunning", false);
        }

        if (_velocity.y > 0 && !_isGrounded)
        {
            _playerAnimator.SetBool("IsJumping", true);
        }
        else if (_velocity.y < 0)
        {
            _playerAnimator.SetBool("IsJumping", false);
        }

        if (_isGrounded)
        {
            _playerAnimator.SetBool("IsGrounded", true);
        }
        else
        {
            _playerAnimator.SetBool("IsGrounded", false);
        }

        if (!IsOnFoot)
        {
            _playerAnimator.SetBool("IsGrounded", true);
            _playerAnimator.SetBool("IsJumping", false);
            _playerAnimator.SetBool("IsWalking", false);
            _playerAnimator.SetBool("IsRunning", false);

        }
    }


    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (_isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(_groundCheckTransform.position, _groundCheckRadius);
    }
}
