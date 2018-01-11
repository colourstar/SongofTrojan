/// <summary>
/// Logic main.
/// 角色
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Character
{
    public Dictionary<int,Attr> m_dicAttrs = new Dictionary<int, Attr>();
    public int                  m_iTypeID = 0;

    public void Init(int iTypeid)
    {
        m_iTypeID = iTypeid;
    }

    public void Update()
    {
        
    }
}