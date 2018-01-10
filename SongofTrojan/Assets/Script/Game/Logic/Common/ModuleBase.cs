/// <summary>
/// Module Base.
/// 模块基类
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class IModuleBase
{
    protected bool      m_bIsBegin = false;
    public string       m_strModuleName = "";

    public IModuleBase(string strName)
    {
        m_strModuleName = strName;
    }

    public virtual void     Init()
    {
        
    }

    public virtual void     Start()
    {
        m_bIsBegin = true;
    }

    public virtual void     Update()
    {
        
    }

    public bool             IsBegin()
    {
        return m_bIsBegin;
    }

    public virtual void     Reload(Dictionary<string,object> akLoad)
    {
    }

    public virtual object   Save()
    {
        return new Dictionary<string,object>();
    }

}