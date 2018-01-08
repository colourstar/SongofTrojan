﻿/// <summary>
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

    public static void Save(string filename)
    {
        Dictionary<string,object> akGameSave = new Dictionary<string,object>();
        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            string modulename = m_arrModules[i].m_strModuleName;
            akGameSave.Add(modulename, m_arrModules[i].Save());
        }

        string kSaveJsonString = FrameWork.Json.Serialize(akGameSave as object);

        ResourceIOTool.WriteStringByFile(PathTool.GetAbsolutePath(ResLoadLocation.Persistent,"/" + filename),kSaveJsonString);
    }

    public static void Reload(string filename)
    {
        string jsoncontent = ResourceIOTool.ReadStringByFile(PathTool.GetAbsolutePath(ResLoadLocation.Persistent,"/" + filename));
        Dictionary<string,object> akGameLoad = FrameWork.Json.Deserialize(jsoncontent) as Dictionary<string,object>;
        for (int i = 0; i < m_arrModules.Count; ++i)
        {
            string modulename = m_arrModules[i].m_strModuleName;
            if (akGameLoad.ContainsKey(modulename))
            {
                Dictionary<string,object> kModuleLoad = akGameLoad[modulename] as Dictionary<string,object>;
                if (kModuleLoad != null)
                {
                    m_arrModules[i].Reload(kModuleLoad);
                }
            }
        }
    }
}