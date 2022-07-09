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

    public void StartHovered(Color color) 
    {
        _outline.enabled = true;
        _outline.OutlineColor = color;
    }

    public void StopHovered()
    {
        _outline.enabled = false;
    }
}
