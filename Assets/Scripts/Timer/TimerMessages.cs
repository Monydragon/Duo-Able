using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMessages
{
    public struct ModifyTimeMessage
    {
        public float Modifier { get; }

        public ModifyTimeMessage(float mod)
        {
            Modifier = mod;
        }
    }

    public struct SetTimeMessage
    {
        public float Time { get; }

        public SetTimeMessage(float time)
        {
            Time = time;
        }
    }

    public struct ResetTimeMessage { }
    public struct StartTimeMessage { }
    public struct StopTimeMessage { }
}
