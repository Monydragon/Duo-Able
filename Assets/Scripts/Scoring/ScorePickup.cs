using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MessagingSystem.MessageSystem;

public class ScorePickup : MonoBehaviour
{
    [SerializeField] private float _score;
    [SerializeField] AudioClip sfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MessageManager.Send(MessageChannels.Scoring, new ScoringMessages.ModifyScoreMessage(_score));
            AudioManager.instance.PlaySFX(sfx);
            Destroy(gameObject);
        }
    }
}
