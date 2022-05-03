using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ActionsOperatingManager : MonoBehaviour
{
    #region Script References
    [Header("Data & Scripts")]
    [SerializeField] private PaitentBaseInfoSO _patientInfoSO;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Patient _currentPatientScript;
    #endregion

    #region Prefab References
    [Header("Prefabs")]
    [SerializeField] private GameObject _player;
    #endregion

    #region Player UI
    [Header("Player UI Parents")]
    //[SerializeField] private GameObject
    //[SerializeField] private GameObject

    [Header("Player UI Texts")]
    //[SerializeField] private TextMeshProUGUI
    //[SerializeField] private TextMeshProUGUI
    #endregion

    #region Patient UI 
    // will be transmuted into scriptableObject
    [Header("Patient UI Parents")]
    [SerializeField] private GameObject _joinPatientPopUp;
    [SerializeField] private GameObject _patientMenu, _patientInfoPanel, _actionLog;

    [Header("Patient UI Texts")]
    [SerializeField] private TextMeshProUGUI _sureName;
    [SerializeField] private TextMeshProUGUI _lastName, _id, _age, _gender, _phoneNumber, _insuranceCompany, _adress, _complaint; /*_incidentAdress*/
    #endregion

    public GameObject AmbulanceActionPanel /*, NatanActionPanel*/, NoBagActionMenu;

    private ActionsOperatingHandler _actionsOperatingHandler;

    // may be removed or changed to decouple
    [field: SerializeField]
    public string UserName, CrewName;

    [field: SerializeField]
    public int UserIndexInCrew, CrewIndex;

    #region MonoBehaviour Callbacks
    private void Start()
    {
        AmbulanceActionPanel.SetActive(false);
        _actionsOperatingHandler = new ActionsOperatingHandler();

        // from patient scripts
        _joinPatientPopUp.SetActive(false);
        _actionLog.SetActive(false);
        _patientInfoPanel.SetActive(false);
    }

    private void Update()
    {
        SetupPatientMenu();
    }
    #endregion

    #region Assignment
    // Triggered upon Clicking on the Patient
    public void SetOperatingCrewCheck()
    {
        if (_player == null)
        {
            return;
        }
        else if (!_currentPatientScript.OperatingUserCrew.ContainsKey(_playerData.UserName))
        {
            _joinPatientPopUp.SetActive(true);
        }
        else if (_currentPatientScript.OperatingUserCrew.ContainsKey(_playerData.UserName))
        {
            _patientMenu.SetActive(true);
        }
    }

    // triggered upon pressing "yes" in patient Join pop-up
    public void ConfirmOperation(bool confirm)
    {
        if (confirm)
        {
            // need to verify that set operating crew is setting an empty group of maximum 4 and insitialize it with current player
            SetOperatingCrew(_currentPatientScript.OperatingUserCrew);
            _joinPatientPopUp.SetActive(false);
            _patientMenu.SetActive(true);
            _patientInfoPanel.SetActive(false);

        }
        else
        {
            _joinPatientPopUp.SetActive(false);
        }
    }

    private void SetOperatingCrew(Dictionary<string, int> operatingUserCrew)
    {
        if (!_currentPatientScript.OperatingUserCrew.ContainsKey(_playerData.UserName))
        {
            _currentPatientScript.OperatingUserCrew.Add(_playerData.UserName, _playerData.CrewIndex);
            _currentPatientScript.DisplayDictionary();
        }
    }

    private void SetupPatientMenu()
    {
        _sureName.text = _patientInfoSO.SureName;
        _sureName.text = _patientInfoSO.SureName;
        _gender.text = _patientInfoSO.Gender;
        _adress.text = _patientInfoSO.AddressLocation;
        _insuranceCompany.text = _patientInfoSO.MedicalCompany;
        _complaint.text = _patientInfoSO.Complaint;
        //_incidentAdress.text = PatientInfoSO.eventPlace;

        _age.text = _patientInfoSO.Age.ToString();
        _id.text = _patientInfoSO.Id.ToString();
        _phoneNumber.text = _patientInfoSO.PhoneNumber.ToString();
    }

    // For Use Externally
    public bool CheckIfPlayerJoined()
    {
        bool playerIsJoined = false;
        if (_player != null)
        {
            if (_currentPatientScript.OperatingUserCrew.ContainsKey(_playerData.UserName))
            {
                playerIsJoined = true;
            }
        }
        return playerIsJoined;
    }
    #endregion

    #region Patient Menu Events
    // clost menu
    public void ClosePatientMenu()
    {
        _actionLog.SetActive(false);
        _patientInfoPanel.SetActive(false);
        _patientMenu.SetActive(false);

        print("Close Patient Menu");
    }


    // paitent background info: name, weghit, gender, adress...
    public void PatientInfo()
    {
        if (!_patientInfoPanel.activeInHierarchy)
            _patientInfoPanel.SetActive(true);
        else
            _patientInfoPanel.SetActive(false);

        print("Patient Information");
    }

    // list of actions done on the patient by players, aranged by time stamp
    public void Log()
    {
        _actionLog.SetActive(true);
        print("Log Window");
    }

    // open up a form that follows this reference: https://drive.google.com/file/d/1EScLHzpHT_YOk02lS_jzjErDfSGRWj2x/view?usp=sharing
    public void TagMiun()
    {
        print("Tag Miun");
    }

    // take current player out of their crew's list
    public void LeavePatient()
    {
        
        if (_currentPatientScript.OperatingUserCrew.ContainsKey(_playerData.UserName))
        {
            for (int i = 0; i < _currentPatientScript.OperatingUsers.Count; i++)
            {
                if (_currentPatientScript.OperatingUsers[i] == _playerData.UserName)
                {
                    _currentPatientScript.OperatingUsers.RemoveAt(i);
                    _currentPatientScript.OperatingCrews.RemoveAt(i);
                }
            }

            _currentPatientScript.OperatingUserCrew.Remove(_playerData.UserName);
        }

        print("Leave Patient");

        ClosePatientMenu();
    }
    #endregion

    #region Player Action Events
    public void OpenNoBagActionMenu()
    {
        _actionsOperatingHandler.OpenNoBagActionMenu(NoBagActionMenu);
    }

    public void CallAction(int actionNumInList)
    {
        if (_patientInfoSO != null)
        {
            _actionsOperatingHandler.RunAction(actionNumInList, _currentPatientScript);
        }
    }
    #endregion

    // refactor for player to do on patient
    #region getting patient data
    private void GetPatientInfo()
    {
        if (CheckIfPlayerJoined())
        {
            AmbulanceActionPanel.SetActive(true);
            _patientInfoSO = _currentPatientScript.PatientInfoSO;
        }
        else
        {
            AmbulanceActionPanel.SetActive(false);
            _patientInfoSO = null;
        }
    }

    //patient
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Patient"))
        {
            _currentPatientScript = other.gameObject.GetComponent<Patient>();
            GetPatientInfo();
        }
    }

    //patient
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Patient"))
        {
            AmbulanceActionPanel.SetActive(false);
            _currentPatientScript = null;
            _patientInfoSO = null;
        }
    }
    
    #endregion
}
