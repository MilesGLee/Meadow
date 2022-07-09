using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandStateBehavior : MonoBehaviour
{
    public enum HandState 
    {
        OPEN,
        CLOSED,
        POINT,
        INDEX
    }

    [Header("Right Hand Variables")]
    [SerializeField] private Animator _animatorR;
    [SerializeField] private InputActionReference _handGripR = null;
    [SerializeField] private InputActionReference _handTriggerR = null;
    private HandState _handStateR;
    public HandState HandStateR { get { return _handStateR; } }
    private bool _handGripCheckR;
    private bool _handTriggerCheckR;

    [Header("Left Hand Variables")]
    [SerializeField] private Animator _animatorL;
    [SerializeField] private InputActionReference _handGripL = null;
    [SerializeField] private InputActionReference _handTriggerL = null;
    private HandState _handStateL;
    public HandState HandStateL { get { return _handStateL; } }
    private bool _handGripCheckL;
    private bool _handTriggerCheckL;

    private void Awake()
    {
        //Default animate state for both hands
        _handStateR = HandState.OPEN;
        _handStateL = HandState.OPEN;
    }

    void Update()
    {
        Animate();
        ChangeState();
    }

    void ChangeState() 
    {
        //Right hand states
        if (_animatorR.GetBool("grip") && !_animatorR.GetBool("trigger")) 
        {
            _handStateR = HandState.POINT;
        }
        if (!_animatorR.GetBool("grip") && !_animatorR.GetBool("trigger"))
        {
            _handStateR = HandState.OPEN;
        }
        if (_animatorR.GetBool("grip") && _animatorR.GetBool("trigger"))
        {
            _handStateR = HandState.CLOSED;
        }
        if (!_animatorR.GetBool("grip") && _animatorR.GetBool("trigger"))
        {
            _handStateR = HandState.INDEX;
        }

        //Left hand states
        if (_animatorL.GetBool("grip") && !_animatorL.GetBool("trigger"))
        {
            _handStateL = HandState.POINT;
        }
        if (!_animatorL.GetBool("grip") && !_animatorL.GetBool("trigger"))
        {
            _handStateL = HandState.OPEN;
        }
        if (_animatorL.GetBool("grip") && _animatorL.GetBool("trigger"))
        {
            _handStateL = HandState.CLOSED;
        }
        if (!_animatorL.GetBool("grip") && _animatorL.GetBool("trigger"))
        {
            _handStateL = HandState.INDEX;
        }
    }

    void Animate() 
    {
        //Right Hand Animate
        //Grip Open
        if (_handGripR.action.ReadValue<float>() == 0 && _handGripCheckR)
        {
            _handGripCheckR = false;
            _animatorR.SetBool("grip", false);
        }
        //Grip Closed
        if (_handGripR.action.ReadValue<float>() == 1 && !_handGripCheckR)
        {
            _handGripCheckR = true;
            _animatorR.SetBool("grip", true);
        }
        //Trigger Open
        if (_handTriggerR.action.ReadValue<float>() == 0 && _handTriggerCheckR)
        {
            _handTriggerCheckR = false;
            _animatorR.SetBool("trigger", false);
        }
        //Trigger Closed
        if (_handTriggerR.action.ReadValue<float>() == 1 && !_handTriggerCheckR)
        {
            _handTriggerCheckR = true;
            _animatorR.SetBool("trigger", true);
        }

        //Left Hand Animate
        //Grip Open
        if (_handGripL.action.ReadValue<float>() == 0 && _handGripCheckL)
        {
            _handGripCheckL = false;
            _animatorL.SetBool("grip", false);
        }
        //Grip Closed
        if (_handGripL.action.ReadValue<float>() == 1 && !_handGripCheckL)
        {
            _handGripCheckL = true;
            _animatorL.SetBool("grip", true);
        }
        //Trigger Open
        if (_handTriggerL.action.ReadValue<float>() == 0 && _handTriggerCheckL)
        {
            _handTriggerCheckL = false;
            _animatorL.SetBool("trigger", false);
        }
        //Trigger Closed
        if (_handTriggerL.action.ReadValue<float>() == 1 && !_handTriggerCheckL)
        {
            _handTriggerCheckL = true;
            _animatorL.SetBool("trigger", true);
        }
    }
}
