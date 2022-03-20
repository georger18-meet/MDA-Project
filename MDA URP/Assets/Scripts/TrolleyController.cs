using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyController : MonoBehaviour
{
    public bool TakeOutBed;
    public bool Folded;
    public bool HasPatient;
    public GameObject Patient;
    public GameObject Player;
    public GameObject TrolleyLegs;
    public GameObject InteractionsBar;
    public Transform PatientPosOnBed;
    public Transform PatientPosOffBed;
    public Transform TrolleyPosInCar;

    private bool _isFollowingPlayer;
    private bool _inCar;

    // Start is called before the first frame update
    void Start()
    {
        InteractionsBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AlwaysChecking();
    }

    private void AlwaysChecking()
    {
        // In Car
        if (_inCar)
        {
            Folded = true;
        }

        // Fold
        if (Folded)
        {
            TrolleyLegs.SetActive(false);
        }
        else if (!Folded)
        {
            TrolleyLegs.SetActive(true);
        }

        // Follow Player
        if (Player != null)
        {
            if (_isFollowingPlayer)
            {
                gameObject.transform.SetParent(Player.transform);
            }
            else if (!_isFollowingPlayer)
            {
                gameObject.transform.SetParent(null);
            }
        }

        // Take Out Bed
        TakeOutReturnBed();
    }

    public void ShowInteractionsToggle()
    {
        if (InteractionsBar.activeInHierarchy)
        {
            InteractionsBar.SetActive(false);
        }
        else if (!InteractionsBar.activeInHierarchy)
        {
            InteractionsBar.SetActive(true);
        }
    }

    public void FoldUnfoldToggle()
    {
        if (Folded)
        {
            Folded = false;
        }
        else if (!Folded)
        {
            Folded = true;
        }
    }

    public void FollowPlayerToggle()
    {
        if (Player != null)
        {
            if (_isFollowingPlayer)
            {
                _isFollowingPlayer = false;
            }
            else if (!_isFollowingPlayer)
            {
                _isFollowingPlayer = true;
            }
        }
    }

    public void PutRemovePatient()
    {
        if (Patient != null && Folded && !HasPatient)
        {
            HasPatient = true;
            Patient.transform.position = PatientPosOnBed.position;
            Patient.transform.SetParent(this.transform);
        }
        else if (Patient != null && Folded && HasPatient && !_inCar)
        {
            HasPatient = false;
            Patient.transform.position = PatientPosOffBed.position;
            Patient.transform.SetParent(null);
        }
    }

    public void TakeOutBedToggle()
    {
        if (_inCar && !TakeOutBed)
        {
            TakeOutBed = true;
        }
        else if (_inCar && TakeOutBed)
        {
            TakeOutBed = false;
        }
    }

    private void TakeOutReturnBed()
    {
        if (_inCar && !TakeOutBed)
        {
            _isFollowingPlayer = false;
            transform.position = TrolleyPosInCar.position;
            transform.rotation = TrolleyPosInCar.rotation;
            transform.SetParent(TrolleyPosInCar);
        }
        else if (_inCar && TakeOutBed)
        {
            _isFollowingPlayer = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.gameObject;
        }
        if (other.CompareTag("Patient"))
        {
            Patient = other.gameObject;
        }
        if (other.CompareTag("Car"))
        {
            _inCar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = null;
        }
        if (other.CompareTag("Patient"))
        {
            Patient = null;
        }
        if (other.CompareTag("Car"))
        {
            _inCar = false;
        }
    }
}
