using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserBehavior : MonoBehaviour
{
    [SerializeField] private HandController _handController;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private LineRenderer _leftLR;
    [SerializeField] private LineRenderer _rightLR;
    private GameObject _rightObj;
    private GameObject _leftObj;
    [SerializeField] private Color _leftColor;
    [SerializeField] private Color _rightColor;

    [SerializeField] private InputActionReference _leftInteract;
    [SerializeField] private InputActionReference _rightInteract;
    private Transform _rightPicked;
    private Transform _leftPicked;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    void Start()
    {
        _leftLR.startColor = _leftColor;
        _leftLR.endColor = _leftColor;
        _rightLR.startColor = _rightColor;
        _rightLR.endColor = _rightColor;
    }

    void Update()
    {
        if (_handController.LeftHandLaser)
        {
            LeftLaser();
        }
        else 
        {
            StopLeftLaser();
        }

        if (_handController.RightHandLaser)
        {
            RightLaser();
        }
        else
        {
            StopRightLaser();
        }

        if (_leftPicked != null) 
        {
            if (Vector3.Distance(_leftPicked.position, _leftHand.position) > 0.185f)
            {
                _leftPicked.GetComponent<Rigidbody>().useGravity = false;
                _leftPicked.position = Vector3.Lerp(_leftPicked.position, _leftHand.position, 0.05f);
            }
            else 
            {
                _leftPicked.GetComponent<Rigidbody>().useGravity = true;
                _leftPicked = null;
            }
        }

        if (_rightPicked != null)
        {
            if (Vector3.Distance(_rightPicked.position, _rightHand.position) > 0.185f)
            {
                _rightPicked.GetComponent<Rigidbody>().useGravity = false;
                _rightPicked.position = Vector3.Lerp(_rightPicked.position, _rightHand.position, 0.05f);
            }
            else
            {
                _rightPicked.GetComponent<Rigidbody>().useGravity = true;
                _rightPicked = null;
            }
        }
    }

    void LeftLaser() 
    {
        RaycastHit hit;
        if (Physics.Raycast(_leftPoint.position, _leftPoint.forward, out hit, Mathf.Infinity))
        {
            _leftLR.positionCount = 2;
            _leftLR.SetPosition(0, _leftPoint.position);
            _leftLR.SetPosition(1, hit.point);

            if (hit.collider.tag == "interactable")
            {
                _leftObj = hit.collider.gameObject;
                _leftObj.GetComponent<Outline>().enabled = true;
                _leftObj.GetComponent<Outline>().OutlineColor = _leftColor;
                if (_leftInteract.action.ReadValue<float>() == 1) 
                {
                    _leftPicked = _leftObj.transform;
                }
            }
            else 
            {
                if (_leftObj != null)
                {
                    _leftObj.GetComponent<Outline>().enabled = false;
                    _leftObj = null;
                }
            }
        }
        else 
        {
            StopLeftLaser();
        }
    }

    void RightLaser()
    {
        RaycastHit hit;
        if (Physics.Raycast(_rightPoint.position, _rightPoint.forward, out hit, Mathf.Infinity))
        {
            _rightLR.positionCount = 2;
            _rightLR.SetPosition(0, _rightPoint.position);
            _rightLR.SetPosition(1, hit.point);

            if (hit.collider.tag == "interactable")
            {
                _rightObj = hit.collider.gameObject;
                _rightObj.GetComponent<Outline>().enabled = true;
                _rightObj.GetComponent<Outline>().OutlineColor = _rightColor;
                if (_rightInteract.action.ReadValue<float>() == 1)
                {
                    _rightPicked = _rightObj.transform;
                }
            }
            else 
            {
                if (_rightObj != null)
                {
                    _rightObj.GetComponent<Outline>().enabled = false;
                    _rightObj = null;
                }
            }
        }
        else
        {
            StopRightLaser();
        }
    }

    void StopRightLaser() 
    {
        _rightLR.positionCount = 0;
        if (_rightObj != null)
        {
            if (_rightObj.GetComponent<Outline>())
            {
                _rightObj.GetComponent<Outline>().enabled = false;
            }
            _rightObj = null;
        }
    }

    void StopLeftLaser()
    {
        _leftLR.positionCount = 0;
        if (_leftObj != null)
        {
            if (_leftObj.GetComponent<Outline>())
            {
                _leftObj.GetComponent<Outline>().enabled = false;
            }
            _leftObj = null;
        }
    }
}
