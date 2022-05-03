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
    public PaitentBaseInfoSO PatientInfoSO;
    #endregion

    #region Prefab References
    [Header("Prefabs")]
    [SerializeField] private GameObject _player;
    #endregion

    #region Patient UI // will be transmuted into scriptableObject
    [Header("Patient UI Parents")]

    [SerializeField] private GameObject _joinPatientPopUp;
    [SerializeField] private GameObject _patientMenu, _patientInfoPanel, _actionLog;

    [Header("Patient UI Texts")]
    [SerializeField] private TextMeshProUGUI _sureName;
    [SerializeField] private TextMeshProUGUI _lastName, _id, _age, _gender, _phoneNumber, _insuranceCompany, _adress, _complaint; /*_incidentAdress*/

    #endregion

    #region private fields
    [SerializeField]
    private Dictionary<string, int> _operatingUserCrew = new Dictionary<string, int>();
    #endregion

    #region private serialized fields
    [Header("Joined Crews & Players Lists")]
    [SerializeField] private List<string> _operatingUsers = new List<string>();
    [SerializeField] private List<int> _operatingCrews = new List<int>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _joinPatientPopUp.SetActive(false);
        _actionLog.SetActive(false);
        _patientInfoPanel.SetActive(false);
        SetupPatientInfo();
    }

    // Triggered upon Clicking on the Patient
    public void SetOperatingCrewCheck()
    {
        if (_player == null)
        {
            return;
        }
        else if (!_operatingUserCrew.ContainsKey(_player.GetComponent<CrewMember>().UserName))
        {
            _joinPatientPopUp.SetActive(true);
        }
        else if (_operatingUserCrew.ContainsKey(_player.GetComponent<CrewMember>().UserName))
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
            SetOperatingCrew();
            _joinPatientPopUp.SetActive(false);
            _patientMenu.SetActive(true);
            _patientInfoPanel.SetActive(false);

        }
        else
        {
            _joinPatientPopUp.SetActive(false);
        }
    }

    // clost menu
    public void ClosePatientMenu()
    {
        _actionLog.SetActive(false);
        _patientInfoPanel.SetActive(false);
        _patientMenu.SetActive(false);

        print("Close Patient Menu");
    }

    // take current player out of their crew's list
    public void LeavePatient()
    {
        if (_operatingUserCrew.ContainsKey(_player.GetComponent<CrewMember>().UserName))
        {
            for (int i = 0; i < _operatingUsers.Count; i++)
            {
                if (_operatingUsers[i] == _player.GetComponent<CrewMember>().UserName)
                {
                    _operatingUsers.RemoveAt(i);
                    _operatingCrews.RemoveAt(i);
                }
            }

            _operatingUserCrew.Remove(_player.GetComponent<CrewMember>().UserName);
        }

        print("Leave Patient");

        ClosePatientMenu();
    }

    // open up a form that follows this reference: https://drive.google.com/file/d/1EScLHzpHT_YOk02lS_jzjErDfSGRWj2x/view?usp=sharing
    public void TagMiun()
    {
        print("Tag Miun");
    }

    // list of actions done on the patient by players, aranged by time stamp
    public void Log()
    {
        _actionLog.SetActive(true);
        print("Log Window");
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

    private void SetupPatientInfo()
    {
        _sureName.text = PatientInfoSO.SureName;
        _sureName.text = PatientInfoSO.SureName;
        _gender.text = PatientInfoSO.Gender;
        _adress.text = PatientInfoSO.AddressLocation;
        _insuranceCompany.text = PatientInfoSO.MedicalCompany;
        _complaint.text = PatientInfoSO.Complaint;
        //_incidentAdress.text = PatientInfoSO.eventPlace;

        _age.text = PatientInfoSO.Age.ToString();
        _id.text = PatientInfoSO.Id.ToString();
        _phoneNumber.text = PatientInfoSO.PhoneNumber.ToString();
    }

    private void SetOperatingCrew()
    {
        if (!_operatingUserCrew.ContainsKey(_player.GetComponent<CrewMember>().UserName))
        {
            _operatingUserCrew.Add(_player.GetComponent<CrewMember>().UserName, _player.GetComponent<CrewMember>().CrewNumber);
            DisplayDictionary();
        }
    }

    private void DisplayDictionary()
    {
        _operatingUsers.Clear();
        _operatingCrews.Clear();

        foreach (KeyValuePair<string, int> diction in _operatingUserCrew)
        {
            Debug.Log("Key = {" + diction.Key + "} " + "Value = {" + diction.Value + "}");
            _operatingUsers.Add(diction.Key);
            _operatingCrews.Add(diction.Value);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_player == null)
        {
            if (other.CompareTag("Player"))
            {
                _player = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
        }
    }


    // For Use Externally
    public bool CheckIfPlayerJoined()
    {
        bool playerIsJoined = false;
        if (_player != null)
        {
            if (_operatingUserCrew.ContainsKey(_player.GetComponent<CrewMember>().UserName))
            {
                playerIsJoined = true;
            }
        }
        return playerIsJoined;
    }
}
