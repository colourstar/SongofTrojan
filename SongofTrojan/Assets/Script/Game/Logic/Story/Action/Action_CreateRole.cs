/// <summary>
/// CreateRole Action.
/// </summary>
/// 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Action_CreateRole : ActionBase
{
    // Use this for initialization
    public override void Init (table.StoryDefine kStruct)
    {
    }

    // Start Action
    public override void Start ()
    {
        Debug.Log ("[Action_CreateRole] : Start : ");
    }

    public void OnCreateRoleEnd()
    {
        
        // 结束本动作
        End();
    }
}