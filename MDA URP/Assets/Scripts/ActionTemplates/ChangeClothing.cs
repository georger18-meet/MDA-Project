using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothing : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ActionsManager _actionManager;
    [SerializeField] private ActionTemplates _actionTemplates;

    [Header("Component's Data")]
    [SerializeField] private Material _newShirtMaterial;
    [SerializeField] private Material _newPantsMaterial;
    [SerializeField] private string _textureToChange, _alertContent;


    public void ChangeClothingAction(int measurementNumber)
    {
        if (!_actionManager.CurrentPatientScript.IsPlayerJoined(_actionManager.PlayerData))
            return;

        // loops throughout measurementList and catches the first element that is equal to measurementNumber
        Measurements measurements = _actionManager.MeasurementList.FirstOrDefault(item => item == (Measurements)measurementNumber);
        _actionManager.CurrentPatientData.PatientShirtMaterial = _newShirtMaterial;
        _actionManager.CurrentPatientData.PatientPantsMaterial = _newPantsMaterial;


        _actionTemplates.ShowAlertWindow(_textureToChange, _alertContent);
        _actionTemplates.UpdatePatientLog($"Patient's {_textureToChange} is: {_alertContent}");
    }
}
