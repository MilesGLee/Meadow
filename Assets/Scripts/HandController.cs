using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandController : MonoBehaviour
{
    [SerializeField] private Animator _leftHand;
    [SerializeField] private InputActionReference _leftHandGrip = null;
    [SerializeField] private InputActionReference _leftHandIndex = null;
    private bool _leftHandGripState = false;
    private bool _leftHandIndexState = false;
    [SerializeField] private Animator _rightHand;
    [SerializeField] private InputActionReference _rightHandGrip = null;
    [SerializeField] private InputActionReference _rightHandIndex = null;
    private bool _rightHandGripState = false;
    private bool _rightHandIndexState = false;

    public bool LeftHandLaser;
    public bool RightHandLaser;
    public bool LeftHandClosed;
    public bool RightHandClosed;

    public Transform RightHeldObject;
    public Transform LeftHeldObject;

    public Transform LeftHand;
    public Transform RightHand;

    private void Update()
    {
        ToggleState();
    }

    private void ToggleState() 
    {
        if (_leftHand.GetBool("handState") && !_leftHand.GetBool("indexState") && LeftHeldObject == null)
            LeftHandLaser = true;
        else
            LeftHandLaser = false;

        if (_rightHand.GetBool("handState") && !_rightHand.GetBool("indexState") && RightHeldObject == null)
            RightHandLaser = true;
        else
            RightHandLaser = false;

        if (_leftHand.GetBool("handState") && !_leftHand.GetBool("indexState"))
            LeftHandClosed = true;
        else
            LeftHandClosed = false;

        if (_rightHand.GetBool("handState") && !_rightHand.GetBool("indexState"))
            RightHandClosed = true;
        else
            RightHandClosed = false;

        //Left Hand
        if (_leftHandGrip.action.ReadValue<float>() == 0 && _leftHandGripState)
        {
            _leftHandGripState = false;
            _leftHand.SetBool("handState", false);
        }
        if (_leftHandGrip.action.ReadValue<float>() == 1 && !_leftHandGripState)
        {
            _leftHandGripState = true;
            _leftHand.SetBool("handState", true);
        }
        if (_leftHandIndex.action.ReadValue<float>() == 0 && _leftHandIndexState)
        {
            _leftHandIndexState = false;
            _leftHand.SetBool("indexState", false);
        }
        if (_leftHandIndex.action.ReadValue<float>() == 1 && !_leftHandIndexState)
        {
            _leftHandIndexState = true;
            _leftHand.SetBool("indexState", true);
        }

        //Right Hand
        if (_rightHandGrip.action.ReadValue<float>() == 0 && _rightHandGripState)
        {
            _rightHandGripState = false;
            _rightHand.SetBool("handState", false);
        }
        if (_rightHandGrip.action.ReadValue<float>() == 1 && !_rightHandGripState)
        {
            _rightHandGripState = true;
            _rightHand.SetBool("handState", true);
        }
        if (_rightHandIndex.action.ReadValue<float>() == 0 && _rightHandIndexState)
        {
            _rightHandIndexState = false;
            _rightHand.SetBool("indexState", false);
        }
        if (_rightHandIndex.action.ReadValue<float>() == 1 && !_rightHandIndexState)
        {
            _rightHandIndexState = true;
            _rightHand.SetBool("indexState", true);
        }
    }
}
