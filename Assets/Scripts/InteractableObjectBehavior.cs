using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectBehavior : MonoBehaviour
{
    private Outline _outline;

    void Start()
    {
        _outline = GetComponent<Outline>();
    }

    void LateUpdate()
    {
        _outline.enabled = false;
    }

    public void Hovered(Color color) 
    {
        _outline.enabled = true;
        _outline.OutlineColor = color;
    }
}
