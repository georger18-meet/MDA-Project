using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMassages : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ActionsManager _actionManager;
    [SerializeField] private ActionTemplates _actionTemplates;

    public void DoHeartMassage()
    {
        if (!_actionManager.CheckIfPlayerJoined())
            return;

        _actionManager.PlayerData.transform.position = _actionManager.PlayerTreatingTr.position;
        // rotate to patient
        // play cpr animation
        // change heart rate after x seconds

        _actionTemplates.UpdatePatientLog($"Performed Heart Massages");
        Debug.Log("Operating Heart Massage On " /*+ _actionData.Patient.name*/);
    }
}
