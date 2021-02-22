using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringMessages
{
    public struct ModifyScoreMessage
    {
        public float Modifier { get; }

        public ModifyScoreMessage(float mod)
        {
            Modifier = mod;
        }
    }

    public struct SetScoreMessage
    {
        public float Score { get; }

        public SetScoreMessage(float score)
        {
            Score = score;
        }
    }

    public struct ResetScoreMessage {}
}
