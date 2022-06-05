using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientV2 : MonoBehaviour
{
    #region Script References
    [Header("Data & Scripts")]
    public PatientData PatientData;
    #endregion

    #region Material References
    [SerializeField] private Material InitialShirt, InitialPants;
    [SerializeField] private Renderer Shirt, Pants;
    #endregion

    #region Public fields
    public Dictionary<string, int> OperatingUserCrew = new Dictionary<string, int>();
    public Animation PatientAnimation;
    #endregion

    #region private serialized fields
    [Header("Joined Crews & Players Lists")]
    public string[] CurrentlyTreatingUser;
    public int[] CurrentlyTreatingCrew;
    #endregion

    private void Start()
    {
        PatientData.PatientShirtMaterial = InitialShirt;
        PatientData.PatientPantsMaterial = InitialPants;
    }

    private void Update()
    {
        Shirt.material = PatientData.PatientShirtMaterial;
        Pants.material = PatientData.PatientPantsMaterial;
    }


    public void AddUserToCurrentlyTreating(GameObject user)
    {
        for (int i = 0; i < CurrentlyTreatingCrew.Length; i++)
        {
            if (CurrentlyTreatingCrew[i].ToString() == user.name)
            {
                continue;
            }
            else
            {
                
            }
        }
        
    }

    // wip -v-
    //private void SetOperatingCrew(Dictionary<string, int> operatingUserCrew)
    //{
    //    if (!_currentPatientScript.OperatingUserCrew.ContainsKey(PlayerData.UserName))
    //    {
    //        _currentPatientScript.OperatingUserCrew.Add(PlayerData.UserName, PlayerData.CrewIndex);
    //        _currentPatientScript.DisplayDictionary();
    //    }
    //}
    // -------

    //public void DisplayDictionary()
    //{
    //    CurrentlyTreatingUser.Clear();
    //    CurrentlyTreatingCrew.Clear();
    //
    //    foreach (KeyValuePair<string, int> diction in OperatingUserCrew)
    //    {
    //        Debug.Log("Key = {" + diction.Key + "} " + "Value = {" + diction.Value + "}");
    //        CurrentlyTreatingUser.Add(diction.Key);
    //        CurrentlyTreatingCrew.Add(diction.Value);
    //    }
    //}
}
