using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MessagingSystem.MessageSystem;

public class ScorePickup : MonoBehaviour
{
    [SerializeField] private float _score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MessageManager.Send(MessageChannels.Scoring, new ScoringMessages.ModifyScoreMessage(_score));
            Destroy(gameObject);
        }
    }
}
