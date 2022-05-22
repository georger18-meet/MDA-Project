using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CheckMeasurement : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ActionsOperatingManager AOM;
    [SerializeField] private ActionTemplates _actionTemplates;

    [Header("Component's Data")]
    [SerializeField] private string _measurementTitle;
    
    private int _measurement;

    public void CheckMeasurementAction()
    {
        if (!AOM.CheckIfPlayerJoined())
            return;
        
        switch (_measurementTitle.ToLower())
        {
            case "bpm":
                _measurementTitle = "bpm:";
                _measurement = AOM.CurrentPatientData.BPM;
                break;

            case "pain level":
                _measurementTitle = "pain level:";
                _measurement = AOM.CurrentPatientData.PainLevel;
                break;

            case "respiratory rate":
                _measurementTitle = "respiratory rate:";
                _measurement = AOM.CurrentPatientData.RespiratoryRate;
                break;

            case "cincinnati level":
                _measurementTitle = "cincinnati level:";
                _measurement = AOM.CurrentPatientData.CincinnatiLevel;
                break;

            case "blood suger":
                _measurementTitle = "blood suger:";
                _measurement = AOM.CurrentPatientData.BloodSuger;
                break;

            case "blood pressure":
                _measurementTitle = "blood pressure:";
                _measurement = AOM.CurrentPatientData.BloodPressure;
                break;

            case "oxygen saturation":
                _measurementTitle = "oxygen saturation:";
                _measurement = AOM.CurrentPatientData.OxygenSaturation;
                break;

            case "etco2":
                _measurementTitle = "ETCO2:";
                _measurement = AOM.CurrentPatientData.ETCO2;
                break;

            default:
                break;
        }

        _actionTemplates.ShowAlertWindow(_measurementTitle, _measurement);
        _actionTemplates.UpdatePatientLog($"Patient's {_measurementTitle} is: {_measurement}");
    }
}
