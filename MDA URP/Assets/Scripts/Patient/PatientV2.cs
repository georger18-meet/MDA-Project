using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientV2 : MonoBehaviour
{
    #region Script References
    [Header("Data & Scripts")]
    private ActionsManager _actionsManager;
    public PatientData PatientData;
    public List<ActionSequence> ActionSequences;
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
    public List<PlayerData> NearbyUsers;
    public List<PlayerData> TreatingUsers;
    public List<int> TreatingCrews;
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


    public void AddUserToTreatingLists(object currentPlayer)
    {
        GameObject currentPlayerGO = currentPlayer != null ? currentPlayer as GameObject : null;

        if (currentPlayerGO == null)
        {
            return;
        }

        PlayerData currentPlayerData = currentPlayerGO.GetComponent<PlayerData>();

        for (int i = 0; i < TreatingUsers.Count; i++)
        {
            if (TreatingUsers.Contains(currentPlayerData))
            {
                continue;
            }
            else
            {
                TreatingUsers.Add(currentPlayerData);
            }

            if (TreatingCrews.Contains(currentPlayerData.CrewIndex))
            {
                return;
            }
            else
            {
                TreatingCrews.Add(currentPlayerData.CrewIndex);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        else
        {
            PlayerData lastEnteredPlayer = other.gameObject.GetComponent<PlayerData>();
            NearbyUsers.Add(lastEnteredPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerData lastEnteredPlayer = other.gameObject.GetComponent<PlayerData>();

            if (!NearbyUsers.Contains(lastEnteredPlayer))
            {
                return;
            }
            else
            {
                NearbyUsers.Remove(lastEnteredPlayer);
            }
        }
    }

    //private void SetOperatingCrew(Dictionary<string, int> operatingUserCrew)
    //{
    //    if (!OperatingUserCrew.ContainsKey(PlayerData.UserName))
    //    {
    //        OperatingUserCrew.Add(PlayerData.UserName, PlayerData.CrewIndex);
    //        DisplayDictionary();
    //    }
    //}

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
