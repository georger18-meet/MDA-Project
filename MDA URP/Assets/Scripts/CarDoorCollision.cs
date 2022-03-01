using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorCollision : MonoBehaviour
{
    public int SeatNum;
    public GameObject CollidedPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollidedPlayer = other.gameObject;
            transform.parent.GetComponent<CarController>().CollisionDetected(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollidedPlayer = null;
            transform.parent.GetComponent<CarController>().CollisionDetected(null);
        }
    }
}
