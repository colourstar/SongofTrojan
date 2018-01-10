/// <summary>
/// Story manager.
/// 剧情管理器,内置剧情
/// </summary>
/// 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class StoryManager : IModuleBase
{
    public StoryManager() : base("StoryManager"){}

	private Dictionary<string,Story> 	m_dicStorys = new Dictionary<string,Story>();
	private List<Story> 				m_arrActivestoryforblocks = new List<Story>();

	private Story 						m_kCurrentstory = null;
	private List<Story> 				m_arrActivestorys = new List<Story>();

	// Use this for initialization
	public override void Init ()
	{
		m_kCurrentstory = null;
		m_dicStorys.Clear ();
		m_arrActivestorys.Clear ();
		m_arrActivestoryforblocks.Clear ();

		// 首先读取配置表,加载所有的剧情
		table.Config kConfig = TabtoyConfigManager.GetConfig();
		for (int i = 0; i < kConfig.Story.Count; i++) 
		{
			var storystruct = kConfig.Story[i];
			string storyname = storystruct.Name;
			Story storyinstance = null;
			if (m_dicStorys.ContainsKey (storyname)) 
			{
				storyinstance = m_dicStorys [storyname];
			}
			else 
			{
				storyinstance = new Story();
				storyinstance.Init(storyname);
				m_dicStorys.Add(storyname,storyinstance);
				storyinstance = m_dicStorys[storyname];

				if (storystruct.InitOpen == true) 
				{
					m_kCurrentstory = storyinstance;
				}
			}

			storyinstance.AddAction (storystruct);
		}
	}

    public override void Start()
	{
        base.Start();

		if (m_kCurrentstory == null) 
		{
			return;
		}

		m_kCurrentstory.Start ();
	}
	
	// Update is called once per frame
    public override void Update ()
	{
		if (m_kCurrentstory == null)
		{
			if (m_arrActivestorys.Count == 0)
			{
				return;
			}
			else
			{
				int count = m_arrActivestorys.Count;
				m_kCurrentstory = m_arrActivestorys[count - 1];
				m_arrActivestorys.RemoveAt(count - 1);
			}
		}

		// 剧情结果判定
		if (m_kCurrentstory.GetAllActionEnd() == true)
		{
			m_kCurrentstory = null;
			return;
		}

		m_kCurrentstory.Update();
	}

	public void JumptoStory(string key)
	{
		Debug.Log ("[storymanager] : JumptoStory,storyname : " + key);
		if (m_dicStorys.ContainsKey(key) == false)
		{
			Debug.LogError("[storymanager] : JumptoStory Error,storyname : " + key);
			return;
		}
		if (m_kCurrentstory != null)
		{
			m_arrActivestorys.Add(m_kCurrentstory);
		}
		m_kCurrentstory = m_dicStorys[key];

		m_kCurrentstory.Start();
	}

    public Story GetCurrentStory()
    {
        return m_kCurrentstory;
    }
}

