using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
//using System.Linq;

public class Patient : MonoBehaviour
{
    #region private serialized fields
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _operatingCheckPanel, _patientMenu,_patientInfoPanel;
   // [SerializeField] private PaitentBaseInfoSO paitentInfo;

    [SerializeField] private List<string> _operatingUsers = new List<string>();
    [SerializeField] private List<int> _operatingCrews = new List<int>();
    #endregion

    #region private fields
    [SerializeField]
    private Dictionary<string, int> _operatingUserCrew = new Dictionary<string, int>();

    private bool _isOperated;
    #endregion

    #region Patient Menu Fields
    [SerializeField]
    private TMP_Text _sureName, _gender, _adress, _insuranceCompany, _incidentAdress, _complaint,_idNumber,_age,_phoneNumber;

    [SerializeField]
    private int _telNumber;
    #endregion // will be transmuted into scriptableObject

    // Start is called before the first frame update
    void Start()
    {
        _operatingCheckPanel.SetActive(false);
        //PatientInfo();
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
            _operatingCheckPanel.SetActive(true);
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
            _operatingCheckPanel.SetActive(false);
            _patientMenu.SetActive(true);
            _patientInfoPanel.SetActive(false);

        }
        else
        {
            _operatingCheckPanel.SetActive(false);
        }
    }

    // clost menu
    public void ClosePatientMenu()
    {
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
                if (_operatingUsers[i]  == _player.GetComponent<CrewMember>().UserName)
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
        print("Log Window");
    }

    // paitent background info: name, weghit, gender, adress...
    public void PatientInfo(PaitentBaseInfoSO paitentInfo)
    {
       
        _patientInfoPanel.SetActive(true);

        _sureName.text = paitentInfo.fullName;
        _gender.text = paitentInfo.gender;
        _adress.text = paitentInfo.addressLocation;
        _insuranceCompany.text = paitentInfo.medicalCompany;
        _incidentAdress.text = paitentInfo.eventPlace;
        _complaint.text = paitentInfo.complaint;

        _age.text = paitentInfo.age.ToString();
        _idNumber.text = paitentInfo.idNumber.ToString("0");
        _phoneNumber.text = paitentInfo.phoneNumber.ToString();



        print("Patient Information");
    }

    // possibly removed later on
    public void Anamnesis()
    {
        print("Anamnesis Drop Down Menu");
    }

    private void SetOperatingCrew()
    {
        if (!_operatingUserCrew.ContainsKey(_player.GetComponent<CrewMember>().UserName))
        {
            _operatingUserCrew.Add(_player.GetComponent<CrewMember>().UserName, _player.GetComponent<CrewMember>().CrewNumber);
            DisplayDictionary();
        }


        _isOperated = true;
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
}
