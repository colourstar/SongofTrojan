/// <summary>
/// Story manager.
/// 剧情管理器,内置剧情
/// </summary>
/// 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StoryManager
{
	private Dictionary<string,Story> 	m_dicStorys = new Dictionary<string,Story>();
	private List<Story> 				m_activestoryforblocks = new List<Story>();

	private bool 						m_isbegin = false;
	private Story 						m_currentstory = null;
	private List<Story> 				m_activestorys = new List<Story>();

	// Use this for initialization
	public void Init ()
	{
		m_currentstory = null;
		m_isbegin = false;
		m_dicStorys.Clear ();
		m_activestorys.Clear ();
		m_activestoryforblocks.Clear ();

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
					m_currentstory = storyinstance;
				}
			}

			storyinstance.AddAction (storystruct);
		}
	}

	public void Start()
	{
		m_isbegin = true;
		if (m_currentstory == null) 
		{
			return;
		}

		m_currentstory.Start ();
	}
	
	// Update is called once per frame
	public void Update ()
	{
		if (m_isbegin == false) 
		{
			return;
		}
		if (m_currentstory == null)
		{
			if (m_activestorys.Count == 0)
			{
				return;
			}
			else
			{
				int count = m_activestorys.Count;
				m_currentstory = m_activestorys[count - 1];
				m_activestorys.RemoveAt(count - 1);
			}
		}

		// 剧情结果判定
		if (m_currentstory.GetAllActionEnd() == true)
		{
			m_currentstory = null;
			return;
		}

		m_currentstory.Update();
	}

	public void JumptoStory(string key)
	{
		Debug.Log ("[storymanager] : JumptoStory,storyname : " + key);
		if (m_dicStorys.ContainsKey(key) == false)
		{
			Debug.LogError("[storymanager] : JumptoStory Error,storyname : " + key);
			return;
		}
		if (m_currentstory != null)
		{
			m_activestorys.Add(m_currentstory);
		}
		m_currentstory = m_dicStorys[key];

		m_currentstory.Start();
	}
}

