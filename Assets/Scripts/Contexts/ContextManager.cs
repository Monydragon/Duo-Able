using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextManager
{
    private Stack<AppContext> _contexts;
    private List<string> _typeCache;

    public ContextManager()
    {
        _contexts = new Stack<AppContext>();
        _typeCache = new List<string>();
    }

    public void PushContext(AppContext context, Dictionary<string, object> options = null)
    {
        try
        {
            Debug.LogFormat("Attempting to push context {0}", context.ToString());
            string contextType = context.GetType().ToString();

            // Double load prevention
            if(_typeCache.Contains(contextType))
            {
                Debug.LogErrorFormat("Context type {0} has already been loaded.", contextType);
                return;
            }

            AppContext prevContext = _contexts.Count == 0 ? null : _contexts.Peek();

            _contexts.Push(context);
            _typeCache.Add(contextType);

            if(prevContext != null)
            {
                prevContext.Transition(ContextState.Background);
            }

            context.Transition(ContextState.Active, prevContext, options);
        }
        catch(Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void PopContext()
    {
        try
        {
            Debug.Log("Attempting to pop context");

            // Empty Guard
            if (_contexts.Count == 0)
            {
                throw new ContextExceptions.ContextNotFoundException();
            }

            AppContext popContext = _contexts.Pop();
            _typeCache.Remove(popContext.GetType().ToString());
            AppContext topContext = _contexts.Count == 0 ? null : _contexts.Peek();

            popContext.Transition(ContextState.Inactive);

            if (topContext != null)
            {
                Debug.LogFormat("Setting context active: {0}", topContext.ToString());
                topContext.Transition(ContextState.Active, popContext);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
    public void PopContext(NavigationMessage message)
    {
        try
        {
            Debug.Log("Attempting to pop context");

            // Empty Guard
            if (_contexts.Count == 0)
            {
                throw new ContextExceptions.ContextNotFoundException();
            }

            AppContext popContext = _contexts.Pop();
            _typeCache.Remove(popContext.GetType().ToString());
            AppContext topContext = _contexts.Count == 0 ? null : _contexts.Peek();

            popContext.Transition(ContextState.Inactive);

            if (topContext != null)
            {
                Debug.LogFormat("Setting context active: {0}", topContext.ToString());
                if(message.Options != null)
                {
                    topContext.Transition(ContextState.Active, popContext, message.Options);
                }
                else
                {
                    topContext.Transition(ContextState.Active, popContext);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void PopAllContexts()
    {
        int length = _contexts.Count;
        for(int i=0;i<length;++i)
        {
            AppContext context = _contexts.Pop();
            context.Transition(ContextState.Inactive);
        }
    }
}
