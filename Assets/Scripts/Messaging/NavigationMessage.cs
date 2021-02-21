using System;
using System.Collections.Generic;

public struct NavigationMessage
{
    public Type NavigateToContext { get; }
    public bool PopCurrentContext { get; }
    public Dictionary<string, object> Options { get; }
    public NavigationMessage(Type navigateToContext, bool popCurrentContext, Dictionary<string, object> options = null)
    {
        NavigateToContext = navigateToContext;
        PopCurrentContext = popCurrentContext;
        Options = options;
    }
}
