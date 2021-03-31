using System;
using UnityEngine;
using static MessagingSystem.MessageSystem;

public abstract class DualTriggerReceiver : MonoBehaviour
{
    [SerializeField] protected string TriggerUID;
    [SerializeField] protected string TriggerUID2;
    [SerializeField] protected bool trigger1;
    [SerializeField] protected bool trigger2;

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
                trigger1 = message.Enabled;
            }
            if (message.UID == TriggerUID2)
            {
                trigger2 = message.Enabled;
            }

            ReceiveTrigger(trigger1, trigger2);

        }
    }

    protected abstract void ReceiveTrigger(bool triggered1, bool triggered2);
}
