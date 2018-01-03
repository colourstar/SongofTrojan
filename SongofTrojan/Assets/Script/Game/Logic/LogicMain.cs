/// <summary>
/// Logic main.
/// 主逻辑模块
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class LogicMain
{
    public static List<IModuleBase> m_arrModules = new List<IModuleBase>();

	// Init Progress
	public static void Init()
	{
        m_arrModules.Add(new StoryManager());
        m_arrModules.Add(new MapManager());
        m_arrModules.Add(new MessageManager());

        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            m_arrModules[i].Init();
        }
	}

    public static void Start()
    {
        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            m_arrModules[i].Start();
        }
    }

	// Update is called once per logic frame
	public static void Update ()
	{
        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            if (m_arrModules[i].IsBegin() == true)
            {
                m_arrModules[i].Update();
            }
        }
	}

    public static IModuleBase GetModule(string kModuleName)
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