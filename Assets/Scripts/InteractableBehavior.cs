using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
    [SerializeField] private HandController _handController;
    private Transform _parent;
    private bool _handType; //True = right, False = left
    public bool Held;

    void Start()
    {
        
    }

    void Update()
    {

        if (_parent != null) 
        {
            transform.position = Vector3.Lerp(transform.position, _parent.position, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, _parent.rotation, 1f);
            GetComponent<Rigidbody>().useGravity = false;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (!_handType && !_handController.LeftHandClosed && Held)
            StopGrabbed();
        if (_handType && !_handController.RightHandClosed && Held)
            StopGrabbed();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "leftHand" && _handController.LeftHandClosed && _handController.LeftHeldObject == null && !Held)
        {
            StartGrabbed(other.transform, false);
        }
        if (other.tag == "rightHand" && _handController.RightHandClosed && _handController.RightHeldObject == null && !Held)
        {
            StartGrabbed(other.transform, true);
        }
    }

    void StartGrabbed(Transform parent, bool type) 
    {
        _parent = parent;
        _handType = type;
        Held = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = false;
        if (type)
            _handController.RightHeldObject = transform;
        if (!type)
            _handController.LeftHeldObject = transform;
    }

    void StopGrabbed() 
    {
        Held = false;
        GetComponent<Rigidbody>().useGravity = true;
        _parent = null;
        if (_handType)
        {
            Debug.Log(_handController.RightVelocity);
            GetComponent<Rigidbody>().AddForce(_handController.RightVelocity * 500);
            _handController.RightHeldObject = null;
        }
        if (!_handType)
        {
            GetComponent<Rigidbody>().AddForce(_handController.LeftVelocity * 500);
            _handController.LeftHeldObject = null;
        }
    }
}
