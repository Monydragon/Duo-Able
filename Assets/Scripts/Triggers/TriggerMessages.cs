using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMessages
{
    public struct TriggerMessage
    {
        public string UID { get; }
        public bool Enabled { get; }

        public TriggerMessage(string uid, bool enabled)
        {
            UID = uid;
            Enabled = enabled;
        }
    }
}
