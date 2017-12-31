/// <summary>
/// Logic main.
/// 主逻辑模块
/// </summary>

using UnityEngine;
using System.Collections;

public static class LogicMain
{
	public static StoryManager m_StoryManager = new StoryManager();
	public static MessageManager m_MessageManager = new MessageManager ();
	// Init Progress
	public static void Init()
	{
		m_StoryManager.Init ();
	}

    public static void Start()
    {
        m_StoryManager.Start();
		m_MessageManager.Start ();
    }

	// Update is called once per logic frame
	public static void Update ()
	{
		m_StoryManager.Update ();
		m_MessageManager.Update ();
	}
}