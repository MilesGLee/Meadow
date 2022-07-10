using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    [Header("Day and Sleep System")]
    private RoutineBehaviour.TimedAction _timeTick; //Currently set to 24 minute day cycles
    [SerializeField] private int _day; //5 days in gameplay
    [SerializeField] private int _time; //Increased every second
    [SerializeField] private GameObject _world; //The playable world
    [SerializeField] private GameObject _statsWorld; //The world that displays when a day changes
    [SerializeField] private Text _dayNumberText;
    [SerializeField] private Text _moneyEarnedText;
    [SerializeField] private UnityEvent _onDayEvent;
    private bool _slept;
    [Header("Money System")]
    [SerializeField] private int _money; //The players current money amount
    [SerializeField] private int _moneyEarned; //The amount of money the player earned that day
    [SerializeField] private Transform _sellPoint;

    void Start()
    {
        //Default variables
        _day = 1;
        _time = 0;
        _world.SetActive(true);
        _statsWorld.SetActive(false);
        _timeTick = RoutineBehaviour.Instance.StartNewTimedAction(args => { _time += 1; }, TimedActionCountType.SCALEDTIME, 1f);
    }

    void Update()
    {
        DayCycle();

        _dayNumberText.text = "On to day " + _day;
        _moneyEarnedText.text = "Money Earned: " + _moneyEarned;
    }

    void DayCycle()
    {
        //Loop to increase the time every second
        if (!_timeTick.IsActive)
        {
            _timeTick = RoutineBehaviour.Instance.StartNewTimedAction(args => { _time += 1; }, TimedActionCountType.SCALEDTIME, 1f);
        }

        // 1440 seconds is 24 minutes
        if (_time >= 1440)
        {
            _slept = false;
            DayOver();
        }
    }

    public void Sleep() 
    {
        _slept = true;
        DayOver();
    }

    void DayOver()
    {
        //Increase the day and switch to the statistic display world
        _time = 0;
        _day++;
        _world.SetActive(false);
        _statsWorld.SetActive(true);
    }

    public void DayBegin() 
    {
        //Set the players money and time depending if they slept at all and call day event
        _onDayEvent.Invoke();
        _money += _moneyEarned;
        _moneyEarned = 0;
        if (_slept)
            _time = 0;
        else
            _time = 720;
        _world.SetActive(true);
        _statsWorld.SetActive(false);
    }

    public void AddMoney(int amount) 
    {
        _moneyEarned += amount;
    }
}
