using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    public string CropType;
    public int TimeToGrow;
    private PlanterBehavior _planterBehavior;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlanterBehavior>())
        {
            _planterBehavior = other.GetComponent<PlanterBehavior>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlanterBehavior>())
        {
            _planterBehavior = null;
        }
    }

    public void PlantSelf() 
    {
        if (_planterBehavior != null) 
        {
            _planterBehavior.PlantCrop(this);
        }
    }
}
