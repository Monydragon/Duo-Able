using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MessagingSystem.MessageSystem;

public class UIGameScreen : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private float _score;

    private void Start()
    {
        MessageManager.RegisterForChannel(MessageChannels.UI, InternalMessageHandler);
        UpdateScoreUI();
    }

    private void OnDestroy()
    {
        MessageManager.UnregisterForChannel(MessageChannels.UI, InternalMessageHandler);    
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
}
