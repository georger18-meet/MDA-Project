using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Roles
{
    CFR, 
    Medic, 
    SeniorMedic, 
    Paramedic, 
    Doctor
}

public class ActionsHandler
{
    public List<Action<ActionsData>> ActionsList = new List<Action<ActionsData>>();

    public ActionsHandler()
    {
        AddActionsToList();
    }


    public void OpenNoBagActionMenu(GameObject noBagActionMenu)
    {
        if (!noBagActionMenu.activeInHierarchy)
            noBagActionMenu.SetActive(true);
        else
            noBagActionMenu.SetActive(false);
    }

    public void AddActionsToList()
    {
        ActionsList.Add(AskPainLevel);
        ActionsList.Add(DoHeartMassage);
        ActionsList.Add(Defibrillation);
    }

    public void RunAction(ActionsManager am, Patient patient, GameObject player, GameObject monitor, Roles roles, int actionIndex)
    {
        ActionsData actionData = new ActionsData(am, patient, player, monitor, roles);

        ActionsList[actionIndex].Invoke(actionData);
    }

    // Pain Level
    public void AskPainLevel(ActionsData actionData)
    {
        if (!actionData.AOM.CheckIfPlayerJoined())
            return;
        else
            //aT.CheckMeasurement();
            //actionData.Patient.PatientInfoSO.PainLevel = actionData.Patient.PatientInfoSO.PainPlaceholderAnswer;

        Debug.Log(actionData.Patient.name + "'s Pain Level: " + actionData.Patient.PatientData.PainLevel);
    }

    // Heart Massage
    public void DoHeartMassage(ActionsData actionData)
    {
        if (!actionData.AOM.CheckIfPlayerJoined())
            return;
        else
            actionData.Player.transform.position = actionData.AOM.PlayerTreatingTr.position;

        Debug.Log("Operating Heart Massage On " + actionData.Patient.name);
    }

    // Defibrillator
    public void Defibrillation(ActionsData actionData)
    {
        if (!actionData.AOM.CheckIfPlayerJoined() || (int)actionData.RolesAD <= 1)
        {
            Debug.Log("You Are NOT WORTHY!");
            return;
        }
        else
        {
            actionData.Player.transform.position = actionData.AOM.PlayerTreatingTr.position;
            MonoBehaviour.Instantiate(actionData.Monitor, actionData.AOM.PatientEquipmentTr.position, Quaternion.identity);
        }

        Debug.Log("CLEAR!!! Defibrillator On " + actionData.Patient.name);
    }
}
