using UnityEngine;
using System.Collections;


public class StartStatus : IApplicationStatus
{
	protected StartWindow	m_kStartWindow = null;		// 启动界面
	protected LobbyWindow   m_kLobbyWindow = null;      // 切换等待界面

	public override void OnEnterStatus()
	{
		// 首先初始化配置
		TabtoyConfigManager.Init();

		if (m_kStartWindow == null)
		{
			m_kStartWindow = OpenUI<StartWindow> () as StartWindow;
		}
		m_kStartWindow.Show();
	}

	public void EnterToLobby()
	{
		if (m_kLobbyWindow == null)
		{
			m_kLobbyWindow = OpenUI<LobbyWindow> () as LobbyWindow;
		}
		m_kLobbyWindow.Show();
		m_kLobbyWindow.OnShow ();
		if (m_kStartWindow != null) 
		{
			m_kStartWindow.Hide ();
		}
	}

	public void EnterToStart()
	{
		if (m_kStartWindow == null)
		{
			m_kStartWindow = OpenUI<StartWindow> ();
		}
		m_kStartWindow.Show();
		m_kStartWindow.OnShow ();
		if (m_kLobbyWindow != null) 
		{
			m_kLobbyWindow.Hide ();
		}
	}


}

