using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsOperatingHandler
{
    public void OpenNoBagActionMenu(GameObject noBagActionMenu)
    {
        if (!noBagActionMenu.activeInHierarchy)
            noBagActionMenu.SetActive(true);
        else
            noBagActionMenu.SetActive(false);
    }

    public void RunAction(ActionsOperatingManager AOM, Patient patient, GameObject player, GameObject monitor, int actionIndex)
    {
        switch (actionIndex)
        {
            case 0:
                AskPainLevel(AOM, patient);
                break;
            case 1:
                DoHeartMassage(AOM, patient, player);
                break;
            case 2:
                Defibrillation(AOM, patient, player, monitor);
                break;
            default:
                break;
        }
    }

    // Pain Level
    public void AskPainLevel(ActionsOperatingManager AOM, Patient patient)
    {
        if (!AOM.CheckIfPlayerJoined())
            return;
        else
            patient.PatientInfoSO.PainLevel = patient.PatientInfoSO.PainPlaceholderAnswer;

        Debug.Log(patient.name + "'s Pain Level: " + patient.PatientInfoSO.PainLevel);
    }

    // Heart Massage
    public void DoHeartMassage(ActionsOperatingManager AOM, Patient patient, GameObject player)
    {
        if (!AOM.CheckIfPlayerJoined())
            return;
        else
            player.transform.position = AOM.PlayerTreatingTr.position;

        Debug.Log("Operating Heart Massage On " + patient.name);
    }

    // Defibrillator
    public void Defibrillation(ActionsOperatingManager AOM, Patient patient, GameObject player, GameObject monitor)
    {
        if (!AOM.CheckIfPlayerJoined())
            return;
        else
        {
            player.transform.position = AOM.PlayerTreatingTr.position;
            MonoBehaviour.Instantiate(monitor, AOM.PatientEquipmentTr.position, Quaternion.identity);
        }

        Debug.Log("CLEAR!!! Defibrillator On " + patient.name);
    }
}
