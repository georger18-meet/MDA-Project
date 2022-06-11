using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothing : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ActionsManagerV2 _actionManager;
    [SerializeField] private ActionTemplates _actionTemplates;

    [Header("Component's Data")]
    [SerializeField] private Material _newShirtMaterial;
    [SerializeField] private Material _newPantsMaterial;
    [SerializeField] private string _textureToChange, _alertContent;


    public void ChangeClothingAction(int measurementNumber)
    {
        if (!PlayerData.Instance.CurrentPatientTreating.IsPlayerJoined(PlayerData.Instance))
            return;

        // loops throughout measurementList and catches the first element that is equal to measurementNumber
        Measurements measurements = _actionManager.MeasurementList.FirstOrDefault(item => item == (Measurements)measurementNumber);
        PlayerData.Instance.CurrentPatientTreating.PatientData.PatientShirtMaterial = _newShirtMaterial;
        PlayerData.Instance.CurrentPatientTreating.PatientData.PatientPantsMaterial = _newPantsMaterial;


        _actionTemplates.ShowAlertWindow(_textureToChange, _alertContent);
        _actionTemplates.UpdatePatientLog($"Patient's {_textureToChange} is: {_alertContent}");
    }
}
