using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    [Header("Day and Sleep System")]
    private RoutineBehaviour.TimedAction _timeTick;
    [SerializeField] private int _day;
    [SerializeField] private int _time;
    [SerializeField] private GameObject _world;
    [SerializeField] private GameObject _statsWorld;
    [SerializeField] private Text _dayNumberText;
    [SerializeField] private Text _moneyEarnedText;
    private bool _slept;
    [Header("Money System")]
    [SerializeField] private int _money;
    [SerializeField] private int _moneyEarned;

    void Start()
    {
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
        _moneyEarnedText.text = "Money Earned: ";
    }

    void DayCycle()
    {
        if (!_timeTick.IsActive)
        {
            _timeTick = RoutineBehaviour.Instance.StartNewTimedAction(args => { _time += 1; }, TimedActionCountType.SCALEDTIME, 1f);
        }

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
        _time = 0;
        _day++;
        _world.SetActive(false);
        _statsWorld.SetActive(true);
    }

    public void DayBegin() 
    {
        _money += _moneyEarned;
        _moneyEarned = 0;
        if (_slept)
            _time = 0;
        else
            _time = 720;
        _world.SetActive(true);
        _statsWorld.SetActive(false);
    }
}
