using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ActionTemplates : MonoBehaviour
{
    [SerializeField] private DocumentationLogManager _docLog;
    [SerializeField] private float _alertTimer;

    #region Most Basic Tools
    public void OpenCloseDisplayWindow(GameObject window)
    {
        if (window.activeInHierarchy)
            window.SetActive(false);
        else
            window.SetActive(true);

        print($"Opend /Closed {window.name}");
    }

    public void UpdateDisplayWindow(GameObject window, TextMeshProUGUI text, int newValue)
    {
        TextMeshProUGUI oldText = text;
        text.text = newValue.ToString();

        print($"Updated {oldText} in {window.name} to {text}");
    }

    public void ShowAlertWindow(GameObject alertWindow, TextMeshProUGUI alertTitle, TextMeshProUGUI alertText, string valueName, string valueText)
    {
        
        _alertTimer = 0;
        alertWindow.SetActive(true);
        //OpenDisplayWindow(window);

        alertTitle.text = valueName;
        alertText.text = valueText;

        if (_alertTimer > 3)
            alertWindow.SetActive(false);
    }

    public void ChangeMeasurement(int currentValue, int newValue)
    {
        currentValue = newValue;

        print($"Changed {currentValue} to {newValue}");
    }

    public void SpawnEquipment(GameObject additionalEquipment, Transform desiredPositionTransform)
    {
        Vector3 desiredPos = desiredPositionTransform.position;

        Instantiate(additionalEquipment, desiredPos, Quaternion.identity);

        print($"Spawned {additionalEquipment.name} at {desiredPos}");
    }

    public void MoveCharacter(Transform characterTransform, Transform desiredPositionTransform)
    {
        Vector3 oldPos = characterTransform.position;
        Vector3 newPos = desiredPositionTransform.position;

        characterTransform.position = newPos;

        print($"Moved character from {oldPos} to {newPos}");
    }

    // need fixing
    public void PlayAnimationOnCharacter(Transform characterTransform, Animation animation)
    {
        animation.Play();

        print($"Played animation {animation} on {characterTransform.name}");
    }

    public void ChangeCharacterTextrues(Texture currentTexture, Texture newTexture)
    {
        currentTexture = newTexture;

        print($"Changed Textures: {newTexture} instead of {currentTexture}");
    }

    public void UpdatePatientLog(string textToLog)
    {
        _docLog.LogThisText(textToLog);
    } 

    public bool IsPlayerJoined(ActionsOperatingManager AOM)
    {
        if (AOM.CheckIfPlayerJoined())
            return true;
        else
            return false;
    }
    #endregion

    // not sure about this - patient bool - isConsious vs if is currently conscious
    public void CheckStatus(bool isConscious, bool isPatientConscious)
    {

    }

    private void Update()
    {
        _alertTimer += Time.deltaTime;
    }

    #region Actions Components
    public void CheckMeasurement(PatientData patientData, GameObject alertWindow,
        TextMeshProUGUI alertTitle, TextMeshProUGUI alertText, string measurementTitle, string measurement)
    {
        alertTitle.text = measurementTitle;

        switch (measurement.ToLower())
        {
            case "bpm":
                measurementTitle = "??? ??";
                measurement = patientData.BPM.ToString();
                break;

            case "pain level":
                measurementTitle = "??? ???";
                measurement = patientData.PainLevel.ToString();
                break;

            case "respiratory rate":
                measurementTitle = "??? ?????";
                measurement = patientData.RespiratoryRate.ToString();
                break;

            case "cincinnati level":
                measurementTitle = "???? ????????";
                measurement = patientData.CincinnatiLevel.ToString();
                break;

            case "blood suger":
                measurementTitle = "???? ???";
                measurement = patientData.BloodSuger.ToString();
                break;

            case "blood pressure":
                measurementTitle = "??? ??";
                measurement = patientData.BloodPressure.ToString();
                break;

            case "oxygen saturation":
                measurementTitle = "???????";
                measurement = patientData.OxygenSaturation.ToString();
                break;

            case "etco2":
                measurementTitle = "ETCO2";
                measurement = patientData.ETCO2.ToString();
                break;

            default:
                break;
        }
        ShowAlertWindow(alertWindow, alertTitle, alertText, measurementTitle, measurement);

        UpdatePatientLog($"Patient's {measurementTitle} is {measurement}");
    }
    #endregion
}
