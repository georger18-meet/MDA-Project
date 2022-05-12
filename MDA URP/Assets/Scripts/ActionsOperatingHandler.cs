using System;
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


public struct ActionData
{
    public ActionsOperatingManager AOM;
    public Patient Patient;
    public GameObject Player;
    public GameObject Monitor;
    public Roles RolesAD;

    public ActionData(ActionsOperatingManager aom, Patient patient, GameObject player, GameObject monitor, Roles roles)
    {
        AOM = aom;
        Patient = patient;
        Player = player;
        Monitor = monitor;
        RolesAD = roles;
    }
}

public class ActionsOperatingHandler
{
    public List<Action<ActionData>> ActionsList = new List<Action<ActionData>>();

    public ActionsOperatingHandler()
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

    public void RunAction(ActionsOperatingManager aom, Patient patient, GameObject player, GameObject monitor, Roles roles, int actionIndex)
    {
        ActionData actionData = new ActionData(aom, patient, player, monitor, roles);

        ActionsList[actionIndex].Invoke(actionData);
    }

    // Pain Level
    public void AskPainLevel(ActionData actionData)
    {
        if (!actionData.AOM.CheckIfPlayerJoined())
            return;
        else
            actionData.Patient.PatientInfoSO.PainLevel = actionData.Patient.PatientInfoSO.PainPlaceholderAnswer;

        Debug.Log(actionData.Patient.name + "'s Pain Level: " + actionData.Patient.PatientInfoSO.PainLevel);
    }

    // Heart Massage
    public void DoHeartMassage(ActionData actionData)
    {
        if (!actionData.AOM.CheckIfPlayerJoined())
            return;
        else
            actionData.Player.transform.position = actionData.AOM.PlayerTreatingTr.position;

        Debug.Log("Operating Heart Massage On " + actionData.Patient.name);
    }

    // Defibrillator
    public void Defibrillation(ActionData actionData)
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
