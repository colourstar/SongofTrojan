/// <summary>
/// Action base.
/// </summary>
/// 


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ActionBase
{
	public enum ActionState
	{
		AS_Waiting = -1,
		AS_Processing = 0,
		AS_End = 1,
	}

	public ActionState m_eState = ActionState.AS_Waiting;           // 当前action的状态,处于等待/进行中/结束

	// Use this for initialization
	public virtual void Init (table.StoryDefine kStruct)
	{

	}

	// Start Action
	public virtual void Start ()
	{

	}

	// Update is called once per frame
	public virtual void Update ()
	{

	}

	public void SetState(ActionState eState)
	{
		m_eState = eState;
	}

	public ActionState GetState()
	{
		return m_eState;
	}

	public void End()
	{
		m_eState = ActionState.AS_End;
	}
}

