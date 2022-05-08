using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ActionTemplates : MonoBehaviour
{
    [SerializeField] private DocumentationLogManager _docLog;

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
        //_docLog.
    }
    #endregion

    // not sure about this
    public void CheckStatus(bool isConscious, bool isPatientConscious)
    {

    }
}
