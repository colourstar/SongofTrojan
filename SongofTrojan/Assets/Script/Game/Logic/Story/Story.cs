using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Story
{
	private string 				m_strStoryName = "";						// 剧情名称
	private List<ActionBase> 	m_arrActionList = new List<ActionBase>();	// 所有动作列表
	private int 				m_iCuractionidx = 0;						// 当前动作索引,0 ~ (size - 1)
	private bool 				m_bIsbegin = false;						// 剧情是否开始
	private Dictionary<string,string> m_dicRegister = new Dictionary<string,string>();	// 剧情内部的寄存器
	private int 				m_iResult = 0;							// 剧情结果值
	private bool 				m_bIsallactionend = false;				// 剧情所有动作都已结束

	// Use this for initialization
	public void Init (string storyname)
	{
        m_strStoryName = storyname;
	}

	public void Start()
	{
        Debug.Log ("[story] : start,storyname : " + m_strStoryName);
		m_bIsbegin = true;
		m_iCuractionidx = 0;
		m_bIsallactionend = false;
		if (m_arrActionList.Count == 0) 
		{
			_End ();
		}
		else 
		{
			for (int i = 0; i < m_arrActionList.Count; ++i) 
			{
				var action = m_arrActionList [i];
				action.SetState (ActionBase.ActionState.AS_Waiting);
			}
		}
	}

	// Update is called once per frame
	public void Update ()
	{
		if (m_bIsbegin == false) 
		{
			return;
		}
		if (m_iCuractionidx < 0 || m_iCuractionidx >= m_arrActionList.Count) 
		{
			return;
		}
		// 刷新逻辑是:->如果当前动作未开始,那么当前动作.start->当前动作update->如果当前动作结束,那么进行下一个动作,直到是最后一个动作为止,整个剧情结束
		while (true) 
		{
			var curaction = m_arrActionList [m_iCuractionidx];
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
				if (m_bIsallactionend == true) 
				{
					break;
				}
				if (m_iCuractionidx >= m_arrActionList.Count - 1) 
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
		m_arrActionList.Add (actionInstance);
	}

	private void _End()
	{
		m_bIsallactionend = true;
		m_bIsbegin = false;
		// 这里派发事件
	}

    // 切换到下一个action
	private void _NextAction()
	{
		if (m_bIsbegin == false) 
		{
			return;
		}
		if (m_iCuractionidx >= m_arrActionList.Count - 1) 
		{
			return;
		}

		m_iCuractionidx += 1;
	}

    // 判定是否所有的action都已经结束了
	public bool GetAllActionEnd()
	{
		return m_bIsallactionend;
	}

    // 获取当前action
    public ActionBase GetCurrentAction()
    {
        if (m_iCuractionidx < 0 || m_iCuractionidx >= m_arrActionList.Count)
        {
            return null;
        }

        return m_arrActionList[m_iCuractionidx];
    }

    // 获取下一个action
    public ActionBase GetNextAction()
    {
        if (m_iCuractionidx + 1 < 0 || m_iCuractionidx + 1 >= m_arrActionList.Count)
        {
            return null;
        }

        return m_arrActionList[m_iCuractionidx + 1];
    }
}