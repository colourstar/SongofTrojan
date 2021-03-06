﻿/// <summary>
/// Action dialog.
/// </summary>
/// 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Action_Dialog : ActionBase
{
	private string  m_dialogcontent = "";           // 对话内容
	private int     m_roleid = 0;                   // 对话者的立绘ID,目前只有一个,未来支持立绘方案的形式

	// Use this for initialization
	public override void Init (table.StoryDefine kStruct)
	{
		m_dialogcontent = kStruct.Args1;
		m_roleid = Convert.ToInt32(kStruct.Args2);
	}

	// Start Action
	public override void Start ()
	{
        Debug.Log ("[Action_Dialog] : Start : " + m_dialogcontent);

        ApplicationStatusManager.GetStatus<GameStatus>().OnDialog(m_dialogcontent,m_roleid);
	}

    public void OnDialogEnd()
    {
        // 对话结束的时候,需要判定一下是否需要隐藏界面,依据是下一个动作是否仍旧是action_dialog类型
        bool isneeddel = true;
        StoryManager kStoryManager = ApplicationStatusManager.GetStatus<GameStatus>().GetLogicMain().GetModule("StoryManager") as StoryManager;
        Story curstory = kStoryManager.GetCurrentStory();
        if (curstory != null)
        {
            Action_Dialog kNextAction = curstory.GetNextAction() as Action_Dialog;
            if (kNextAction != null)
            {
                isneeddel = false;
            }
        }
        if (isneeddel)
        {
            ApplicationStatusManager.GetStatus<GameStatus>().OnDialogEnd(isneeddel);
        }

        // 结束本动作
		End();
    }
}