using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTr, _patientTr;

    [SerializeField]
    private PaitentBaseInfoSO _patientInfo;

    [SerializeField]
    private PlayerData _playerData;

    [SerializeField]
    private List<GameObject> _addedItems;

    // Form

    private void OnCollisionEnter(Collision collision)
    {
        //* get patient info
        if (collision.collider.tag == "Patient")
        {
            //if (_playerData.CheckPatientIsJoined(collision.gameObject.name))
            //{
            //    _patientInfo = collision.gameObject.GetComponent<PaitentBaseInfoSO>();
            //    _patientTr = collision.gameObject.transform;
            //}
            //
            //else
            //    return;
        }
    }

    public void SetPatient(GameObject patient)
    {
        if (_playerData.CheckPatientIsJoined(patient.name))
        {
            // get patient info
            //_patientInfo = patient.GetComponent<Patient>().PatientInfoSO;
            _patientTr = patient.transform;
        }

        else
            return;
    }

    private void OnCollisionExit(Collision collision)
    {
        // forget patient info
        if (collision.collider.tag == "Patient")
        {
            _patientInfo = null;
            _patientTr = null;
        }
    }

    public void NoBag(string action)
    {
        switch (action.ToLower())
        {
            case "heart massage":
                if (_patientInfo.HeartRate == 0)
                _patientInfo.HeartRate = 65;
                // show heartbeat
                break;

            case "pain question":
                _patientInfo.PainMeterText.text = _patientInfo.PainPlaceholderAnswer.ToString();
                print($"8");
                break;

            case "defibrilation":
                break;

            default:
                break;
        }
    }
}
