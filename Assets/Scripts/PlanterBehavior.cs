using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterBehavior : MonoBehaviour
{
    [SerializeField] private Outline _outline;
    private string _currentCrop;
    private int _timeToGrow;
    private int _timeGrown;
    private bool _planted;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject _carrotSpawn;
    [SerializeField] private GameManagerBehavior _gameManager;
    [SerializeField] private Rigidbody _springPosition;

    void Start()
    {
        _outline.enabled = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CropBehavior>() && _planted == false)
        {
            _outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CropBehavior>() && _planted == false) 
        {
            _outline.enabled = false;
        }
    }

    public void PlantCrop(CropBehavior crop) 
    {
        _planted = true;
        _outline.enabled = false;
        _currentCrop = crop.CropType;
        _timeToGrow = crop.TimeToGrow;
        _timeGrown = 0;
        Destroy(crop.gameObject);
    }

    public void GrowCrop() 
    {
        if (_planted)
        {
            _timeGrown++;
            if (_timeGrown >= _timeToGrow)
            {
                if (_currentCrop == "Carrot")
                {
                    GameObject spawned = Instantiate(_carrotSpawn, _spawnPosition.position, Quaternion.identity);
                    spawned.GetComponent<MarketableBehavior>().GameManager = _gameManager;
                    SpringJoint spring = spawned.AddComponent<SpringJoint>();
                    spring.connectedBody = _springPosition;
                    spring.breakForce = 1;
                    _planted = false;
                }
            }
        }
    }
}
