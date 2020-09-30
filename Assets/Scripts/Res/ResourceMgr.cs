using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceMgr
{
    public T LoadRes<T>(string resName) where T : Object
    {
        if (m_resDic.ContainsKey(resName))
            return (T)m_resDic[resName];
        T res = Resources.Load<T>(resName);
        m_resDic[resName] = res;
        return res;
    }

    private Dictionary<string, Object> m_resDic = new Dictionary<string, Object>();

    private static ResourceMgr s_instance;
    public static ResourceMgr instance
    {
        get
        {
            if (null == s_instance)
                s_instance = new ResourceMgr();
            return s_instance;
        }
    }
}
