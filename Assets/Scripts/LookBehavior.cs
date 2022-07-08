using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBehavior : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(_target.position);
    }
}
