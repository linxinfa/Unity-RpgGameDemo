using UnityEngine;
using System.Collections.Generic;

public delegate void MyEventHandler(params object[] objs);

public class EventDispatcher
{
    public void Regist(string type, MyEventHandler handler)
    {
        if (handler == null)
            return;

        if (!listeners.ContainsKey(type))
        {
            listeners.Add(type, new Dictionary<int, MyEventHandler>());
        }
        var handlerDic = listeners[type];
        var handlerHash = handler.GetHashCode();
        if (handlerDic.ContainsKey(handlerHash))
        {
            handlerDic.Remove(handlerHash);
        }
        listeners[type].Add(handler.GetHashCode(), handler);
    }

    public void UnRegist(string type, MyEventHandler handler)
    {
        if (handler == null)
            return;

        if (listeners.ContainsKey(type))
        {
            listeners[type].Remove(handler.GetHashCode());
            if (null == listeners[type] || 0 == listeners[type].Count)
            {
                listeners.Remove(type);
            }
        }
    }

    public void DispatchEvent(string evt, params object[] objs)
    {
        if (listeners.ContainsKey(evt))
        {
            var handlerDic = listeners[evt];
            if (handlerDic != null && 0 < handlerDic.Count)
            {
                var dic = new Dictionary<int, MyEventHandler>(handlerDic);
                foreach (var f in dic.Values)
                {
                    try
                    {
                        f(objs);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogErrorFormat(szErrorMessage, evt, ex.Message, ex.StackTrace);
                    }
                }
            }
        }
    }


    public void ClearEvents(string key)
    {
        if (listeners.ContainsKey(key))
        {
            listeners.Remove(key);
        }
    }

    private Dictionary<string, Dictionary<int, MyEventHandler>> listeners = new Dictionary<string, Dictionary<int, MyEventHandler>>();
    private readonly string szErrorMessage = "DispatchEvent Error, Event:{0}, Error:{1}, {2}";

    private static EventDispatcher s_instance;
    public static EventDispatcher instance
    {
        get
        {
            if (null == s_instance)
                s_instance = new EventDispatcher();
            return s_instance;
        }
    }
}


