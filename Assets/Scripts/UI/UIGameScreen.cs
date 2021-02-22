using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MessagingSystem.MessageSystem;

public class UIGameScreen : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timerText;
    [SerializeField] private bool timerIsRunning = false;
    [SerializeField] private float timeRemaining = 120;
    private float _score;
    private float _timeDisplay;

    private void Start()
    {
        MessageManager.RegisterForChannel(MessageChannels.Scoring, InternalMessageHandler);
        MessageManager.RegisterForChannel(MessageChannels.Timer, InternalMessageHandler);
        UpdateScoreUI();
    }

    private void OnDestroy()
    {
        MessageManager.UnregisterForChannel(MessageChannels.Scoring, InternalMessageHandler);    
        MessageManager.UnregisterForChannel(MessageChannels.Timer, InternalMessageHandler);    
    }

    private void InternalMessageHandler(IMessageEnvelope envelope)
    {
        Type type = envelope.MessageType;

        if(type.Equals(typeof(ScoringMessages.ModifyScoreMessage)))
        {
            ScoringMessages.ModifyScoreMessage message = envelope.Message<ScoringMessages.ModifyScoreMessage>();
            ModifyScore(message.Modifier);
        }
        else if(type.Equals(typeof(ScoringMessages.SetScoreMessage)))
        {
            ScoringMessages.SetScoreMessage message = envelope.Message<ScoringMessages.SetScoreMessage>();
            SetScore(message.Score);
        }
        else if(type.Equals(typeof(ScoringMessages.ResetScoreMessage)))
        {
            ResetScore();
        }
        else if (type.Equals(typeof(TimerMessages.ModifyTimeMessage)))
        {
            TimerMessages.ModifyTimeMessage message = envelope.Message<TimerMessages.ModifyTimeMessage>();
            timeRemaining += message.Modifier;
        }
        else if (type.Equals(typeof(TimerMessages.SetTimeMessage)))
        {
            TimerMessages.SetTimeMessage message = envelope.Message<TimerMessages.SetTimeMessage>();
            timeRemaining = message.Time;
        }
        else if (type.Equals(typeof(TimerMessages.StopTimeMessage)))
        {
            timerIsRunning = false;
        }
        else if (type.Equals(typeof(TimerMessages.StartTimeMessage)))
        {
            timerIsRunning = true;
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time Has Expired");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    private void ResetScore()
    {
        _score = 0;
        UpdateScoreUI();
    }

    private void SetScore(float score)
    {
        _score = score;
        UpdateScoreUI();
    }

    private void ModifyScore(float mod)
    {
        _score += mod;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (_scoreText != null)
        {
            _scoreText.text = string.Format("Score: {0}", _score);
        }
    }

   private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
