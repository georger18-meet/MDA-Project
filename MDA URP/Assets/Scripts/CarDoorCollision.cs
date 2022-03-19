using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorCollision : MonoBehaviour
{
    public bool DoorOnLeft;
    public bool DoorOpen = false;
    public bool SeatOccupied = false;
    public int SeatNum;
    public GameObject CollidedPlayer;
    public Transform SeatPos;

    private Animator _doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnterExitVehicle();
    }

    public void OpenCloseDoorToggle()
    {
        if (DoorOpen)
        {
            DoorOpen = false;
            _doorAnimator.SetBool("IsDoorOpen", false);
        }
        else if (!DoorOpen)
        {
            DoorOpen = true;
            _doorAnimator.SetBool("IsDoorOpen", true);
        }
    }

    private void EnterExitVehicle()
    {
        if (CollidedPlayer != null)
        {
            if (DoorOpen)
            {
                if (Input.GetKeyDown(KeyCode.E) && !SeatOccupied)
                {
                    OpenCloseDoorToggle();
                    SeatOccupied = true;
                    CollidedPlayer.GetComponent<ThirdPersonMovement>().InControl = false;
                    CollidedPlayer.GetComponent<ThirdPersonMovement>().TogglePOV();
                }
                else if (Input.GetKeyDown(KeyCode.E) && SeatOccupied)
                {
                    SeatOccupied = false;
                    CollidedPlayer.transform.position = gameObject.transform.position;
                    CollidedPlayer.GetComponent<ThirdPersonMovement>().InControl = true;
                    CollidedPlayer.GetComponent<ThirdPersonMovement>().TogglePOV();
                }
            }

            if (SeatOccupied)
            {
                CollidedPlayer.transform.position = SeatPos.position;
                CollidedPlayer.transform.rotation = SeatPos.rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !SeatOccupied)
        {
            CollidedPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!SeatOccupied)
        {
            CollidedPlayer = null;
        }
    }
}
