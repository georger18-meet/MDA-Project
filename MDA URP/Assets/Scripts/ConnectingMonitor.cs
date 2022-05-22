using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingMonitor : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ActionsOperatingManager _AOM;
    [SerializeField] private ActionTemplates _actionTemplates;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _monitor;

    public void Defibrillation()
    {
        if (!_AOM.CheckIfPlayerJoined()/* || (int)actionData.RolesAD <= 1*/)
        {
            Debug.Log("You Are NOT WORTHY!");
            return;
        }
        else
        {
            _player.transform.position = _AOM.PlayerTreatingTr.position;
            MonoBehaviour.Instantiate(_monitor, _AOM.PatientEquipmentTr.position, Quaternion.identity);
        }

        Debug.Log("CLEAR!!! Defibrillator On " /*+ _AOM.Patient.name*/);
    }
}
