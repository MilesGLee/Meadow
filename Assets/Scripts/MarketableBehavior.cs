using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketableBehavior : MonoBehaviour
{
    [SerializeField] private int _worth;
    [SerializeField] private GameManagerBehavior _gameManager;
    public int Worth { get { return _worth; } }

    private bool _canBeSold;

    public bool CanBeSold { set { _canBeSold = value; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnDropped() 
    {
        if (_canBeSold) 
        {
            _gameManager.AddMoney(_worth);
            Destroy(gameObject);
        }
    }

}
