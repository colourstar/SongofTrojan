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
        ApplicationStatusManager.GetStatus<GameStatus>().CloseUI<DialogWindow>();
        ApplicationStatusManager.GetStatus<GameStatus>().OpenUI<DialogWindow>();
        End();
	}

	// Update is called once per frame
	public override void Update ()
	{
        
	}

    public void OnDialogEnd()
    {
        End();
    }

}