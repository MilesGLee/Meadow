using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectBehavior : MonoBehaviour
{
    private Outline _outline;
    private Transform _moveTarget;
    private float _stoppingDistance = 0.125f;
    private bool _hasTarget;

    void Start()
    {
        _outline = GetComponent<Outline>();
    }

    private void Update()
    {
        //If has a target and outside the stopping distance, keep moving towards the target
        if (_moveTarget != null && Vector3.Distance(transform.position, _moveTarget.position) > _stoppingDistance) 
        {
            transform.position = Vector3.Lerp(transform.position, _moveTarget.position, 0.15f);
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            //If within the stopping distance, remove target and stop moving
            if (Vector3.Distance(transform.position, _moveTarget.position) <= _stoppingDistance) 
            {
                _moveTarget = null;
                _hasTarget = false;
            }
        }
    }

    public void StartHovered(Color color) 
    {
        _outline.enabled = true;
        _outline.OutlineColor = color;
    }

    public void StopHovered()
    {
        _outline.enabled = false;
    }

    public void SetTarget(Transform target) 
    {
        if (!_hasTarget)
        {
            _moveTarget = target;
            _hasTarget = true;
        }
    }
}
