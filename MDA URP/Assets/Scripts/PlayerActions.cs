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
        // get patient info
        if (collision.gameObject.layer == 10)
        {
            if (_playerData.CheckPatientIsJoined(collision.gameObject.name))
            {
                _patientInfo = collision.gameObject.GetComponent<PaitentBaseInfoSO>();
                _patientTr = collision.gameObject.transform;
            }

            else
                return;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // forget patient info
        if (collision.gameObject.layer == 10)
            _patientInfo = null;
    }

    public void NoBag(string action)
    {
        switch (action.ToLower())
        {
            case "check for heartbeat":
                _patientInfo.BreathingText.text = _patientInfo.Breathing.ToString();
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
