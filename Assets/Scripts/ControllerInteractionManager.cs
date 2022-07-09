using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerInteractionManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference _inputGrabR; //The right hand input button to grab highlighted object
    [SerializeField] private InputActionReference _inputGrabL; //The left hand input button to grab highlighted object
    [Header("Laser Pointer")]
    //Right hand laser variables
    [SerializeField] private HandController _handController;
    [SerializeField] private LineRenderer _lineRendererR; //The line renderer of the right hand laser
    [SerializeField] private Transform _laserSpawnR; //The starting point of the right hand laser
    [SerializeField] private Color _colorR; //The color of the right hand laser
    private InteractableObjectBehavior _hoveredR;
    private bool _laserActiveR;
    //Left hand laser variables
    [SerializeField] private LineRenderer _lineRendererL; //The line renderer of the left hand laser
    [SerializeField] private Transform _laserSpawnL; //The starting point of the left hand laser
    [SerializeField] private Color _colorL; //The color of the left hand laser
    private InteractableObjectBehavior _hoveredL;
    private bool _laserActiveL;
    //[Header("LP Grab Object")]
    //[Header("LP Activate Object")]

    void Start()
    {
        //Set the colors of both line renderers to the designated color.
        _lineRendererR.startColor = _colorR;
        _lineRendererR.endColor = _colorR;

        _lineRendererL.startColor = _colorL;
        _lineRendererL.endColor = _colorL;
    }

    void Update()
    {
        //If the hand is performing the laser action, start the laser pointer for the hand. If not, stop the laser.
        if (_handController.RightHandLaser)
            LaserR();
        else if(_laserActiveR)
            StopLaserR();
        if (_handController.LeftHandLaser)
            LaserL();
        else if(_laserActiveL)
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
                }
            }

            //If the laser hits an interactable object
            if (hit.collider.GetComponent<InteractableObjectBehavior>()) 
            {
                //Begin highlighting that object with the hands color
                _hoveredR = hit.collider.GetComponent<InteractableObjectBehavior>();
                _hoveredR.StartHovered(_colorR);
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
                }
            }

            //If the laser hits an interactable object
            if (hit.collider.GetComponent<InteractableObjectBehavior>())
            {
                //Begin highlighting that object with the hands color
                _hoveredL = hit.collider.GetComponent<InteractableObjectBehavior>();
                _hoveredL.StartHovered(_colorL);
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
        }
    }
}
