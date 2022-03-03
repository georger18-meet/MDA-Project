using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform Cam;
    public Transform GroundCheck;
    public Animator PlayerAnimator;
    private CharacterController _characterController;

    public float MoveSpeed = 6f;
    public float RotationSmoothTime = 0.1f;
    public float Gravity = -9.81f;
    public float GroundDistance = 0.5f;
    public LayerMask GroundMask;
    public float JumpHeight = 3f;
    public float SprintMultiplier = 2f;
    public float MaxFlyingHeight = 100f;
    public bool InControl = true;
    public bool EnableJump = true;
    public bool EnableSprint = true;
    public bool EnableCrouch = true;
    public bool EnableFly = false;

    private float _rotationSmoothVelocity;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isMoving;
    private bool _isSprinting;
    private float _TotalMoveSpeed;
    private bool _isCrouching;
    private Vector3 _standingOffset;
    private Vector3 _crouchOffset;
    private float _standingHeight;
    private float _crouchHeight;

    //--------------------------------
    // Main Unity Methods
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _standingHeight = _characterController.height;
        _standingOffset = _characterController.center;
        _crouchHeight = _standingHeight / 2;
        _crouchOffset.y = (_crouchHeight - 2) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (InControl)
        {
            MovePlayer();
            ApplyGravity();
            Jump();
            Crouch();
            Fly();
            AnimationController();
        }
        else
        {
            AnimationController();
        }
    }


    //--------------------------------
    // Created Methods

    public void MovePlayer()
    {
        // Calculate Direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // If There's Input
        if (direction.magnitude >= 0.1f)
        {
            _isMoving = true;
            // Rotate Player to Direction of Movement
            // *(The "+ Cam.eulerAngles.y" Makes the Player Face Look Direction)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationSmoothVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move Towards Look Direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // Apply Sprint
            Sprint();
            // Move Player
            _characterController.Move(moveDir.normalized * _TotalMoveSpeed * Time.deltaTime);
        }
        else
        {
            _isMoving = false;
        }
    }

    public void ApplyGravity()
    {
        if (!EnableFly)
        {
            _isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2;
            }

            _velocity.y += Gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded && EnableJump)
        {
            _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }
    }

    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && EnableSprint)
        {
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }

        if (_isSprinting)
        {
            _TotalMoveSpeed = MoveSpeed * SprintMultiplier;
        }
        else
        {
            _TotalMoveSpeed = MoveSpeed;
        }
    }

    public void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && EnableCrouch && _isGrounded)
        {
            _isCrouching = true;
        }
        else
        {
            _isCrouching = false;
        }

        if (_isCrouching)
        {
            _characterController.height = _crouchHeight;
            _characterController.center = _crouchOffset;
        }
        else
        {
            _characterController.height = _standingHeight;
            _characterController.center = _standingOffset;
        }
    }

    public void Fly()
    {
        if (Input.GetKeyDown(KeyCode.G) && EnableFly)
        {
            EnableFly = false;
            EnableJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.G) && !EnableFly)
        {
            EnableFly = true;
            EnableJump = false;
        }
        if (EnableFly)
        {
            Sprint();

            Vector3 moveDir = Vector3.zero;
            if (Input.GetKey(KeyCode.E) && !(_characterController.transform.position.y >= MaxFlyingHeight))
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
            _characterController.Move(moveDir.normalized * _TotalMoveSpeed * Time.deltaTime);
        }
    }

    public void AnimationController()
    {
        if (_isMoving && !_isSprinting)
        {
            PlayerAnimator.SetBool("IsWalking", true);
            PlayerAnimator.SetBool("IsRunning", false);
        }
        else if (_isMoving && _isSprinting)
        {
            PlayerAnimator.SetBool("IsWalking", true);
            PlayerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsWalking", false);
            PlayerAnimator.SetBool("IsRunning", false);
        }

        if (_velocity.y > 0 && !_isGrounded)
        {
            PlayerAnimator.SetBool("IsJumping", true);
        }
        else if (_velocity.y < 0)
        {
            PlayerAnimator.SetBool("IsJumping", false);
        }

        if (_isGrounded)
        {
            PlayerAnimator.SetBool("IsGrounded", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsGrounded", false);
        }

        if (!InControl)
        {
            PlayerAnimator.SetBool("IsGrounded", true);
            PlayerAnimator.SetBool("IsJumping", false);
            PlayerAnimator.SetBool("IsWalking", false);
            PlayerAnimator.SetBool("IsRunning", false);

        }
    }
}
