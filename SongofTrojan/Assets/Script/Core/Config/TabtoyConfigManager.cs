using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FrameWork;
using System.Text;
using System;
using LuaInterface;
using System.IO;

/// <summary>
/// excel配置管理,读取streaming assetspath/Config/Config.bin文件
/// </summary>
public static class TabtoyConfigManager
{
	public static table.Config m_tabtoyConfig = new table.Config();

	public static void Init()
	{
		var stream = new FileStream (Application.streamingAssetsPath + "/Config/Config.bin", FileMode.Open);
		stream.Position = 0;

		var reader = new tabtoy.DataReader(stream);

		if ( !reader.ReadHeader(  ) )
		{
			Console.WriteLine("combine file crack!");
			return;
		}
			
		table.Config.Deserialize(m_tabtoyConfig, reader);
		var directFetch = m_tabtoyConfig.Story[0];


		m_tabtoyConfig.TableLogger.AddTarget( new tabtoy.DebuggerTarget() );
	}

	public static table.Config GetConfig()
	{
		return m_tabtoyConfig;
	}
}

