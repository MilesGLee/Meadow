using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandModelBehavior : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = _parent.position;
        transform.rotation = _parent.rotation;
    }
}
