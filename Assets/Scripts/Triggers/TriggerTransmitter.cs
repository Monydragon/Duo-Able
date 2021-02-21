using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MessagingSystem.MessageSystem;

public class TriggerTransmitter : MonoBehaviour
{
    [SerializeField] protected string UID;
    [SerializeField] protected bool Triggered;

    public virtual void Trigger()
    {
        if(!string.IsNullOrEmpty(UID))
        {
            Triggered = !Triggered;
            MessageManager.Send(MessageChannels.Triggers, new TriggerMessages.TriggerMessage(UID, Triggered));
        }
        else
        {
            Debug.LogErrorFormat("UID has not been set for gameobject {0}", gameObject.name);
        }
    }
}
