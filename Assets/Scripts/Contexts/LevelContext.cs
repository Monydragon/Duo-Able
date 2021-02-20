using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContext : AppContext
{
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

            // TODO: Spawn UI for Level screen

        }
        else if (ContextState == ContextState.Active && state == ContextState.Background)
        {
            // TODO: Set UI for Level screen to inactive
        }
        else if (ContextState == ContextState.Background && state == ContextState.Active)
        {
            // TODO: Set UI for Level screen to active
        }
        else if (state == ContextState.Inactive)
        {
            // TODO: Remove UI
        }

        ContextState = state;
    }
}
