using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<string> JoinedPatients;

    public bool CheckPatientIsJoined(string patientName)
    {
        bool isJoined = false;

        for (int i = 0; i < JoinedPatients.Count; i++)
        {
            if (JoinedPatients[i] == patientName)
            {
                print($"{patientName} is Joined");
                isJoined = true;
                break;
            }
            
            if (i == JoinedPatients.Count)
            {
                print($"{patientName} is not Joined");
                isJoined = false;
            }
        }

        return isJoined;
    }
}
