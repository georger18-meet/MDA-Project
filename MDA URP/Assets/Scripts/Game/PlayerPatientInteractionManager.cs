using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerPatientInteractionManager : MonoBehaviour
{
    [SerializeField] private PatientData _patientData;
    [SerializeField] private Patient _patientScript;

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private CameraControllerV2 _playerCameraController;

    [SerializeField] private Canvas _patientMenuCanvas;
    [SerializeField] private Image _ActionBar;

    [SerializeField]
    private GameObject _playerGO, _patientGO;
    
    [SerializeField] private List<GameObject> _addedItems;
    [SerializeField] private List<Transform> _addedItemsFixedPositions;

    public void JoinPatient()
    {
        if (_playerCameraController.Interact().transform.tag == "Patient")
        {
            print("Managed to get raycstHit");
        }
    }
}
