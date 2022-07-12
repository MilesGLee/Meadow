using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketableBehavior : MonoBehaviour
{
    [SerializeField] private int _worth;
    public GameManagerBehavior GameManager;
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
            GameManager.AddMoney(_worth);
            transform.position = new Vector3(0, -10, 0);
            RoutineBehaviour.Instance.StartNewTimedAction(args => { Destroy(gameObject); }, TimedActionCountType.SCALEDTIME, 1f); 
        }
    }

}
