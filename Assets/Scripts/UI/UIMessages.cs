using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMessages
{ 
    public struct RemoveUIWidgetMessage
    {
        public string WidgetUID { get; }

        public RemoveUIWidgetMessage(string uid)
        {
            WidgetUID = uid;
        }
    }

    public struct DisplayUIWidgetMessage
    {
        public string UIName { get; }
        public UILayer UILayer { get; }

        public DisplayUIWidgetMessage(string uiName, UILayer layer)
        {
            UIName = uiName;
            UILayer = layer;
        }
    }
}
