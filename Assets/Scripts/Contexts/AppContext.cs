using System.Collections.Generic;

public enum ContextState
{
    Inactive = 0,
    Active = 1,
    Background = 2,
}

public abstract class AppContext
{
    /// <summary>
    /// The current state of the context
    /// </summary>
    public ContextState ContextState { get; protected set; }

    /// <summary>
    /// Transition to the specified context and state
    /// </summary>
    /// <param name="state"></param>
    /// <param name="context"></param>
    /// <param name="options"></param>
    public abstract void Transition(ContextState state, AppContext context = null, Dictionary<string, object> options = null);
}
