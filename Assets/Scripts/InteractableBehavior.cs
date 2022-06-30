using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
    [SerializeField] private HandController _handController;
    private Transform _parent;
    private bool _handType; //True = right, False = left

    void Start()
    {
        
    }

    void Update()
    {
        if (_parent != null) 
        {
            transform.position = Vector3.Lerp(transform.position, _parent.position, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, _parent.rotation, 1f);
        }

        if (!_handType && !_handController.LeftHandClosed)
            StopGrabbed();
        if (_handType && !_handController.RightHandClosed)
            StopGrabbed();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "leftHand" && _handController.LeftHandClosed && _handController.LeftHeldObject == null)
        {
            StartGrabbed(other.transform, false);
        }
        else if (other.tag == "rightHand" && _handController.RightHandClosed && _handController.RightHeldObject == null)
        {
            StartGrabbed(other.transform, true);
        }
    }

    void StartGrabbed(Transform parent, bool type) 
    {
        _parent = parent;
        _handType = type;
        GetComponent<Rigidbody>().useGravity = false;
        if (type)
            _handController.RightHeldObject = transform;
        if (!type)
            _handController.LeftHeldObject = transform;
    }

    void StopGrabbed() 
    {
        GetComponent<Rigidbody>().useGravity = true;
        _parent = null;
        if (_handType)
            _handController.RightHeldObject = null;
        if (!_handType)
            _handController.LeftHeldObject = null;
    }
}
