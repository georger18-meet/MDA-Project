using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Measurements
{
   BPM, PainLevel, RespiratoryRate, CincinnatiLevel,
   BloodSuger, BloodPressure, OxygenSaturation, ETCO2
}

public struct ActionsData
{
    public ActionsManager AOM;
    public Patient Patient;
    public GameObject Player;
    public GameObject Monitor;
    public Roles RolesAD;

    public ActionsData(ActionsManager aom, Patient patient, GameObject player, GameObject monitor, Roles roles)
    {
        AOM = aom;
        Patient = patient;
        Player = player;
        Monitor = monitor;
        RolesAD = roles;
    }
}
