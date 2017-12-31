/// <summary>
/// Action dialog.
/// </summary>
/// 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Action_Dialog : ActionBase
{
	private string m_dialogcontent = "";
	private int m_roleid = 0;

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
		if (UIManager.GetUI<DialogWindow>() == null) 
		{
			ApplicationStatusManager.GetStatus<GameStatus>().OpenUI<DialogWindow>();
		}
		DialogWindow kWindow = UIManager.GetUI<DialogWindow>() as DialogWindow;
		if (kWindow != null) 
		{
			kWindow.OnRefreshContent (m_dialogcontent,m_roleid);
		}
	}

    public void OnDialogEnd()
    {
		ApplicationStatusManager.GetStatus<GameStatus>().CloseUI<DialogWindow>();
		End();
    }
}