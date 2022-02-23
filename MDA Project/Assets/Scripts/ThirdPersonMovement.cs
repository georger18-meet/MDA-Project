using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform Cam;
    private CharacterController _characterController;

    public float MoveSpeed = 6f;
    public float RotationSmoothTime = 0.1f;
    private float _rotationSmoothVelocity;

    private void Awake()
    {
       _characterController = GetComponent<CharacterController>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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
            // Rotate Player to Direction of Movement
            // *(The "+ Cam.eulerAngles.y" Makes the Player Face Look Direction)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationSmoothVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move Towards Look Direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // Move Player
            _characterController.Move(moveDir.normalized * MoveSpeed * Time.deltaTime);
        }
    }
}
