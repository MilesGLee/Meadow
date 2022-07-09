using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerInteractionManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference _inputActivateR; //The right hand input button to grab highlighted object
    [SerializeField] private InputActionReference _inputActivateL; //The left hand input button to grab highlighted object

    [Header("Right Hand Laser Pointer")]
    [SerializeField] private HandStateBehavior _handStateBehavior;
    [SerializeField] private LineRenderer _lineRendererR; //The line renderer of the right hand laser
    [SerializeField] private Transform _laserSpawnR; //The starting point of the right hand laser
    [SerializeField] private Color _colorR; //The color of the right hand laser
    private InteractableObjectBehavior _hoveredR; //The object being hovered by the right hand
    private bool _laserActiveR;
    private bool _highlightCheckR;

    [Header("Left Hand Laser Pointer")]
    [SerializeField] private LineRenderer _lineRendererL; //The line renderer of the left hand laser
    [SerializeField] private Transform _laserSpawnL; //The starting point of the left hand laser
    [SerializeField] private Color _colorL; //The color of the left hand laser
    private InteractableObjectBehavior _hoveredL; //The object being hovered by the left hand
    private bool _laserActiveL;
    private bool _highlightCheckL;

    [Header("Grab Object")]
    [SerializeField] private Transform _grabPointR;
    [SerializeField] private Transform _grabPointL;
    private bool _grabbedR;
    private bool _grabbedL;

    [Header("Activate Object")]
    [SerializeField] private Transform _cameraTransform;
    //Right Hand Canvas
    [SerializeField] private Canvas _HUDCanvasR;
    [SerializeField] private Text _HUDTextR;
    [SerializeField] private Image _HUDImageR;
    private float _HUDImageAmountR;
    //Left Hand Canvas
    [SerializeField] private Canvas _HUDCanvasL;
    [SerializeField] private Text _HUDTextL;
    [SerializeField] private Image _HUDImageL;
    private float _HUDImageAmountL;


    void Start()
    {
        _HUDImageAmountR = 0.0f;
        _HUDImageAmountL = 0.0f;

        //Set the colors of both line renderers to the designated color.
        _lineRendererR.startColor = _colorR;
        _lineRendererR.endColor = _colorR;

        _lineRendererL.startColor = _colorL;
        _lineRendererL.endColor = _colorL;
    }

    void Update()
    {
        //Turn off the huds if the laser pointers are not hovering something that needs them displayed.
        _HUDCanvasR.enabled = false;
        _HUDCanvasL.enabled = false;
        _HUDTextR.enabled = false;
        _HUDTextL.enabled = false;
        _HUDImageR.enabled = false;
        _HUDImageL.enabled = false;

        //If the hand is performing the laser action, start the laser pointer for the hand. If not, stop the laser.
        if (_handStateBehavior.HandStateR == HandStateBehavior.HandState.POINT && !_grabbedR)
            LaserR();
        else if (_laserActiveR)
            StopLaserR();

        if (_handStateBehavior.HandStateL == HandStateBehavior.HandState.POINT && !_grabbedL)
            LaserL();
        else if (_laserActiveL)
            StopLaserL();
    }

    //The function that creates the right handed laser pointer
    void LaserR()
    {
        if (!_laserActiveR)
            _laserActiveR = true;

        //Create a raycast to detect hit positions for the laser
        RaycastHit hit;
        if (Physics.Raycast(_laserSpawnR.position, _laserSpawnR.forward, out hit, Mathf.Infinity))
        {
            _lineRendererR.positionCount = 2;
            _lineRendererR.SetPosition(0, _laserSpawnR.position);
            _lineRendererR.SetPosition(1, hit.point);

            //If an object that is hovered exists but is not what is currently hovered by the laser, turn off its outline
            if (_hoveredR)
            {
                if (hit.collider.GetComponent<InteractableObjectBehavior>() != _hoveredR)
                {
                    _hoveredR.StopHovered();
                    _hoveredR = null;
                    _highlightCheckR = false;
                }
            }

            //If the laser hits an interactable object
            if (hit.collider.GetComponent<InteractableObjectBehavior>() && hit.collider.tag != "bed")
            {
                //Begin highlighting that object with the hands color
                _hoveredR = hit.collider.GetComponent<InteractableObjectBehavior>();
                if (!_highlightCheckR)
                {
                    _hoveredR.StartHovered(_colorR);
                    _highlightCheckR = true;
                }

                //If the input to grab the object is pressed
                if (_inputActivateR.action.ReadValue<float>() == 1)
                {
                    _hoveredR.SetTarget(_grabPointR);
                }
            }
            //If the laser hits the bed
            if (hit.collider.GetComponent<InteractableObjectBehavior>() && hit.collider.tag == "bed") 
            {
                //Begin highlighting that object with the hands color
                _hoveredR = hit.collider.GetComponent<InteractableObjectBehavior>();
                if (!_highlightCheckR) 
                {
                    _hoveredR.StartHovered(_colorR);
                    _highlightCheckR = true;
                }
                //Turn on the canvas and change the text
                _HUDCanvasR.enabled = true;
                _HUDCanvasR.transform.position = hit.point;
                _HUDCanvasR.transform.LookAt(_cameraTransform.position);
                _HUDTextR.enabled = true;
                _HUDTextR.text = "Press [B] to sleep.";

                //if the input button is pressed start increasing the wheel fill amount
                if (_inputActivateR.action.ReadValue<float>() == 1)
                {
                    _HUDImageR.enabled = true;
                    _HUDTextR.enabled = false;
                    ActivateHoveredR();
                }
                if (_inputActivateR.action.ReadValue<float>() == 0)
                {
                    _HUDImageAmountR = 0;
                }
            }
        }
    }

    //Stops the right handed laser
    void StopLaserR()
    {
        _laserActiveR = false;
        _lineRendererR.positionCount = 0;
        if (_hoveredR)
        {
            _hoveredR.StopHovered();
            _hoveredR = null;
            _highlightCheckR = false;
            _HUDImageAmountR = 0;
        }
    }

    //The function that creates the left handed laser pointer
    void LaserL()
    {
        if (!_laserActiveL)
            _laserActiveL = true;

        //Create a raycast to detect hit positions for the laser
        RaycastHit hit;
        if (Physics.Raycast(_laserSpawnL.position, _laserSpawnL.forward, out hit, Mathf.Infinity))
        {
            _lineRendererL.positionCount = 2;
            _lineRendererL.SetPosition(0, _laserSpawnL.position);
            _lineRendererL.SetPosition(1, hit.point);

            //If an object that is hovered exists but is not what is currently hovered by the laser, turn off its outline
            if (_hoveredL)
            {
                if (hit.collider.GetComponent<InteractableObjectBehavior>() != _hoveredL)
                {
                    _hoveredL.StopHovered();
                    _hoveredL = null;
                    _highlightCheckL = false;
                }
            }

            //If the laser hits an interactable object
            if (hit.collider.GetComponent<InteractableObjectBehavior>() && hit.collider.tag != "bed")
            {
                //Begin highlighting that object with the hands color
                _hoveredL = hit.collider.GetComponent<InteractableObjectBehavior>();
                if (!_highlightCheckL)
                {
                    _hoveredL.StartHovered(_colorL);
                    _highlightCheckL = true;
                }

                //If the input to grab the object is pressed
                if (_inputActivateL.action.ReadValue<float>() == 1)
                {
                    _hoveredL.SetTarget(_grabPointL);
                }
            }

            //If the laser hits the bed
            if (hit.collider.GetComponent<InteractableObjectBehavior>() && hit.collider.tag == "bed")
            {
                //Begin highlighting that object with the hands color
                _hoveredL = hit.collider.GetComponent<InteractableObjectBehavior>();
                if (!_highlightCheckL)
                {
                    _hoveredL.StartHovered(_colorL);
                    _highlightCheckL = true;
                }
                //Turn on the canvas and change the text
                _HUDCanvasL.enabled = true;
                _HUDCanvasL.transform.position = hit.point;
                _HUDCanvasL.transform.LookAt(_cameraTransform.position);
                _HUDTextL.enabled = true;
                _HUDTextL.text = "Press [B] to sleep.";

                //if the input button is pressed start increasing the wheel fill amount
                if (_inputActivateL.action.ReadValue<float>() == 1)
                {
                    _HUDImageL.enabled = true;
                    _HUDTextL.enabled = false;
                    ActivateHoveredL();
                }
                if (_inputActivateL.action.ReadValue<float>() == 0)
                {
                    _HUDImageAmountL = 0;
                }
            }
        }
    }

    //Stops the left handed laser
    void StopLaserL()
    {
        _laserActiveL = false;
        _lineRendererL.positionCount = 0;
        if (_hoveredL)
        {
            _hoveredL.StopHovered();
            _hoveredL = null;
            _highlightCheckL = false;
            _HUDImageAmountL = 0;
        }
    }

    //Called on the XR Controller for when an object is held in hand or dropped from hand
    public void GrabRight()
    {
        _grabbedR = true;
    }
    public void DropRight() 
    {
        _grabbedR = false;
    }
    public void GrabLeft()
    {
        _grabbedL = true;
    }
    public void DropLeft()
    {
        _grabbedL = false;
    }

    //Fuction used to increase the fill amount of the image used to activate hovered objects.
    void ActivateHoveredR() 
    {
        RoutineBehaviour.Instance.StartNewTimedAction(args => { _HUDImageAmountR += 0.01f; }, TimedActionCountType.SCALEDTIME, 0.1f);
        _HUDImageR.fillAmount = _HUDImageAmountR;

        if(_HUDImageAmountR >= 1) 
        {
            _hoveredR.Activate();
            _HUDImageAmountR = 0;
        }
    }

    void ActivateHoveredL()
    {
        RoutineBehaviour.Instance.StartNewTimedAction(args => { _HUDImageAmountL += 0.01f; }, TimedActionCountType.SCALEDTIME, 0.1f);
        _HUDImageL.fillAmount = _HUDImageAmountL;

        if (_HUDImageAmountL >= 1)
        {
            _hoveredL.Activate();
            _HUDImageAmountL = 0;
        }
    }
}
