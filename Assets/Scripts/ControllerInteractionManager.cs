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
    private bool _laserActiveR;
    //Left hand laser variables
    [SerializeField] private LineRenderer _lineRendererL; //The line renderer of the left hand laser
    [SerializeField] private Transform _laserSpawnL; //The starting point of the left hand laser
    [SerializeField] private Color _colorL; //The color of the left hand laser
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

            //If the laser hits an interactable object
            if (hit.collider.GetComponent<InteractableObjectBehavior>()) 
            {
                //Begin highlighting that object with the hands color
                InteractableObjectBehavior IOB = hit.collider.GetComponent<InteractableObjectBehavior>();
                IOB.Hovered(_colorR);
            }
        }
    }

    //Stops the right handed laser
    void StopLaserR() 
    {
        _laserActiveR = false;
        _lineRendererR.positionCount = 0;
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

            //If the laser hits an interactable object
            if (hit.collider.GetComponent<InteractableObjectBehavior>())
            {
                //Begin highlighting that object with the hands color
                InteractableObjectBehavior IOB = hit.collider.GetComponent<InteractableObjectBehavior>();
                IOB.Hovered(_colorL);
            }
        }
    }

    //Stops the left handed laser
    void StopLaserL()
    {
        _laserActiveL = false;
        _lineRendererL.positionCount = 0;
    }
}
