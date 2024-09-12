using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int _gameTimeInSeconds;
    [SerializeField] private TextMeshProUGUI _timerText;

    private int _timeRemaining;
    private int _minutes;
    private int _seconds;

    private float _elapsedTime;

    private void Start()
    {
        _timeRemaining = _gameTimeInSeconds;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime > 1)
        {
            _timeRemaining -= 1;
            _elapsedTime = 0f;
        }

        _minutes = _timeRemaining / 60;
        _seconds = _timeRemaining - _minutes * 60;

        _timerText.text = ($"{_minutes}:{_seconds}");

        if(_timeRemaining <= 0 )
        {
            //déclencher fin du jeu
        }
    }
}
