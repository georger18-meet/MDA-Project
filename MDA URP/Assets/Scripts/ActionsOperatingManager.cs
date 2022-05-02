using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsOperatingManager : MonoBehaviour
{
    public Patient CurrentPatient;
    public PaitentBaseInfoSO _patientInfoSORef;
    public GameObject ActionPanel;

    private ActionsOperatingHandler _actionsOperatingHandler;

    // Start is called before the first frame update
    void Start()
    {
        ActionPanel.SetActive(false);
        _actionsOperatingHandler = new ActionsOperatingHandler();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallAction(int actionNumInList)
    {
        if (_patientInfoSORef != null)
        {
            _actionsOperatingHandler.RunAction(actionNumInList, CurrentPatient);
        }
    }

    #region getting patient data
    private void GetPatientInfo()
    {
        if (CurrentPatient.CheckIfPlayerJoined())
        {
            ActionPanel.SetActive(true);
            _patientInfoSORef = CurrentPatient.PatientInfoSO;
        }
        else
        {
            ActionPanel.SetActive(false);
            _patientInfoSORef = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Patient"))
        {
            CurrentPatient = other.gameObject.GetComponent<Patient>();
            GetPatientInfo();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Patient"))
        {
            ActionPanel.SetActive(false);
            CurrentPatient = null;
            _patientInfoSORef = null;
        }
    }
    #endregion
}
