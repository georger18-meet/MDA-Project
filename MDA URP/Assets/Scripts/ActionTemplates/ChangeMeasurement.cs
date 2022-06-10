using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMeasurement : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ActionsManager _actionManager;
    [SerializeField] private ActionTemplates _actionTemplates;

    [Header("Component's Data")]
    [SerializeField] private string _measurementTitle;
    [SerializeField] private int _newMeasurement;

    public void ApplyMeasurementAction(int measurementNumber)
    {
        if (!_actionManager.CurrentPatientScript.IsPlayerJoined(_actionManager.PlayerData))
            return;

        // loops throughout measurementList and catches the first element that is equal to measurementNumber
        Measurements measurements = _actionManager.MeasurementList.FirstOrDefault(item => item == (Measurements)measurementNumber);
        _actionManager.CurrentPatientData.SetMeasurementName(measurementNumber, _newMeasurement);

        _actionTemplates.ShowAlertWindow(_measurementTitle, _newMeasurement);
        _actionTemplates.UpdatePatientLog($"Patient's {_measurementTitle} was changed");
    }
}