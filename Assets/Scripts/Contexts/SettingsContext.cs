using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsContext : AppContext
{
    private const string SETTINGS_UI_PREFAB = "";
    private AppManager _appManager;
    private UIWidget _settingsWidget;

    public SettingsContext(AppManager appManager)
    {
        _appManager = appManager;
    }

    public override void Transition(ContextState state, AppContext context = null, Dictionary<string, object> options = null)
    {
        // Non-transition guard
        if(ContextState == state)
        {
            return;
        }

        if(ContextState == ContextState.Inactive && state == ContextState.Active)
        {
            if(context != null)
            {
                context.Transition(ContextState.Background);
            }

            _settingsWidget = _appManager.UIManager.AddUIToLayer(SETTINGS_UI_PREFAB, UILayer.UI);
        }
        else if(ContextState == ContextState.Active && state == ContextState.Background)
        {
            _settingsWidget.GameObject.SetActive(false);
        }
        else if(ContextState == ContextState.Background && state == ContextState.Active)
        {
            _settingsWidget.GameObject.SetActive(true);
        }
        else if(state == ContextState.Inactive)
        {
            _appManager.UIManager.RemoveWidgetById(_settingsWidget.UID);
        }

        ContextState = state;
    }
}
