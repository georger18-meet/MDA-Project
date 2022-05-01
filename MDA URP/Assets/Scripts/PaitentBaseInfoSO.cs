using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "New Patient Info",menuName = "Patient Info") ]
public class PaitentBaseInfoSO : ScriptableObject
{
    public string fullName;
    public string gender;
    public string addressLocation;
    public string medicalCompany;
    public string eventPlace;
    public string complaint;

    public int idNumber;
    public int age;
    public int phoneNumber;

    // gal's additions (playerActions related)
    public GameObject PatientManu, HeartRatePanel;
    public TextMeshProUGUI HeartRateText, PainMeterText, BreathingText;
    public int HeartRate, PainMeter, Breathing, PainPlaceholderAnswer;
}
