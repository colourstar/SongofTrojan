/// <summary>
/// Logic main.
/// 主逻辑模块
/// </summary>

using UnityEngine;
using System.Collections;

public static class LogicMain
{
	private static StoryManager m_StoryManager = new StoryManager();
	// Init Progress
	public static void Init()
	{
		m_StoryManager.Init ();
	}

	// Update is called once per logic frame
	public static void Update ()
	{
		m_StoryManager.Update ();
	}
}