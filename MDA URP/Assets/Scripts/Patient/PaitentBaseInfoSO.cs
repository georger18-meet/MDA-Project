using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Patient Info",menuName = "Patient Info") ]
public class PaitentBaseInfoSO : ScriptableObject
{
    // General Info
    [Header("Patient Informaion")]
    public string SureName, LastName;
    public int Id, Age;
    public string Gender;
    public int PhoneNumber;
    public string MedicalCompany, AddressLocation, Complaint;
    //public string eventPlace;

    // Health Data
    [Header("Measurments")]
    public int HeartRate, PainLevel, PainPlaceholderAnswer;

    // gal's additions (playerActions related)
    // public GameObject PatientManu, HeartRatePanel;
    // public TextMeshProUGUI HeartRateText, PainMeterText, BreathingText;
    // public int HeartRate, PainMeter, Breathing, PainPlaceholderAnswer;
}
