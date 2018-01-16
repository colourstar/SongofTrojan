/// <summary>
/// EnterMap Action.
/// </summary>
/// 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Action_EnterMap : ActionBase
{
    private int  m_iMapID = 0;           // 进入地图的ID

    // Use this for initialization
    public override void Init (table.StoryDefine kStruct)
    {
        m_iMapID = Convert.ToInt32(kStruct.Args1);
    }

    // Start Action
    public override void Start ()
    {
        Debug.Log ("[Action_EnterMap] : Start : ");

        MapManager kMapManager = ApplicationStatusManager.GetStatus<GameStatus>().GetLogicMain().GetModule("MapManager") as MapManager;
        kMapManager.EnterMap(m_iMapID);

        End();
    }
}