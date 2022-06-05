using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
//using System.Linq;

public class Patient : MonoBehaviour
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
    [SerializeField] public List<string> OperatingUsers = new List<string>();
    [SerializeField] public List<int> OperatingCrews = new List<int>();
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

    public void DisplayDictionary()
    {
        OperatingUsers.Clear();
        OperatingCrews.Clear();

        foreach (KeyValuePair<string, int> diction in OperatingUserCrew)
        {
            Debug.Log("Key = {" + diction.Key + "} " + "Value = {" + diction.Value + "}");
            OperatingUsers.Add(diction.Key);
            OperatingCrews.Add(diction.Value);
        }
    }

    private void InitializePlayerData()
    {

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (_player == null)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //            _player = other.gameObject;
    //        }
    //    }
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        _player = null;
    //    }
    //}

    // refactor for player to do on patient

    #region getting patient data
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (_player == null)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //            _player = other.gameObject;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        _player = null;
    //    }
    //}

    //private void GetPatientInfo()
    //{
    //    if (_currentPatientScript.CheckIfPlayerJoined())
    //    {
    //        AmbulanceActionPanel.SetActive(true);
    //        _patientInfoSO = _currentPatientScript.PatientInfoSO;
    //    }
    //    else
    //    {
    //        AmbulanceActionPanel.SetActive(false);
    //        _patientInfoSO = null;
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Patient"))
    //    {
    //        _currentPatientScript = other.gameObject.GetComponent<Patient>();
    //        GetPatientInfo();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Patient"))
    //    {
    //        AmbulanceActionPanel.SetActive(false);
    //        _currentPatientScript = null;
    //        _patientInfoSO = null;
    //    }
    //}
    #endregion

    //// For Use Externally
    //public bool CheckIfPlayerJoined()
    //{
    //    bool playerIsJoined = false;
    //    if (_player != null)
    //    {
    //        if (OperatingUserCrew.ContainsKey(_player.GetComponent<CrewMember>().UserName))
    //        {
    //            playerIsJoined = true;
    //        }
    //    }
    //    return playerIsJoined;
    //}
}
