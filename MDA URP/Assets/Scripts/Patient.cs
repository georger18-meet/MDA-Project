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
    [SerializeField] private GameObject _operatingCheckPanel, _patientMenu;

    [SerializeField] private List<int> _operatingCrews = new List<int>();
    [SerializeField] private List<string> _operatingUsers = new List<string>();
    #endregion

    #region private fields
    [SerializeField]
    private Dictionary<string, int> _operatingUserCrew = new Dictionary<string, int>();

    private bool _isOperated;
    #endregion

    #region Patient Menu Fields
    [SerializeField]
    private string _sureName, _lastName, _adress, _insuranceCompany, _incidentAdress, _complaint;

    [SerializeField]
    private int _telNumber;
    #endregion // will be transmuted into scriptableObject

    // Start is called before the first frame update
    void Start()
    {
        _operatingCheckPanel.SetActive(false);
    }

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

        //if (_operatingUsers != null)
        //{
        //    bool userMatch = false;
        //    foreach (var operatingUser in _operatingUsers)
        //    {
        //        if (operatingUser == _player.GetComponent<CrewMember>().UserName)
        //        {
        //            userMatch = true;
        //        }
        //    }
        //    if (!userMatch)
        //    {
        //        _operatingCheckPanel.SetActive(true);
        //    }
        //}
        //else
        //{
        //    _operatingCheckPanel.SetActive(true);
        //}
    }


    // triggered upon pressing "yes" in patient Join pop-up
    public void ConfirmOperation(bool confirm)
    {
        if (confirm)
        {
            // need to verify that set operating crew is setting an empty group of maximum 4 and insitialize it with current player
            SetOperatingCrew();
        }

        _operatingCheckPanel.SetActive(false);
        _patientMenu.SetActive(true);
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
            _operatingUserCrew.Remove(_player.GetComponent<CrewMember>().UserName);
        }

        print("Leave Patient");
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
    public void PatientInfo()
    {
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
        }


        //if (_operatingCrews != null)
        //{
        //    bool crewMatch = false;
        //    foreach (var operatingCrew in _operatingCrews)
        //    {
        //        if (operatingCrew == _player.GetComponent<CrewMember>().CrewNumber)
        //        {
        //            crewMatch = true;
        //        }
        //    }
        //    if (!crewMatch)
        //    {
        //        _operatingCrews.Add(_player.GetComponent<CrewMember>().CrewNumber);
        //    }
        //}
        //else
        //{
        //    _operatingCrews.Add(_player.GetComponent<CrewMember>().CrewNumber);
        //}

        //if (_operatingUsers != null)
        //{

        //    bool userMatch = false;
        //    foreach (var operatingUser in _operatingUsers)
        //    {
        //        if (operatingUser == _player.GetComponent<CrewMember>().UserName)
        //        {
        //            userMatch = true;
        //        }
        //    }
        //    if (!userMatch)
        //    {
        //        _operatingUsers.Add(_player.GetComponent<CrewMember>().UserName);
        //    }
        //}
        //else
        //{
        //    _operatingUsers.Add(_player.GetComponent<CrewMember>().UserName);
        //}

        _isOperated = true;
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
