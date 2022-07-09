using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private bool _objectGrabbedLeft;
    private bool _objectGrabbedRight;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Text _canvasText;
    [SerializeField] private Image _selectIcon;
    private bool _UIActiveLeft;
    private bool _UIActiveRight;
    [SerializeField] private Transform _mainCameraTransform;
    private float _selectedAmount;

    void Start()
    {
        _leftLR.startColor = _leftColor;
        _leftLR.endColor = _leftColor;
        _rightLR.startColor = _rightColor;
        _rightLR.endColor = _rightColor;
    }

    void Update()
    {
        _canvas.SetActive(false);
        _canvasText.enabled = false;
        _selectIcon.enabled = false;
        if (_handController.LeftHandLaser && !_objectGrabbedLeft)
        {
            LeftLaser();
        }
        else 
        {
            StopLeftLaser();
        }

        if (_handController.RightHandLaser && !_objectGrabbedRight)
        {
            RightLaser();
        }
        else
        {
            StopRightLaser();
        }

        if (_leftPicked != null) 
        {
            if (Vector3.Distance(_leftPicked.position, _leftHand.position) > 0.05f)
            {
                //_leftPicked.GetComponent<Rigidbody>().useGravity = false;
                _leftPicked.position = Vector3.Lerp(_leftPicked.position, _leftHand.position, 0.15f);
                _leftPicked.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                if (_objectGrabbedLeft)
                {
                    //_leftPicked.GetComponent<Rigidbody>().useGravity = true;
                    _leftPicked = null;
                }
            }
            else 
            {
                //_leftPicked.GetComponent<Rigidbody>().useGravity = true;
                _leftPicked = null;
            }
            
        }

        if (_rightPicked != null)
        {
            if (Vector3.Distance(_rightPicked.position, _rightHand.position) > 0.05f)
            {
                //_rightPicked.GetComponent<Rigidbody>().useGravity = false;
                _rightPicked.position = Vector3.Lerp(_rightPicked.position, _rightHand.position, 0.15f);
                _rightPicked.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                if (_objectGrabbedRight)
                {
                    //_rightPicked.GetComponent<Rigidbody>().useGravity = true;
                    _rightPicked = null;
                }
            }
            else
            {
                //_rightPicked.GetComponent<Rigidbody>().useGravity = true;
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

                if (hit.collider.tag == "bed" && !_UIActiveRight)
                {
                    if(!_UIActiveLeft)
                        _selectedAmount = 0;
                    _UIActiveLeft = true;
                    _canvas.SetActive(true);
                    _canvasText.enabled = true;
                    _canvas.transform.position = hit.point;
                    _canvas.transform.LookAt(_mainCameraTransform);
                    _canvasText.text = "Press [B] to sleep";
                    if (_leftInteract.action.ReadValue<float>() == 1)
                    {
                        _canvasText.enabled = false;
                        
                        _selectIcon.enabled = true;
                        _selectIcon.fillAmount = _selectedAmount;
                    }
                }
                else 
                {
                    _UIActiveLeft = false;
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

                if (hit.collider.tag == "bed" && !_UIActiveLeft)
                {
                    if (!_UIActiveRight)
                        _selectedAmount = 0;
                    _UIActiveRight = true;
                    _canvas.SetActive(true);
                    _canvas.transform.position = hit.point;
                    _canvas.transform.LookAt(_mainCameraTransform);
                    _canvasText.enabled = true;
                    _canvasText.text = "Press [B] to sleep";
                    if (_rightInteract.action.ReadValue<float>() == 1)
                    {
                        _canvasText.enabled = false;
                        RoutineBehaviour.Instance.StartNewTimedAction(args => { _selectedAmount += 0.1f; }, TimedActionCountType.SCALEDTIME, 0.5f);
                        _selectIcon.enabled = true;
                        _selectIcon.fillAmount = _selectedAmount;
                    }
                }
                else
                {
                    _UIActiveRight = false;
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
        _UIActiveLeft = false;
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
        _UIActiveRight = false;
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

    public void GrabLeft() 
    {
        _objectGrabbedLeft = true;
    }

    public void LeaveLeft() 
    {
        _objectGrabbedLeft = false;
    }

    public void GrabRight()
    {
        _objectGrabbedRight = true;
    }

    public void LeaveRight()
    {
        _objectGrabbedRight = false;
    }

    void UseLaser() 
    {
        while (_selectedAmount != 1) 
        {
            RoutineBehaviour.Instance.StartNewTimedAction(args => { _selectedAmount += 0.1f; }, TimedActionCountType.SCALEDTIME, 0.5f);
        }
        if (_selectedAmount == 1) 
        {
            _selectedAmount = 0;
        }
    }
}
