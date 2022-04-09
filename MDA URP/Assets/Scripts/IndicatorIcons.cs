using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IndicatorIcons : MonoBehaviour
{
    //[SerializeField]
    //private LayerMask _interactableMask;

    [SerializeField]
    private Transform _playerTr;

    [SerializeField]
    private Image _indicatorIcon;

    [SerializeField]
    private float _distance = 8f;

    [SerializeField]
    private bool _isHovering;

    private float _exitCounter = 0;

    private void Start()
    {
        _indicatorIcon.enabled = false;
    }


    private void Update()
    {
        if (_exitCounter > Mathf.Epsilon)
            _isHovering = true;

        else if (_exitCounter < Mathf.Epsilon)
            _isHovering = false;

        if (_isHovering)
            _indicatorIcon.enabled = true;
        else
            _indicatorIcon.enabled = false;
    }

    private void OnMouseEnter()
    {
        float distance = Vector3.Distance(_playerTr.position, transform.position);

        if (distance < _distance)
            _exitCounter += Time.deltaTime; 
    }

    private void OnMouseExit()
    {
        _exitCounter = 0;
    }

    /*
     * RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f, InteractableMask))
            {
                //Our custom method. 
                Debug.Log("Object Hit: " + raycastHit.transform.gameObject.name);
                Vector3 pointToLook = ray.GetPoint(Vector3.Distance(ray.origin, raycastHit.transform.position));
                Debug.DrawLine(ray.origin, pointToLook, Color.cyan, 5f);
                raycastHit.transform.gameObject.GetComponent<MakeItAButton>().EventToCall.Invoke();
            }
    */
}
