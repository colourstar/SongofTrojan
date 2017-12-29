/// <summary>
/// Game status.
/// 游戏正常运行流程,会进行LogicMain的启动与刷新
/// </summary>


using UnityEngine;
using System.Collections;

public class GameStatus : IApplicationStatus
{
	public override void OnEnterStatus()
	{
		// 初始化Logic的配置
		LogicMain.Init ();

        // 开始逻辑
        LogicMain.Start ();
	}

	public override void OnUpdate()
	{
		LogicMain.Update ();
	}
}