using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Patient Info",menuName = "Patient Info") ]
public class PaitentBaseInfoSO : ScriptableObject
{
    // General Info
    [Header("General Info")]
    public string fullName;
    public string gender;
    public string addressLocation;
    public string medicalCompany;
    public string eventPlace;
    public string complaint;



    public int idNumber;
    public int age;
    public int phoneNumber;


    // Health Data
    [Header("Health Data")]
    public int HeartRate;
    public int PainLevel;
}
