using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorCollision : MonoBehaviour
{
    public bool SeatOccupied = false;
    public int SeatNum;
    public GameObject CollidedPlayer;
    public Transform SeatPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnterExitVehicle();
    }

    private void EnterExitVehicle()
    {
        if (CollidedPlayer != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !SeatOccupied)
            {
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
