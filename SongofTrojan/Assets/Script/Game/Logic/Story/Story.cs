using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Story
{
	private string 				m_storyname = "";						// 剧情名称
	private List<ActionBase> 	m_ActionList = new List<ActionBase>();	// 所有动作列表
	private int 				m_curactionidx = 0;						// 当前动作索引,0 ~ (size - 1)
	private bool 				m_isbegin = false;						// 剧情是否开始
	private Dictionary<string,string> m_dicRegister = new Dictionary<string,string>();	// 剧情内部的寄存器
	private int 				m_result = 0;							// 剧情结果值
	private bool 				m_isallactionend = false;				// 剧情所有动作都已结束

	// Use this for initialization
	public void Init (string storyname)
	{
		m_storyname = storyname;
	}

	public void Start()
	{
		Debug.Log ("[story] : start,storyname : " + m_storyname);
		m_isbegin = true;
		m_curactionidx = 0;
		m_isallactionend = false;
		if (m_ActionList.Count == 0) 
		{
			_End ();
		}
		else 
		{
			for (int i = 0; i < m_ActionList.Count; ++i) 
			{
				var action = m_ActionList [i];
				action.SetState (ActionBase.ActionState.AS_Waiting);
			}
		}
	}

	// Update is called once per frame
	public void Update ()
	{
		if (m_isbegin == false) 
		{
			return;
		}
		if (m_curactionidx < 0 || m_curactionidx >= m_ActionList.Count) 
		{
			return;
		}
		// 刷新逻辑是:->如果当前动作未开始,那么当前动作.start->当前动作update->如果当前动作结束,那么进行下一个动作,直到是最后一个动作为止,整个剧情结束
		while (true) 
		{
			var curaction = m_ActionList [m_curactionidx];
			if (curaction.GetState () < ActionBase.ActionState.AS_Processing) 
			{
				curaction.SetState (ActionBase.ActionState.AS_Processing);
				curaction.Start ();
			}

			// 当前剧情刷新
			curaction.Update();

			// 处理当前动作结束
			if (curaction.GetState () > ActionBase.ActionState.AS_Processing) 
			{
				if (m_isallactionend == true) 
				{
					break;
				}
				if (m_curactionidx >= m_ActionList.Count - 1) 
				{
					_End ();
					break;
				} 
				else 
				{
					_NextAction ();
				}
			} 
			else 
			{
				break;
			}
		}
	}

	// 添加action
	public void AddAction(table.StoryDefine kDefineStruct)
	{
		string str = kDefineStruct.ActionType;
		var actiontype = TabtoyConfigManager.GetConfig().GetActionByName (str);
		if (actiontype == null) 
		{
			Debug.LogError ("action " + str + "does not exist!!!");
			return;
		}
		Type type = Type.GetType(actiontype.ScriptName,true,true);
		var temp= Activator.CreateInstance(type);
		ActionBase actionInstance = temp as ActionBase;
		actionInstance.Init (kDefineStruct);
		m_ActionList.Add (actionInstance);
	}

	private void _End()
	{
		m_isallactionend = true;
		m_isbegin = false;
		// 这里派发事件
	}

    // 切换到下一个action
	private void _NextAction()
	{
		if (m_isbegin == false) 
		{
			return;
		}
		if (m_curactionidx >= m_ActionList.Count - 1) 
		{
			return;
		}

		m_curactionidx += 1;
	}

    // 判定是否所有的action都已经结束了
	public bool GetAllActionEnd()
	{
		return m_isallactionend;
	}

    // 获取当前action
    public ActionBase GetCurrentAction()
    {
        if (m_curactionidx < 0 || m_curactionidx >= m_ActionList.Count)
        {
            return null;
        }

        return m_ActionList[m_curactionidx];
    }

    // 获取下一个action
    public ActionBase GetNextAction()
    {
        if (m_curactionidx + 1 < 0 || m_curactionidx + 1 >= m_ActionList.Count)
        {
            return null;
        }

        return m_ActionList[m_curactionidx + 1];
    }
}