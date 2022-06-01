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
    //[SerializeField] private Transform _mainPlayerCamTransform;
    //[SerializeField] private Transform _playerFirstPersonCamParent;

    [Header("Momvement")]
    [SerializeField] private CharacterController _characterController;
    //[SerializeField] private GameObject _thirdPersonCameraScenemachine;
    //[SerializeField] private GameObject _firstPersonCameraScenemachine;
    [SerializeField] private float _movementSpeed = 4f, _sprintMultiplier = 2f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _maxFlyingHeight = 100f;
    public float RotationSmootingTime = 0.1f, RotationSmoothVelocity;
    //private float _headRotation = 0f;
    //public float _firstPersonCamMouseSensitivity = 100f;
    //private bool _isCursorFree;
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

    //[SerializeField] private bool _isJumping = true;
    //[SerializeField] private bool _isCrouching2 = true;
    //private bool _isCrouching;
    //private Vector3 _standingOffset;
    //private Vector3 _crouchOffset;
    //private float _standingHeight;
    //private float _crouchHeight;


    //--------------------------------
    // Main Unity Methods
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //_standingHeight = _characterController.height;
        //_standingOffset = _characterController.center;
        //_crouchHeight = _standingHeight / 2;
        //_crouchOffset.y = -(_crouchHeight / 2);

        
    }

    // Update is called once per frame
    void Update()
    {
        //FreeCursorToggle();
        //FreeCursor();

        if (IsOnFoot)
        {
            _characterController.enabled = true;
            MovePlayer();
            ApplyGravity();
            //Jump();
            //Crouch();
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

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    TogglePOV();
        //}

        //CameraFPSControls();
        CheckInteraction();
    }


    //--------------------------------
    // Created Methods


    //public void CameraFPSControls()
    //{
    //    if (IsOnFoot)
    //    {
    //        _headRotation = 0f;
    //        _playerFirstPersonCamParent.localRotation = Quaternion.Euler(0f, _headRotation, 0f);
    //    }
    //    else if (!IsOnFoot)
    //    {
    //        float mouseX = Input.GetAxis("Mouse X") * _firstPersonCamMouseSensitivity * Time.deltaTime;
    //        _headRotation += mouseX;
    //        _headRotation = Mathf.Clamp(_headRotation, -90f, 90f);
    //        _playerFirstPersonCamParent.localRotation = Quaternion.Euler(0f, _headRotation, 0f);
    //    }
    //}

    public void MovePlayer()
    {
        // get input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // calculate Direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //if (_useFirstPersonCam)
        //{
        //    float mouseX = Input.GetAxis("Mouse X") * _firstPersonCamMouseSensitivity * Time.deltaTime;
        //
        //    transform.Rotate(Vector3.up * mouseX);
        //}

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
                // Apply Sprint
                Sprint();
                // Move Player
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

    //public void Jump()
    //{
    //    if (Input.GetButtonDown("Jump") && _isGrounded && _isJumping)
    //    {
    //        _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    //    }
    //}

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

    //public void Crouch()
    //{
    //    if (Input.GetKey(KeyCode.LeftControl) && _isCrouching2 && _isGrounded)
    //    {
    //        _isCrouching = true;
    //    }
    //    else
    //    {
    //        _isCrouching = false;
    //    }
    //
    //    if (_isCrouching)
    //    {
    //        _characterController.height = _crouchHeight;
    //        _characterController.center = _crouchOffset;
    //    }
    //    else
    //    {
    //        _characterController.height = _standingHeight;
    //        _characterController.center = _standingOffset;
    //    }
    //}

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

    //public void SetPOV(string pov)
    //{
    //    if (pov == "1st")
    //    {
    //        _useFirstPersonCam = true;
    //        _firstPersonCameraScenemachine.SetActive(true);
    //        _thirdPersonCameraScenemachine.SetActive(false);
    //    }
    //    else if (pov == "3rd")
    //    {
    //        _useFirstPersonCam = false;
    //        _thirdPersonCameraScenemachine.SetActive(true);
    //        _firstPersonCameraScenemachine.SetActive(false);
    //    }
    //}
    //
    //public void TogglePOV()
    //{
    //    if (_useFirstPersonCam)
    //    {
    //        _useFirstPersonCam = false;
    //        _thirdPersonCameraScenemachine.SetActive(true);
    //        _firstPersonCameraScenemachine.SetActive(false);
    //    }
    //    else
    //    {
    //        _useFirstPersonCam = true;
    //        _firstPersonCameraScenemachine.SetActive(true);
    //        _thirdPersonCameraScenemachine.SetActive(false);
    //    }
    //}

    public RaycastHit CheckInteraction()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 10f, _interactableLayer))
        {
            //Our custom method. 
            if (raycastHit.transform.gameObject.GetComponent<Collider>().enabled)
            {
                _indicatorIcon.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Object Hit: " + raycastHit.transform.gameObject.name);
                    Vector3 pointToLook = ray.GetPoint(Vector3.Distance(ray.origin, raycastHit.transform.position));
                    Debug.DrawLine(ray.origin, pointToLook, Color.cyan, 5f);
                    _indicatorSound.Play();
                    raycastHit.transform.gameObject.GetComponent<MakeItAButton>().EventToCall.Invoke();
                }
            }
        }
        else
        {
            _indicatorIcon.SetActive(false);
        }

        return raycastHit;
    }

    //private void FreeCursorToggle()
    //{
    //    if (!UseOldControls)
    //    {
    //        if (Input.GetMouseButtonDown(1))
    //        {
    //            _isCursorFree = false;
    //        }
    //        if (Input.GetMouseButtonUp(1))
    //        {
    //            _isCursorFree = true;
    //        }
    //    }
    //    else
    //    {
    //        if (Input.GetKeyUp(KeyCode.LeftAlt))
    //        {
    //            _isCursorFree = false;
    //        }
    //        if (Input.GetKeyDown(KeyCode.LeftAlt))
    //        {
    //            _isCursorFree = true;
    //        }
    //    }
    //}

    //private void FreeCursor()
    //{
    //    if (!UseOldControls)
    //    {
    //        if (!_isCursorFree)
    //        {
    //            Cursor.visible = false;
    //            Cursor.lockState = CursorLockMode.Locked;
    //            _mainPlayerCamTransform.GetComponent<CinemachineBrain>().enabled = true;
    //
    //            float targetAngle = _mainPlayerCamTransform.eulerAngles.y;
    //            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationSmoothVelocity, _rotationSmootingTime);
    //            transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //
    //        }
    //        else
    //        {
    //            Cursor.visible = true;
    //            Cursor.lockState = CursorLockMode.None;
    //            _mainPlayerCamTransform.GetComponent<CinemachineBrain>().enabled = false;
    //        }
    //    }
    //    else
    //    {
    //        if (!_isCursorFree)
    //        {
    //            Cursor.visible = false;
    //            Cursor.lockState = CursorLockMode.Locked;
    //            _mainPlayerCamTransform.GetComponent<CinemachineBrain>().enabled = true;
    //
    //        }
    //        else
    //        {
    //            Cursor.visible = true;
    //            Cursor.lockState = CursorLockMode.None;
    //            _mainPlayerCamTransform.GetComponent<CinemachineBrain>().enabled = false;
    //        }
    //    }
    //}

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
