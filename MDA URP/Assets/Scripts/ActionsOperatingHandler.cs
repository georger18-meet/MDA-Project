using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsOperatingHandler : MonoBehaviour
{
    public void RunAction(int actionIndex, Patient patient)
    {
        switch (actionIndex)
        {
            case 0:
                GetPainLevel(patient);
                break;
            case 1:
                DoHeartMassage(patient);
                break;
            case 2:
                Defibrillator(patient);
                break;
            default:
                break;
        }
    }

    // Pain Level
    public void GetPainLevel(Patient patient)
    {
        patient.PatientInfoSO.PainLevel = patient.PatientInfoSO.PainPlaceholderAnswer.ToString();
        Debug.Log(patient.name + "'s Pain Level: " + patient.PatientInfoSO.PainLevel);
    }

    // Heart Massage
    public void DoHeartMassage(Patient patient)
    {
        Debug.Log("Operating Heart Massage On " + patient.name);
    }

    // Defibrillator
    public void Defibrillator(Patient patient)
    {
        Debug.Log("CLEAR!!! Defibrillator On " + patient.name);
    }
}
