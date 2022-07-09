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
        POINT
    }

    [Header("Right Hand Variables")]
    [SerializeField] private Animator _animatorR;
    [SerializeField] private InputActionReference _handGripR = null;
    [SerializeField] private InputActionReference _handTriggerR = null;
    private HandState _handStateR;
    public HandState HandStateR { get { return _handStateR; } }
    private bool _handGripCheckR;
    private bool _handTriggerCheckR;

    void Update()
    {
        
    }

    void ChangeState() 
    {
        
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
            _animatorR.SetBool("index", false);
        }
        //Trigger Closed
        if (_handTriggerR.action.ReadValue<float>() == 1 && !_handTriggerCheckR)
        {
            _handTriggerCheckR = true;
            _animatorR.SetBool("index", true);
        }
    }
}
