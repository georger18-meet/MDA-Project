using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorCollision : MonoBehaviour
{
    public bool IsDoorOpen = false;
    public bool IsSeatOccupied = false;
    public int SeatNumber;
    public GameObject CollidingPlayer;
    public Transform SeatPosition;

    private Animator _doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Collider>().enabled = IsDoorOpen;
        EnterExitVehicle();
    }

    public void OpenCloseDoorToggle()
    {
        if (IsDoorOpen)
        {
            IsDoorOpen = false;
            _doorAnimator.SetBool("IsDoorOpen", false);
        }
        else if (!IsDoorOpen)
        {
            IsDoorOpen = true;
            _doorAnimator.SetBool("IsDoorOpen", true);
            if (IsSeatOccupied)
            {
                EnterExitToggle();
            }
        }
    }

    private void EnterExitVehicle()
    {
        if (CollidingPlayer != null)
        {
            if (IsSeatOccupied)
            {
                CollidingPlayer.transform.SetPositionAndRotation(SeatPosition.position, SeatPosition.rotation);
            }
        }
    }

    public void EnterExitToggle()
    {
        if (IsDoorOpen && CollidingPlayer != null)
        {
            if (!IsSeatOccupied)
            {
                PlayerControllerV2 playerController = CollidingPlayer.GetComponent<PlayerControllerV2>();
                OpenCloseDoorToggle();
                IsSeatOccupied = true;
                // use player driving state
            }
            else if (IsSeatOccupied)
            {
                IsSeatOccupied = false;
                CollidingPlayer.transform.position = gameObject.transform.position;
                // use player driving state
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !IsSeatOccupied)
        {
            CollidingPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsSeatOccupied)
        {
            CollidingPlayer = null;
        }
    }
}