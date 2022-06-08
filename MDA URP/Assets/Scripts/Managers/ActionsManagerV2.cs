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
    public List<PatientV2> AllPatients;
    #endregion

    private PatientV2 _lastClickedPatient;

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
        _lastClickedPatient = patient;

        PatientData currentPatientData = patient != null ? patient.PatientData : null;

        if (currentPatientData == null)
        {
            return;
        }

        if (!patient.IsPlayerJoined(PlayerData.Instance))
        {
            //_joinPatientPopUp.SetActive(true);
            //_patientMenuParent.SetActive(true);
        }
        else
        {
            //SetupPatientInfoDisplay();
        }
    }

    public void OnJoinPatient(bool isJoined)
    {
        if (isJoined)
        {
            _lastClickedPatient.AddUserToTreatingLists(PlayerData.Instance);
            // need to verify that set operating crew is setting an empty group of maximum 4 and insitialize it with current player

            //SetOperatingCrew(_currentPatientScript.OperatingUserCrew);

            //SetupPatientInfoDisplay();
            //_joinPatientPopUp.SetActive(false);
            //_patientMenuParent.SetActive(true);
            //_patientInfoParent.SetActive(false);

        }
        else
        {
            //_joinPatientPopUp.SetActive(false);
        }
    }
    #endregion
}