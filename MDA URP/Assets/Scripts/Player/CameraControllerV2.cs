using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerV2 : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _playerCamera;

    [Header("Interaction")]
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private GameObject _indicatorIcon;
    [SerializeField] private AudioSource _indicatorSound;
    [SerializeField] private float _raycastDistance = 10f;

    private void Update()
    {
        CheckInteraction();
    }

    public RaycastHit CheckInteraction()
    {
        RaycastHit raycastHit;
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, _raycastDistance, _interactableLayer))
        {
            Debug.DrawLine(ray.origin, raycastHit.point, Color.cyan, _raycastDistance);
            // our custom method. 
            if (raycastHit.transform.gameObject.GetComponent<Collider>().enabled)
            {
                _indicatorIcon.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Object Hit: " + raycastHit.transform.gameObject.name);
                    Vector3 pointToLook = ray.GetPoint(Vector3.Distance(ray.origin, raycastHit.transform.position));
                    Debug.DrawLine(ray.origin, pointToLook, Color.cyan, 5f);
                    _indicatorSound.Play();
                    raycastHit.transform.gameObject.GetComponent<MakeItAButton>().EventToCall.Invoke();
                }
            }
        }
        else
        {
            _indicatorIcon.SetActive(false);
        }

        return raycastHit;
    }
}
