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
    public void OpenDisplayWindow(GameObject window)
    {
        if (window.activeInHierarchy)
            window.SetActive(false);
        else
            window.SetActive(true);

        print($"Closed {window.name}");
    }

    public void UpdateDisplayWindow(GameObject window, TextMeshProUGUI text, int newValue)
    {
        TextMeshProUGUI oldText = text;
        text.text = newValue.ToString();

        print($"Updated {oldText} in {window.name} to {text}");
    }

    public void ShowAlertWindow(GameObject playerUI, GameObject window, TextMeshProUGUI alertTitle, TextMeshProUGUI alertText, int value)
    {
        _alertTimer = 0;
        Instantiate(window, playerUI.transform);
        //OpenDisplayWindow(window);

        alertText.text = value.ToString();

        if (_alertTimer > 3)
            Destroy(window);
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
    public void CheckMeasurement(PaitentBaseInfoSO PatientInfo, GameObject playerUI, GameObject window, TextMeshProUGUI alertTitle, TextMeshProUGUI alertText, string measurementTitle, int measurement)
    {
        alertTitle.text = measurementTitle;
        ShowAlertWindow(playerUI, window, alertTitle, alertText, measurement);

        UpdatePatientLog($"Patient's {measurementTitle} is {measurement}");
    }

    public void 
    #endregion
}
