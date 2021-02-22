using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelContext : AppContext
{
    private const string GAME_UI_PREFAB = "UI_GameScreen";
    private UIWidget _gameScreen;
    private AppManager _appManager;

    public LevelContext(AppManager appManager)
    {
        _appManager = appManager;
    }

    public override void Transition(ContextState state, AppContext context = null, Dictionary<string, object> options = null)
    {
        // Non-transition guard
        if (ContextState == state)
        {
            return;
        }

        if (ContextState == ContextState.Inactive && state == ContextState.Active)
        {
            if (context != null)
            {
                context.Transition(ContextState.Background);
            }

            string levelName = "SampleScene";
            if (options != null)
            {
                if (options.ContainsKey("LevelName"))
                {
                    levelName = (string)options["LevelName"];
                }
            }

            SceneManager.LoadScene(levelName);

            _gameScreen = _appManager.UIManager.AddUIToLayer(GAME_UI_PREFAB, UILayer.UI);
        }
        else if (ContextState == ContextState.Active && state == ContextState.Background)
        {
            _gameScreen.GameObject.SetActive(false);

        }
        else if (ContextState == ContextState.Background && state == ContextState.Active)
        {
            _gameScreen.GameObject.SetActive(true);
        }
        else if (state == ContextState.Inactive)
        {
            _appManager.UIManager.RemoveWidgetById(_gameScreen.UID);
        }

        ContextState = state;
    }
}
