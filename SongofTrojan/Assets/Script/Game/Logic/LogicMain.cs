/// <summary>
/// Logic main.
/// 主逻辑模块
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class LogicMain
{
    public List<IModuleBase> m_arrModules = new List<IModuleBase>();

	// Init Progress
	public void Init()
	{
        m_arrModules.Add(new StoryManager());
        m_arrModules.Add(new MapManager());
        m_arrModules.Add(new MessageManager());
        m_arrModules.Add(new CharacterManager());

        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            m_arrModules[i].Init();
        }
	}

    public void Start()
    {
        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            m_arrModules[i].Start();
        }
    }

	// Update is called once per logic frame
	public void Update ()
	{
        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            if (m_arrModules[i].IsBegin() == true)
            {
                m_arrModules[i].Update();
            }
        }
	}

    public IModuleBase GetModule(string kModuleName)
    {
        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            if (m_arrModules[i].m_strModuleName == kModuleName)
            {
                return m_arrModules[i];
            }
        }

        return null;
    }
}