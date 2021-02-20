using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UIWidget
{
    public string UID { get; }
    public string UIName { get; }
    public GameObject GameObject { get; }

    public UIWidget(string uid, string uiName, GameObject gameObject)
    {
        UID = uid;
        UIName = uiName;
        GameObject = gameObject;
    }
}
