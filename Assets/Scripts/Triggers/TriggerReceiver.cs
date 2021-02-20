using System;
using UnityEngine;
using static MessagingSystem.MessageSystem;

public abstract class TriggerReceiver : MonoBehaviour
{
    [SerializeField] protected string TriggerUID;

    protected virtual void Start()
    {
        MessageManager.RegisterForChannel(MessageChannels.Triggers, InternalMessageHandler);
    }

    protected virtual void OnDestroy()
    {
        MessageManager.UnregisterForChannel(MessageChannels.Triggers, InternalMessageHandler);
    }

    protected virtual void InternalMessageHandler(IMessageEnvelope envelope)
    {
        Type type = envelope.MessageType;

        if(type.Equals(typeof(TriggerMessages.TriggerMessage)))
        {
            TriggerMessages.TriggerMessage message = envelope.Message<TriggerMessages.TriggerMessage>();
            if (message.UID == TriggerUID)
            {
                ReceiveTrigger(message.Enabled);
            }
        }
    }

    protected abstract void ReceiveTrigger(bool triggered);
}
