using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ActionsManagerV2 : MonoBehaviour
{
    public static ActionsManagerV2 Instance;

    public List<Measurements> MeasurementList;

    #region Data References
    [Header("Data & Scripts")]
    [SerializeField] private UIManager _uIManager;
    
    public List<PatientV2> AllPatients;

    private PatientV2 _lastClickedPatient;
    private PatientData _lastClickedPatientData;
    #endregion


    #region MonoBehaviour Callbacks
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Assignment
    // Triggered upon Clicking on the Patient
    public void OnPatientClicked(PatientV2 patient)
    {
        if (patient == null)
        {
            return;
        }

        _lastClickedPatient = patient;

        PatientData currentPatientData = patient != null ? patient.PatientData : null;

        _lastClickedPatientData = currentPatientData;

        if (!patient.IsPlayerJoined(PlayerData.Instance))
        {
            _uIManager.JoinPatientPopUp.SetActive(true);
        }
        else
        {
            SetupPatientInfoDisplay();
            _uIManager.PatientMenuParent.SetActive(true);
        }
    }

    public void OnJoinPatient(bool isJoined)
    {
        if (isJoined)
        {
            _lastClickedPatient.AddUserToTreatingLists(PlayerData.Instance);

            SetupPatientInfoDisplay();

            _uIManager.JoinPatientPopUp.SetActive(false);
            _uIManager.PatientMenuParent.SetActive(true);
            _uIManager.PatientInfoParent.SetActive(false);
        }
        else
        {
            _uIManager.JoinPatientPopUp.SetActive(false);
        }
    }

    private void SetupPatientInfoDisplay()
    {
        _uIManager.SureName.text = _lastClickedPatientData.SureName;
        _uIManager.LastName.text = _lastClickedPatientData.LastName;
        _uIManager.Gender.text = _lastClickedPatientData.Gender;
        _uIManager.Adress.text = _lastClickedPatientData.AddressLocation;
        _uIManager.InsuranceCompany.text = _lastClickedPatientData.MedicalCompany;
        _uIManager.Complaint.text = _lastClickedPatientData.Complaint;

        _uIManager.Age.text = _lastClickedPatientData.Age.ToString();
        _uIManager.Id.text = _lastClickedPatientData.Id.ToString();
        _uIManager.PhoneNumber.text = _lastClickedPatientData.PhoneNumber.ToString();
    }

    public void LeavePatient(PatientV2 patient)
    {
        Debug.Log("Attempting leave patient");
        // if (_photonView.isMine)
        // {
            _uIManager.CloseAllPatientWindows();
            patient.TreatingUsers.Remove(PlayerData.Instance);
            Debug.Log("Left Patient Succesfully");
        // }


    }
    #endregion
}