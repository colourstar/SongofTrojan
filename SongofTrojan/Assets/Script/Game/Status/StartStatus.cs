using UnityEngine;
using System.Collections;


public class StartStatus : IApplicationStatus
{
	public override void OnEnterStatus()
	{
		// 首先初始化配置
		TabtoyConfigManager.Init();

		OpenUI<StartWindow> ();
	}
}

