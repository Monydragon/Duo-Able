using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MessagingSystem.MessageSystem;

public class AppManager : MonoBehaviour
{
    public ContextManager ContextManager;

    public UIManager UIManager;


    [SerializeField] private GameObject _uiRoot;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        MessageManager.RegisterForChannel(MessageChannels.Navigation, InternalMessageHandler);
    }

    private void Start()
    {
        ContextManager = new ContextManager();

        // Create UI Root
        GameObject go = Instantiate(_uiRoot);
        UIManager = new UIManager(go.transform);
    }

    private void OnDestroy()
    {
        MessageManager.UnregisterForChannel(MessageChannels.Navigation, InternalMessageHandler);
    }

    private void InternalMessageHandler(IMessageEnvelope envelope)
    {
        Type type = envelope.MessageType;

        if(type.Equals(typeof(NavigationMessage)))
        {
            NavigationMessage message = envelope.Message<NavigationMessage>();
            
            if (message.PopCurrentContext)
            {
                ContextManager.PopContext();
            }

            if(message.NavigateToContext == null)
            {
                return;
            }

            if (message.NavigateToContext == typeof(LevelContext))
            {
                ContextManager.PushContext(new LevelContext(this), message.Options);
            }
            else if(message.NavigateToContext == typeof(SettingsContext))
            {
                ContextManager.PushContext(new SettingsContext(this), message.Options);
            }
            else
            {
                Debug.LogWarningFormat("Unsupported Context Type attempted to be pushed: {0}", message.NavigateToContext.ToString());
            }
        }
    }
}
