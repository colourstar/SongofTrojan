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

	// Use this for initialization
	public override void Init (table.StoryDefine kStruct)
	{
		m_dialogcontent = kStruct.Args1;
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
			kWindow.OnRefreshContent (m_dialogcontent,0);
		}
	}

    public void OnDialogEnd()
    {
		ApplicationStatusManager.GetStatus<GameStatus>().CloseUI<DialogWindow>();
		End();
    }
}