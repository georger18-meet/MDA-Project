using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Patient Data",menuName = "Patient Data") ]
public class PatientData : ScriptableObject
{
    // General Info
    [Header("Patient Informaion")]
    public string SureName;
    public string LastName;
    public int Id, Age;
    public string Gender;
    public int PhoneNumber;
    public string MedicalCompany, AddressLocation, Complaint;

    // Health Data
    [Header("Measurments")]
    public int BPM;
    public int PainLevel, RespiratoryRate, CincinnatiLevel, BloodSuger, BloodPressure, OxygenSaturation, ETCO2;
}
