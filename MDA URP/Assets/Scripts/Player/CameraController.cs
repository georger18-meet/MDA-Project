using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovementScript;

    [Header("Cameras")]
    [SerializeField] private GameObject _thirdPersonCameraScenemachine;
    [SerializeField] private GameObject _firstPersonCameraScenemachine;

    [SerializeField] private Transform _mainPlayerCamTransform;
    [SerializeField] private Transform _playerFirstPersonCamParent;

    private float _headRotation = 0f;
    private bool _isCursorFree;

    public float FirstPersonCamMouseSensitivity = 100f;

    private void Start()
    {
        if (!_playerMovementScript.UseOldControls)
        {
            _isCursorFree = true;
        }
    }

    private void Update()
    {
        FreeCursorToggle();
        FreeCursor();

        if (Input.GetKeyDown(KeyCode.V))
        {
            TogglePOV();
        }

        CameraFPSControls();
    }

    public void CameraFPSControls()
    {
        if (!_playerMovementScript.IsOnFoot)
        {
            _headRotation = 0f;
            _playerFirstPersonCamParent.localRotation = Quaternion.Euler(0f, _headRotation, 0f);
        }
        else 
        {
            float mouseX = Input.GetAxis("Mouse X") * FirstPersonCamMouseSensitivity * Time.deltaTime;
            _headRotation += mouseX;
            _headRotation = Mathf.Clamp(_headRotation, -90f, 90f);
            _playerFirstPersonCamParent.localRotation = Quaternion.Euler(0f, _headRotation, 0f);
        }
    }

    public void SetFirstPersonMouseSensetivity()
    {
        if (_playerMovementScript.UseFirstPersonCam)
        {
            float mouseX = Input.GetAxis("Mouse X") * FirstPersonCamMouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);
        }
    }

    public void SetPOV(string pov)
    {
        if (pov == "FirstPerson")
        {
            _playerMovementScript.UseFirstPersonCam = true;
            _firstPersonCameraScenemachine.SetActive(true);
            _thirdPersonCameraScenemachine.SetActive(false);
        }
        else if (pov == "ThirdPerson")
        {
            _playerMovementScript.UseFirstPersonCam = false;
            _thirdPersonCameraScenemachine.SetActive(true);
            _firstPersonCameraScenemachine.SetActive(false);
        }
    }

    public void TogglePOV()
    {
        if (_playerMovementScript.UseFirstPersonCam)
        {
            _playerMovementScript.UseFirstPersonCam = false;
            _thirdPersonCameraScenemachine.SetActive(true);
            _firstPersonCameraScenemachine.SetActive(false);
        }
        else
        {
            _playerMovementScript.UseFirstPersonCam = true;
            _firstPersonCameraScenemachine.SetActive(true);
            _thirdPersonCameraScenemachine.SetActive(false);
        }
    }

    private void FreeCursorToggle()
    {
        if (!_playerMovementScript.UseOldControls)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _isCursorFree = false;
            }
            if (Input.GetMouseButtonUp(1))
            {
                _isCursorFree = true;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                _isCursorFree = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _isCursorFree = true;
            }
        }
    }

    private void FreeCursor()
    {
        if (!_playerMovementScript.UseOldControls)
        {
            if (!_isCursorFree)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                _mainPlayerCamTransform.GetComponent<CinemachineBrain>().enabled = true;

                float targetAngle = _mainPlayerCamTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _playerMovementScript.RotationSmoothVelocity, _playerMovementScript.RotationSmootingTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _mainPlayerCamTransform.GetComponent<CinemachineBrain>().enabled = false;
            }
        }
        else
        {
            if (!_isCursorFree)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                _mainPlayerCamTransform.GetComponent<CinemachineBrain>().enabled = true;

            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _mainPlayerCamTransform.GetComponent<CinemachineBrain>().enabled = false;
            }
        }
    }
}
