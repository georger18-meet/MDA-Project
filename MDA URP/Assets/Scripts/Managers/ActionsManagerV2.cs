using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ActionsManagerV2 : MonoBehaviour
{
    public List<Measurements> MeasurementList;

    [field: SerializeField] public PlayerData PlayerData { get; private set; }

    #region Data References
    [Header("Data & Scripts")]
    public List<PatientV2> AllPatients;
    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
        PlayerData = GetComponent<PlayerData>();
    }
    #endregion

    #region Assignment
    // Triggered upon Clicking on the Patient
    public void OnPatientClicked()
    {
        //if ()
    }
    #endregion
}