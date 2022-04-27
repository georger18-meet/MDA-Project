using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class Patient : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _operatingCheckPanel;
    private Dictionary<string, int> _operatingUserCrew = new Dictionary<string, int>();
    private List<int> _operatingCrews = new List<int>();
    private List<string> _operatingUsers = new List<string>();
    private bool _isOperated;

    // Start is called before the first frame update
    void Start()
    {
        _operatingCheckPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOperatingCrewCheck()
    {
        if (!_operatingUserCrew.ContainsKey(_player.GetComponent<CrewMember>().UserName))
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

    public void ConfirmOperation(bool confirm)
    {
        if (confirm)
        {
            SetOperatingCrew();
        }
        _operatingCheckPanel.SetActive(false);
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
