using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MessagingSystem.MessageSystem;

public enum UILayer
{
    UI,
    NOTIFICATION,
    ERROR,
}

public class UIManager
{
    private Transform _uiRoot;
    private Dictionary<string, UIWidget> _widgetMap;
    private Dictionary<string, Transform> _layerMap;

    public UIManager(Transform uiRoot)
    {
        _widgetMap = new Dictionary<string, UIWidget>();
        _layerMap = new Dictionary<string, Transform>();
        _uiRoot = uiRoot;
        MessageManager.RegisterForChannel(MessageChannels.UI, InternalMessageHandler);
        InitLayers();
    }

    ~UIManager()
    {
        MessageManager.UnregisterForChannel(MessageChannels.UI, InternalMessageHandler);
    }

    private void InternalMessageHandler(IMessageEnvelope envelope)
    {
        Type type = envelope.MessageType;

        if (type.Equals(typeof(UIMessages.DisplayUIWidgetMessage)))
        {
            UIMessages.DisplayUIWidgetMessage message = envelope.Message<UIMessages.DisplayUIWidgetMessage>();
            AddUIToLayer(message.UIName, message.UILayer);
        }
        else if (type.Equals(typeof(UIMessages.RemoveUIWidgetMessage)))
        {
            UIMessages.RemoveUIWidgetMessage message = envelope.Message<UIMessages.RemoveUIWidgetMessage>();
            RemoveWidgetById(message.WidgetUID);
        }
    }

    private void InitLayers()
    {
        string[] layers = Enum.GetNames(typeof(UILayer));
        for (int i = 0; i < layers.Length; ++i)
        {
            string name = layers[i];

            Transform layerFound = _uiRoot.transform.Find(name);
            Transform layer;

            if (layerFound == null)
            {
                //Create layer
                GameObject go = new GameObject(name, typeof(RectTransform));
                layer = go.transform;

                RectTransform rt = go.GetComponent<RectTransform>();
                rt.SetParent(_uiRoot);
                rt.transform.localPosition = Vector3.zero;
                rt.pivot = new Vector2(0.5f, 0.5f);
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.localScale = Vector3.one;
                rt.offsetMin = Vector3.zero;
                rt.offsetMax = Vector3.zero;

                Canvas canvas = go.AddComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingLayerName = name;

                go.layer = 5;
            }
            else
            {
                layer = layerFound;

                Canvas canvas = layer.GetComponent<Canvas>();
                if (canvas == null)
                {
                    canvas = layer.gameObject.AddComponent<Canvas>();
                }

                canvas.overrideSorting = true;
                canvas.sortingLayerName = name;

                layer.gameObject.layer = 5;
            }

            _layerMap.Add(name, layer);
        }
    }

    public UIWidget AddUIToLayer(string uiName, UILayer layer)
    {
        string uid = string.Format("{0}_{1}", layer, uiName);

        GameObject loaded = Resources.Load<GameObject>(uiName);
        if(loaded == null)
        {
            Debug.LogErrorFormat("Resource not found in Resources folder: {0}", uiName);
            return new UIWidget();
        }
        GameObject go = GameObject.Instantiate(loaded, _layerMap[layer.ToString()]);
        go.name = uid;

        UIWidget widget = new UIWidget(uid, uiName, go);
        _widgetMap.Add(uid, widget);

        return widget;
    }

    private void RemoveWidget(UIWidget widget)
    {
        UnityEngine.Object.Destroy(widget.GameObject);
        _widgetMap.Remove(widget.UID);
    }
    
    public void RemoveWidgetById(string widgetUID)
    {
        if(_widgetMap.ContainsKey(widgetUID))
        {
            RemoveWidget(_widgetMap[widgetUID]);
        }
    }
}
