using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstPersonLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;

    public Transform PlayerBody;

    public Transform FPSCamPos;

    private float xRotation = 0f;

    private CinemachineBrain _cinemachineBrain;

    // Start is called before the first frame update
    void Start()
    {
        _cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_cinemachineBrain.enabled)
        {
            transform.position = FPSCamPos.position;

            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
