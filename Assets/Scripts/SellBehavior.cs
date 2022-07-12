using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBehavior : MonoBehaviour
{
    private bool _animationCheck;
    [SerializeField] private List<GameObject> _objectsInRange;
    [SerializeField] private Animator _animator;

    void Start()
    {
        
    }

    void Update()
    {
        if (_objectsInRange != null && _objectsInRange.Count > 0 && !_animationCheck)
        {
            _animationCheck = true;
            _animator.SetTrigger("open");
        }
        if(_objectsInRange.Count == 0 && _animationCheck)
        {
            _animationCheck = false;
            _animator.SetTrigger("close");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MarketableBehavior>() && other.GetComponent<MarketableBehavior>().Worth > 0) 
        {
            _objectsInRange.Add(other.gameObject);
            other.GetComponent<MarketableBehavior>().CanBeSold = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_objectsInRange.Contains(other.gameObject))
        {
            _objectsInRange.Remove(other.gameObject);
            other.GetComponent<MarketableBehavior>().CanBeSold = false;
        }
    }

}
